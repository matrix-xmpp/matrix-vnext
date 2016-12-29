using System;
using System.Collections.Generic;
using System.Text;
using DotNetty.Common.Internal.Logging;

using Microsoft.Extensions.Logging.Console;

namespace ConsoleClient
{
    public static class ExampleHelper
    {
        public static void SetConsoleLogger() => InternalLoggerFactory.DefaultFactory.AddProvider(new ConsoleLoggerProvider((s, level) => true, false));
    }
}
