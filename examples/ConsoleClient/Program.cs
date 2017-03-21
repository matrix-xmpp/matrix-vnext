/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
 *
 * This file is part of the MatriX project.
 *
 * NOTICE: All information contained herein is, and remains the property
 * of AG-Software and its suppliers, if any.
 * The intellectual and technical concepts contained herein are proprietary
 * to AG-Software and its suppliers and may be covered by German and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 *
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from AG-Software.
 *
 * Contact information for AG-Software is available at http://www.ag-software.de
 */

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
                //Password = "secret",
                //XmppDomain = "ag-software.net",

                //Username = "alex",
                //Password = "secret",
                //XmppDomain = "jabber.org",
                //XmppDomain = "localhost",
                
                // jabber.org
                Username = "gnauck",
                Password = "secret",
                XmppDomain = "jabber.org",
                HostnameResolver = new SrvNameResolver(),

                // local prosody
                //Username = "alex",
                //Password = "secret",
                //XmppDomain = "localhost",
                //HostnameResolver = new StaticNameResolver(IPAddress.Parse("192.168.1.151")),
                //CertificateValidator = new AlwaysAcceptCertificateValidator(),

                // Openfire Flepo
                //Username = "admin",
                //Password = "secret",
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
        }
    }
}
