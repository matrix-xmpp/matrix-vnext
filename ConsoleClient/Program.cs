using System;
using System.Reflection;
using ConsoleClient;
using Matrix;
using Matrix.Xml;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Factory.RegisterElementsFromAssembly(typeof(Matrix.Xmpp.Client.Iq).GetTypeInfo().Assembly);

            ExampleHelper.SetConsoleLogger();


            var xmppClient = new XmppClient()
            {
                Username = "alex",
                Password = "***REMOVED***",
                XmppDomain = "ag-software.net",
               
            };

            xmppClient.ConnectAsync().GetAwaiter().GetResult();

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }

}
