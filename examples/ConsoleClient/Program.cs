/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
 * See the COPYING file for more information.
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
using Matrix.Extensions.Client.Roster;
using Matrix.Extensions.Client.Presence;
using Matrix.Xmpp;
using Matrix.Xmpp.Base;
using Matrix.Srv;

using DotNetty.Transport.Channels;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // log with DotNetty's internal logger
            //DotNetty.Common.Internal.Logging.InternalLoggerFactory.DefaultFactory.AddProvider(new ConsoleLoggerProvider((s, level) => true, false));
            //var pipelineInitializerAction = new Action<IChannelPipeline>(pipeline =>
            //{
            //    pipeline.AddFirst(new DotNetty.Handlers.Logging.LoggingHandler());
            //});

            // log with custom DotNetty handler to standard netCore logger
            LoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole();
            var logger = loggerFactory.CreateLogger<Program>();

            var pipelineInitializerAction = new Action<IChannelPipeline>(pipeline =>
            {
                pipeline.AddFirst(new MyLoggingHandler(logger));
            });

            var xmppClient = new XmppClient(pipelineInitializerAction)
            {                
                Username = "alex",
                Password = "***REMOVED***",
                XmppDomain = "ag-software.net",
                HostnameResolver = new SrvNameResolver()
            };            

            xmppClient.XmppSessionStateObserver.Subscribe(v => {
                Debug.WriteLine($"State changed: {v}");
            });
          
            xmppClient
                .XmppXElementStreamObserver
                .Where(el => el is Presence)
                .Subscribe(el =>
                {
                    Debug.WriteLine(el.ToString());
                });
           
            xmppClient
                .XmppXElementStreamObserver
                .Where(el => el is Message)
                .Subscribe(el =>
                {
                    Debug.WriteLine(el.ToString());
                });

            xmppClient
                .XmppXElementStreamObserver
                .Where(el => el is Iq)
                .Subscribe(el =>
                {
                    Debug.WriteLine(el.ToString());
                });

            // Connect the XMPP connection
            xmppClient.ConnectAsync().GetAwaiter().GetResult();

            // request the roster (aka contact list)
            var roster = xmppClient.RequestRosterAsync().GetAwaiter().GetResult();
            Console.WriteLine(roster.ToString());

            // Send our presence to the server
            xmppClient.SendPresenceAsync(Show.Chat, "free for chat").GetAwaiter().GetResult();
            
            Console.ReadLine();

            // Disconnect the XMPP connection
            xmppClient.DisconnectAsync().GetAwaiter().GetResult();

            Console.ReadLine();
        }
    }
}
