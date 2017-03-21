function Get-XmlNamespaceManager([ xml ]$XmlDocument, [string]$NamespaceURI = "")
{
    # If a Namespace URI was not given, use the Xml document's default namespace.
    if ([string]::IsNullOrEmpty($NamespaceURI)) { $NamespaceURI = $XmlDocument.DocumentElement.NamespaceURI }   
     
    # In order for SelectSingleNode() to actually work, we need to use the fully qualified node path along with an Xml Namespace Manager, so set them up.
    [System.Xml.XmlNamespaceManager]$xmlNsManager = New-Object System.Xml.XmlNamespaceManager($XmlDocument.NameTable)
    $xmlNsManager.AddNamespace("ns", $NamespaceURI)
    return ,$xmlNsManager       # Need to put the comma before the variable name so that PowerShell doesn't convert it into an Object[].
}
 
function Get-FullyQualifiedXmlNodePath([string]$NodePath, [string]$NodeSeparatorCharacter = '.')
{
    return "/ns:$($NodePath.Replace($($NodeSeparatorCharacter), '/ns:'))"
}

function Get-XmlNode([ xml ]$XmlDocument, [string]$NodePath, [string]$NamespaceURI = "", [string]$NodeSeparatorCharacter = '.')
{
    $xmlNsManager = Get-XmlNamespaceManager -XmlDocument $XmlDocument -NamespaceURI $NamespaceURI
    [string]$fullyQualifiedNodePath = Get-FullyQualifiedXmlNodePath -NodePath $NodePath -NodeSeparatorCharacter $NodeSeparatorCharacter
     
    # Try and get the node, then return it. Returns $null if the node was not found.
    $node = $XmlDocument.SelectSingleNode($fullyQualifiedNodePath, $xmlNsManager)
    return $node
}

function Set-XmlElementsTextValue([ xml ]$XmlDocument, [string]$ElementPath, [string]$TextValue, [string]$NamespaceURI = "", [string]$NodeSeparatorCharacter = '.')
{
    # Try and get the node. 
    $node = Get-XmlNode -XmlDocument $XmlDocument -NodePath $ElementPath -NamespaceURI $NamespaceURI -NodeSeparatorCharacter $NodeSeparatorCharacter
     
    # If the node already exists, update its value.
    if ($node)
    { 
        $node.InnerText = $TextValue
    }
    # Else the node doesn't exist yet, so create it with the given value.
    else
    {
        # Create the new element with the given value.
        $elementName = $ElementPath.SubString($ElementPath.LastIndexOf($NodeSeparatorCharacter) + 1)
        $element = $XmlDocument.CreateElement($elementName, $XmlDocument.DocumentElement.NamespaceURI)      
        $textNode = $XmlDocument.CreateTextNode($TextValue)
        $element.AppendChild($textNode) > $null
         
        # Try and get the parent node.
        $parentNodePath = $ElementPath.SubString(0, $ElementPath.LastIndexOf($NodeSeparatorCharacter))
        $parentNode = Get-XmlNode -XmlDocument $XmlDocument -NodePath $parentNodePath -NamespaceURI $NamespaceURI -NodeSeparatorCharacter $NodeSeparatorCharacter
         
        if ($parentNode)
        {
            $parentNode.AppendChild($element) > $null
        }
        else
        {
            throw "$parentNodePath does not exist in the xml."
        }
    }
}

function Get-XmlElementsTextValue([ xml ]$XmlDocument, [string]$ElementPath, [string]$NamespaceURI = "", [string]$NodeSeparatorCharacter = '.')
{
    # Try and get the node. 
    $node = Get-XmlNode -XmlDocument $XmlDocument -NodePath $ElementPath -NamespaceURI $NamespaceURI -NodeSeparatorCharacter $NodeSeparatorCharacter
     
    # If the node already exists, return its value, otherwise return null.
    if ($node) { return $node.InnerText } else { return $null }
}


$buildNumber = $env:BUILD_BUILDNUMBER
if ($buildNumber -eq $null)
{
    $buildIncrementalNumber = 0
}
else
{
    $splitted = $buildNumber.Split('.')
    $buildIncrementalNumber = $splitted[$splitted.Length - 1]
}
      
$SrcPath = $env:BUILD_SOURCESDIRECTORY
Write-Verbose "Executing Update-AssemblyInfoVersionFiles in path $SrcPath for product version Version $productVersion"  -Verbose
 
$AllProjectFiles = Get-ChildItem $SrcPath *.csproj -recurse

#calculation Julian Date 
$year = Get-Date -format yy
$julianYear = $year.Substring(0)
$dayOfYear = (Get-Date).DayofYear
$julianDate = $julianYear + "{0:D3}" -f $dayOfYear
Write-Verbose "Julian Date: $julianDate" -Verbose
     
foreach ($file in $AllProjectFiles) 
{ 
    #default values for each segment
    $v1="1"
    $v2="0"
    $v3="0"
    $v4="0"

    #load the file and process the lines
    # Read in the file contents, update the version node's value, and save the file.
    [xml] $xml = Get-Content -Path $file.FullName    
   
    $origVersion = Get-XmlElementsTextValue -XmlDocument $xml -ElementPath  "Project.PropertyGroup.AssemblyVersion"
    
    if ($origVersion)
    {        
        $segments=$origVersion.Split(".")            
        
        #assign them based on what was found
        if ($segments.Length -gt 0) { $v1=$segments[0] }
        if ($segments.Length -gt 1) { $v2=$segments[1] } 
        if ($segments.Length -gt 2) { $v3=$segments[2] } 
        if ($segments.Length -gt 3) { $v4=$segments[3] }      
        
        Write-Verbose "Found Major is $v1" -Verbose
        Write-Verbose "Found Minor is $v2" -Verbose         

    
        $assemblyVersion     = "$v1.$v2.$julianDate.$buildIncrementalNumber"
        $assemblyFileVersion = "$v1.$v2.$julianDate.$buildIncrementalNumber"

        Set-XmlElementsTextValue -XmlDocument $xml -ElementPath "Project.PropertyGroup.AssemblyVersion" -TextValue $assemblyVersion
        Set-XmlElementsTextValue -XmlDocument $xml -ElementPath "Project.PropertyGroup.FileVersion" -TextValue $assemblyFileVersion
        Set-XmlElementsTextValue -XmlDocument $xml -ElementPath "Project.PropertyGroup.Version" -TextValue $assemblyFileVersion

        Write-Verbose "Transformed Assembly Version is $assemblyVersion" -Verbose
        Write-Verbose "Transformed Assembly File Version is $assemblyFileVersion" -Verbose 

        $xml.Save($file.FullName )  
    }
}