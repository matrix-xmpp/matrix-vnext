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

using System.Linq;
using System.Reflection;
using DotNetty.Transport.Channels;
using Matrix;
using Matrix.Network.Codecs;
using Matrix.Xml;
using Matrix.Xmpp.Stream;

namespace Server.Handlers
{
    public class ServerConnectionHandler : SimpleChannelInboundHandler<XmlStreamEvent>
    {
        private IChannelHandlerContext context;

        public string  XmppDomain { get; internal set; }

        public string User { get; set; }
        public string Resource { get; set; }

        public SessionState SessionState { get; set; } = SessionState.Disconnected;

        public void ResetStream(IChannelPipeline pipeline)
        {
            pipeline.Get<XmlStreamDecoder>().Reset();
        }
        public void ResetStream()
        {
            ResetStream(context.Channel.Pipeline);
        }

        protected override async void ChannelRead0(IChannelHandlerContext ctx, XmlStreamEvent xmlStreamEvent)
        {
            context = ctx;

            if (xmlStreamEvent.XmlStreamEventType == XmlStreamEventType.StreamStart)
            {
                if (xmlStreamEvent.XmppXElement.OfType<Matrix.Xmpp.Base.Stream>())
                {
                    var inStream = xmlStreamEvent.XmppXElement.Cast<Matrix.Xmpp.Base.Stream>();
                    Jid host = inStream.To;
                    if (ServerSettings.HostExists(host))
                    {
                        // host exists
                        XmppDomain = host;

                        var stream = new Matrix.Xmpp.Client.Stream
                        {
                            Version = "1.0",
                            From = XmppDomain,
                            Id = Id.GenerateShortGuid()
                        };

                        await ctx.WriteAndFlushAsync(stream.StartTag());
                        await ctx.WriteAndFlushAsync(BuildStreamFeatures(ctx));
                    }
                    else
                    {
                        // we don't server this host. Return unknown host.
                        /*
                         * <?xml version='1.0'?>
                           <stream:stream
                               from='im.example.com'
                               id='g4qSvGvBxJ+xeAd7QKezOQJFFlw='
                               to='example.net'
                               version='1.0'
                               xml:lang='en'
                               xmlns='jabber:server'
                               xmlns:stream='http://etherx.jabber.org/streams'>
                           <stream:error>
                             <host-unknown
                                 xmlns='urn:ietf:params:xml:ns:xmpp-streams'/>
                           </stream:error>
                           </stream:stream>

                         */
                        var stream = new Matrix.Xmpp.Client.Stream
                        {
                            Version = "1.0",
                            From = ServerSettings.Hosts[0]["domain"],
                            Id = Id.GenerateShortGuid()
                        };


                        await ctx.WriteAndFlushAsync(stream.StartTag());
                        await ctx.WriteAndFlushAsync(new Error(ErrorCondition.HostUnknown));
                        await ctx.WriteAndFlushAsync(stream.EndTag());
                        await ctx.CloseAsync();
                    }
                }
                return;
                //ctx.FireChannelRead(xmlStreamEvent);
            }

            ctx.FireChannelRead(xmlStreamEvent);
        }

        private XmppXElement BuildStreamFeatures(IChannelHandlerContext ctx)
        {
            var feat = new StreamFeatures();

            ctx.Channel.Pipeline
                .Where(h => h.GetType().GetTypeInfo().ImplementedInterfaces.Contains(typeof(IStreamFeature)))
                .Cast<IStreamFeature>()
                .ToList().ForEach(
                    sf => sf.AddStreamFeatures(this, feat)
                );
            
            return feat;
        }
    }
}
