using System;
using System.Xml;
using System.Reactive.Linq;
using System.Reflection;
using System.Xml.Linq;
using Matrix;
using Matrix.Network;
using Matrix.Xml;
using Matrix.Xmpp;
using Matrix.Xmpp.Base;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string xml1 = "<a><b>foo</bb></a>";
            var el1 = XmppXElement.LoadXml(xml1);

            ExampleHelper.SetConsoleLogger();

            var xmppClient = new XmppClient
            {
                // AG-Software
                //Username = "alex",
                //Password = "***REMOVED***",
                //XmppDomain = "ag-software.net",

                //Username = "alex",
                //Password = "***REMOVED***",
                //XmppDomain = "jabber.org",
                //XmppDomain = "localhost",

                // jabber.org
                //Username = "gnauck",
                //Password = "***REMOVED***",
                //XmppDomain = "jabber.org",

                // local prosody
                //Username = "alex",
                //Password = "***REMOVED***",
                //XmppDomain = "localhost",
                //HostnameResolver = new StaticNameResolver(IPAddress.Parse("192.168.1.151")),
                //CertificateValidator = new AlwaysAcceptCertificateValidator(),

                // Openfire Flepo
                Username = "admin",
                Password = "***REMOVED***",
                XmppDomain = "flepo",
                Tls = true,
                Compression = false,
                Resource = "vnext",
                CertificateValidator = new AlwaysAcceptCertificateValidator(),


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

            xmppClient.SendPresenceAsync(Show.Chat, "free for chat");
            

            Console.WriteLine("Hello World!");
            Console.ReadLine();

            var ret1 = xmppClient.CloseAsync().GetAwaiter().GetResult();

            Console.ReadLine();
        }
    }

}
