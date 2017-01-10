using DotNetty.Common.Internal.Logging;

using Microsoft.Extensions.Logging.Console;

namespace ConsoleClient
{
    public static class ExampleHelper
    {
        //public static void SetConsoleLogger() => InternalLoggerFactory.DefaultFactory.AddProvider(new ConsoleLoggerProvider((s, level) => true, false));

        public static void SetConsoleLogger()
        {
          InternalLoggerFactory.DefaultFactory.AddProvider(new ConsoleLoggerProvider((s, level) =>  true, false));  
        } 
    }
}
