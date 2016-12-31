using System;
using System.Reactive.Linq;
using System.Reflection;
using ConsoleClient;
using Matrix;
using Matrix.Xml;
using Matrix.Xmpp;
using Matrix.Xmpp.Base;
using Matrix.Xmpp.Ping;
using Matrix.Xmpp.Stream;

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
                //Username = "alex",
                //Password = "***REMOVED***",
                //XmppDomain = "ag-software.net",
                Username = "gnauck",
                Password = "***REMOVED***",
                XmppDomain = "jabber.org",
                Resource = "vnext"
            };

            xmppClient
                .XmppXElementStream
                .Where(el => el is Presence)
                .Subscribe(el =>
                {
                    System.Diagnostics.Debug.WriteLine(el.ToString());
                });

            xmppClient
                .XmppXElementStream
                .Where(el => el is Message)
                .Subscribe(el =>
                {
                    System.Diagnostics.Debug.WriteLine(el.ToString());
                });

            xmppClient
                .XmppXElementStream
                .Where(el => el is Iq)
                .Subscribe(el =>
                {
                    System.Diagnostics.Debug.WriteLine(el.ToString());
                });

            xmppClient.ConnectAsync().GetAwaiter().GetResult();

            var roster = xmppClient.RequestRosterAsync().GetAwaiter().GetResult();
            Console.WriteLine(roster.ToString());

            xmppClient.SendPresenceAsync().GetAwaiter().GetResult();
            

            Console.WriteLine("Hello World!");
            Console.ReadLine();

            xmppClient.CloseAsync().GetAwaiter().GetResult();

            Console.ReadLine();
        }
    }

}
