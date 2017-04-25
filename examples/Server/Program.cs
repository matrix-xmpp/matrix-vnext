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
using System.IO;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Handlers.Tls;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Matrix;
using Matrix.Network;
using Matrix.Network.Codecs;
using Matrix.Network.Handlers;
using Matrix.Network.Resolver;
using Matrix.Xml;
using Matrix.Xmpp;
using Matrix.Xmpp.Base;
using Matrix.Xmpp.Stream;

using System.Reflection;
using Server.Handlers;

namespace Server
{
    class Program
    {
        
        //static readonly XmppStreamEventHandler  xmppStreamEventHandler  = new XmppStreamEventHandler();
        //static readonly XmppStanzaHandler       xmppStanzaHandler       = new XmppStanzaHandler();

        //private static IObservable<XmlStreamEvent> XmlStreamEvent => xmppStreamEventHandler.XmlStreamEvent;

        private static readonly SaslPlainHandler salsPlainHandler = new SaslPlainHandler();

        private static IChannelPipeline pipeline;
        static async Task RunServerAsync()
        {
            ExampleHelper.SetConsoleLogger();
         
            var bossGroup = new MultithreadEventLoopGroup(1);
            var workerGroup = new MultithreadEventLoopGroup();
            try
            {
                var bootstrap = new ServerBootstrap();
                bootstrap
                    .Group(bossGroup, workerGroup)
                    .Channel<TcpServerSocketChannel>()
                    .Option(ChannelOption.SoBacklog, 100)
                    .Handler(new LoggingHandler("SRV-LSTN"))
                    .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        pipeline = channel.Pipeline;
                       
                        pipeline.AddLast(new LoggingHandler("SRV-CONN"));

                        pipeline.AddLast(new XmlStreamDecoder());


                        //Pipeline.AddLast(new StringEncoder());
                        //pipeline.AddLast(new ZlibEncoder());
                        pipeline.AddLast(new XmppXElementEncoder());
                        pipeline.AddLast(new UTF8StringEncoder());

                        

                        pipeline.AddLast(new ServerConnectionHandler());
                       
                        //pipeline.AddLast(new StreamFooterHandler());
                        pipeline.AddLast(new XmppStreamEventHandler());

                        pipeline.AddLast(new StartTlsHandler(new PfxCertificateProvider()));
                        pipeline.AddLast(new SaslPlainHandler());
                        pipeline.AddLast(new SaslScramHandler());
                        pipeline.AddLast(new BindHandler());
                        pipeline.AddLast(new SessionHandler());

                        pipeline.AddLast(new XmppStanzaHandler());


                        //pipeline.AddLast(salsPlainHandler);


                    }));


             

                IChannel boundChannel = await bootstrap.BindAsync(IPAddress.Any, ServerSettings.Port);

                Console.ReadLine();

                await boundChannel.CloseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                await Task.WhenAll(
                    bossGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)),
                    workerGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)));
            }
        }

     
        static void Main() => RunServerAsync().Wait();
    }

}
