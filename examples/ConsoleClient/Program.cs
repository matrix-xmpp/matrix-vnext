using System;
using System.Reactive.Linq;
using Matrix;
using Matrix.Xmpp;
using Matrix.Xmpp.Base;
using Matrix.Srv;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Channels;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleHelper.SetConsoleLogger();

            var pipelineInitializerAction = new Action<IChannelPipeline>(pipeline => {
                pipeline.AddFirst(new LoggingHandler());
            });


            var xmppClient = new XmppClient(pipelineInitializerAction)
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
                Username = "gnauck",
                Password = "***REMOVED***",
                XmppDomain = "jabber.org",
                HostnameResolver = new SrvNameResolver(),

                // local prosody
                //Username = "alex",
                //Password = "***REMOVED***",
                //XmppDomain = "localhost",
                //HostnameResolver = new StaticNameResolver(IPAddress.Parse("192.168.1.151")),
                //CertificateValidator = new AlwaysAcceptCertificateValidator(),

                // Openfire Flepo
                //Username = "admin",
                //Password = "***REMOVED***",
                //XmppDomain = "flepo",
                //Tls = true,
                //Compression = false,
                //Resource = "vnext",
                //CertificateValidator = new AlwaysAcceptCertificateValidator(),


            };

            //xmppClient.AddHandler(new AutoReplyToPingHandler<Iq>());


            
            //xmppClient
            //   .XmppXElementStream
            //   .Where(el => el is Presence)
            //   .Subscribe(el =>
            //   {
            //       System.Diagnostics.Debug.WriteLine(el.ToString());                   
            //   });

            xmppClient.WhenXmppSessionStateChanged.Subscribe(v => {
                System.Diagnostics.Debug.WriteLine($"State changed: {v}");
            });
          

            xmppClient
                .XmppXElementStreamObserver
                .Where(el => el is Presence)
                .Subscribe(el =>
                {
                    System.Diagnostics.Debug.WriteLine(el.ToString());
                });

            xmppClient
                .XmppXElementStreamObserver
                .Where(el => el is Message)
                .Subscribe(el =>
                {
                    System.Diagnostics.Debug.WriteLine(el.ToString());
                });

            xmppClient
                .XmppXElementStreamObserver
                .Where(el => el is Iq)
                .Subscribe(el =>
                {
                    System.Diagnostics.Debug.WriteLine(el.ToString());
                });

            xmppClient.ConnectAsync().GetAwaiter().GetResult();

            var roster = xmppClient.RequestRosterAsync().GetAwaiter().GetResult();
            Console.WriteLine(roster.ToString());

            xmppClient.SendPresenceAsync(Show.Chat, "free for chat").GetAwaiter().GetResult();
            

            Console.WriteLine("Hello World!");
            Console.ReadLine();

            var ret1 = xmppClient.CloseAsync().GetAwaiter().GetResult();

            Console.ReadLine();


            xmppClient.ConnectAsync().GetAwaiter().GetResult();

             roster = xmppClient.RequestRosterAsync().GetAwaiter().GetResult();
            Console.WriteLine(roster.ToString());

            xmppClient.SendPresenceAsync(Show.Chat, "free for chat").GetAwaiter().GetResult();


            Console.WriteLine("Hello World!");
            Console.ReadLine();
            ret1 = xmppClient.CloseAsync().GetAwaiter().GetResult();
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }

}
