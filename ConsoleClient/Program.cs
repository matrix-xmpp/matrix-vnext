using System;
using System.Net;
using System.Reactive.Linq;
using System.Reflection;
using ConsoleClient;
using Matrix;
using Matrix.Network;
using Matrix.Network.Resolver;
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

            var xmppClient = new XmppClient
            {
                //Username = "alex",
                //Password = "***REMOVED***",
                //XmppDomain = "ag-software.net",
                //Username = "alex",
                //Password = "***REMOVED***",
                //XmppDomain = "jabber.org",
                //XmppDomain = "localhost",

                // jabber.org
                Username = "gnauck",
                Password = "***REMOVED***",
                XmppDomain = "jabber.org",

                // local prosody
                //Username = "alex",
                //Password = "***REMOVED***",
                //XmppDomain = "localhost",
                //HostnameResolver = new StaticNameResolver(IPAddress.Parse("192.168.1.151")),
                //CertificateValidator = new AlwaysAcceptCertificateValidator(),

                Resource = "vnext",

               
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

            var ret1 = xmppClient.CloseAsync().GetAwaiter().GetResult();

            Console.ReadLine();
        }
    }

}
