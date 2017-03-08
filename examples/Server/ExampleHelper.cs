// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using DotNetty.Common.Internal.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Console;

namespace Server
{
    public static class ExampleHelper
    {
        static ExampleHelper()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(ProcessDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public static string ProcessDirectory =>  System.AppContext.BaseDirectory;

        public static IConfigurationRoot Configuration { get; }

        public static void SetConsoleLogger() => InternalLoggerFactory.DefaultFactory.AddProvider(new ConsoleLoggerProvider((s, level) => true, false));
    }
}