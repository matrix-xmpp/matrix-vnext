#addin "Cake.FileHelpers"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

/*
    Additional arguments:
    civersion:      when present a ci version should be build. Its appending
                    ci-$juliandate-$buildnumber

    nugetpush:      when present will push nuget packages

    nuget.feed:     the Uri of the nuget feed
    nuget.token:    Nuget Api-key for pushing packages
 */

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
    {
        // clear all bin directories
        var binDirs = GetDirectories("./**/bin");
        foreach(var dir in binDirs)
        {
            CleanDirectory(Directory(dir.FullPath));
        }

        // clear all test results
        var testDirs = GetDirectories("./**/TestResults");
        foreach(var dir in testDirs)
        {
            CleanDirectory(Directory(dir.FullPath));
        }		
    });

Task("Update-Assembly-Version")
    .WithCriteria(HasArgument("civersion") && (TFBuild.IsRunningOnVSTS || TFBuild.IsRunningOnTFS))
    .IsDependentOn("Clean")
    .Does(() =>
    {
        var year = System.DateTime.Now.ToString("yy");
        var julianYear = year.Substring(0);
        var dayOfYear = DateTime.Now.DayOfYear;
        var julianDate = julianYear + String.Format("{0:D3}", dayOfYear);

        var vstsBuildNumber = TFBuild.Environment.Build.Number;
        var splitted = vstsBuildNumber.Split('.');
        var buildIncrementalNumber = splitted[splitted.Length - 1];

        var projects = GetFiles("./src/**/*.csproj");
        foreach(var project in projects)
        {
            var currentVersion = XmlPeek(project.FullPath, "/Project/PropertyGroup/AssemblyVersion");
            var newVersion = $"{currentVersion}-ci-{julianDate}-{buildIncrementalNumber}";

            XmlPoke(project.FullPath, "/Project/PropertyGroup/FileVersion", currentVersion);
            XmlPoke(project.FullPath, "/Project/PropertyGroup/Version", newVersion);
        }
    });

Task("Restore-NuGet-Packages")
    .IsDependentOn("Update-Assembly-Version")
    .Does(() =>
    {
        NuGetRestore("./MatriX-vNext.sln");
    });

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
    {
        if(IsRunningOnWindows())
        {
        // Use MSBuild
        MSBuild("./MatriX-vNext.sln", settings =>
            settings.SetConfiguration(configuration));
        }
        else
        {
        // Use XBuild
        XBuild("./MatriX-vNext.sln", settings =>
            settings.SetConfiguration(configuration));
        }
    });

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var projects = GetFiles("./test/**/*.csproj");
        foreach(var project in projects)
        {
            DotNetCoreTest(
                project.FullPath,
                new DotNetCoreTestSettings()
                {
                    Configuration = configuration,
                    NoBuild = true,
                    // Outputing test results as XML so that VSTS can pick then up in another task
                    ArgumentCustomization = args => args.Append("--logger \"trx;LogFileName=TestResults.xml\"")
                });
        }
    });


Task("Publish-Nuget")
    .IsDependentOn("Run-Unit-Tests")
    .WithCriteria(HasArgument("nugetpush"))
    .ContinueOnError()
    .Does(() =>
    {
        // Get the paths to the packages.
        var packages = GetFiles("./src/**/Matrix*.nupkg");
        foreach(var package in packages)
        {
            try
            {
                // Push the package.
                NuGetPush(package, new NuGetPushSettings {
                    Source = Argument<string>("nuget.feed"),
                    ApiKey = Argument<string>("nuget.token")
                });
            }
            catch(System.Exception ex)
            {
                // ignore
            }
        }
    }); 
   

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Publish-Nuget");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
