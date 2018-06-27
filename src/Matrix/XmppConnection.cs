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
using System.Threading.Tasks;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Matrix.Network;
using Matrix.Network.Codecs;
using Matrix.Network.Handlers;
using Matrix.Network.Resolver;
using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Stream;
using System.Threading;
using DotNetty.Buffers;

namespace Matrix
{
    public abstract class XmppConnection : IStanzaSender, IDisposable
    {
        protected Bootstrap Bootstrap = new Bootstrap();
        readonly MultithreadEventLoopGroup eventLoopGroup = new MultithreadEventLoopGroup();

        readonly XmppStreamEventHandler xmppStreamEventHandler = new XmppStreamEventHandler();
        private INameResolver resolver = new NameResolver();

        protected XmppConnection()
            : this(null)
        {
        }

        protected XmppConnection(Action<IChannelPipeline> pipelineInitializerAction)
        {
            Bootstrap
                .Group(eventLoopGroup)
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Option(ChannelOption.SoKeepalive, true)
                .Resolver(HostnameResolver)
                .Option(ChannelOption.Allocator, UnpooledByteBufferAllocator.Default)
                .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                {
                    Pipeline = channel.Pipeline;

                    Pipeline.AddLast2(new ZlibDecoder());

                    Pipeline.AddLast2(new KeepAliveHandler());

                    Pipeline.AddLast2(new XmlStreamDecoder());

                    Pipeline.AddLast2(new ZlibEncoder());

                    Pipeline.AddLast2(new XmppXElementEncoder());


                    Pipeline.AddLast2(new UTF8StringEncoder());


                    Pipeline.AddLast2(new XmppPingHandler<Iq>());
                    Pipeline.AddLast2(xmppStreamEventHandler);

                    //Pipeline.AddLast2(new StreamManagementHandler());

                    Pipeline.AddLast2(new StreamFooterHandler());

                    Pipeline.AddLast2(XmppStanzaHandler);

                    Pipeline.AddLast2(new CatchAllXmppStanzaHandler());

                    Pipeline.AddLast2(new DisconnectHandler(this));

                    pipelineInitializerAction?.Invoke(Pipeline);
                }));
        }

        #region << Properties >>
        public IChannelPipeline Pipeline { get; protected set; } = null;

        internal XmppSessionState XmppSessionState { get; } = new XmppSessionState();

        public string XmppDomain { get; set; }

        public int Port { get; set; } = 5222;

        public ICertificateValidator CertificateValidator { get; set; } = new DefaultCertificateValidator();

        private readonly XmppStanzaHandler XmppStanzaHandler = new XmppStanzaHandler();

        // Observers
        private IObservable<XmlStreamEvent> XmlStreamEventObserver => xmppStreamEventHandler.XmlStreamEvent;
        public IObservable<XmppXElement> XmppXElementStreamObserver => xmppStreamEventHandler.XmppXElementStream;
        public IObservable<SessionState> XmppSessionStateObserver => XmppSessionState.ValueChanged;

        public INameResolver HostnameResolver
        {
            get { return resolver; }
            set
            {
                resolver = value;
                Bootstrap.Resolver(resolver);
            }
        }
        #endregion

        #region << Send members >>

        #region << SendAsync string members >>
        protected async Task SendAsync(string data)
        {
            await Pipeline.WriteAndFlushAsync(data);
        }

        protected async Task<T> SendAsync<T>(string data)
            where T : XmppXElement
        {
            return await SendAsync<T>(data, XmppStanzaHandler.DefaultTimeout);
        }
        protected async Task<T> SendAsync<T>(string data, int timeout)
           where T : XmppXElement
        {
            return await XmppStanzaHandler.SendAsync<T>(data, timeout);
        }

        protected async Task<T> SendAsync<T>(string data, CancellationToken cancellationToken)
           where T : XmppXElement
        {
            return await SendAsync<T>(data, XmppStanzaHandler.DefaultTimeout, cancellationToken);
        }

        protected async Task<T> SendAsync<T>(string data, int timeout, CancellationToken cancellationToken)
           where T : XmppXElement
        {
            return await XmppStanzaHandler.SendAsync<T>(data, timeout, cancellationToken);
        }

        protected async Task<XmppXElement> SendAsync<T1, T2>(string data)
            where T1 : XmppXElement
            where T2 : XmppXElement
        {
            return await SendAsync<T1, T2>(data, XmppStanzaHandler.DefaultTimeout);
        }

        protected async Task<XmppXElement> SendAsync<T1, T2>(string data, int timeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
        {
            return await XmppStanzaHandler.SendAsync<T1, T2>(data, timeout);
        }

        protected async Task<XmppXElement> SendAsync<T1, T2>(string data, CancellationToken cancellationToken)
          where T1 : XmppXElement
          where T2 : XmppXElement
        {
            return await SendAsync<T1, T2>(data, XmppStanzaHandler.DefaultTimeout, cancellationToken);
        }

        protected async Task<XmppXElement> SendAsync<T1, T2>(string data, int timeout, CancellationToken cancellationToken)
           where T1 : XmppXElement
           where T2 : XmppXElement
        {
            return await XmppStanzaHandler.SendAsync<T1, T2>(data, timeout, cancellationToken);
        }
        #endregion

        #region << SendAsync XmppXElement members >>
        /// <summary>
        /// Send a XmppXElement asynchronous
        /// </summary>
        /// <param name="el"></param>
        /// <returns></returns>
        public virtual async Task SendAsync(XmppXElement el)
        {
            await Pipeline.WriteAndFlushAsync(el);
        }

        /// <summary>
        /// Send a XmppXElement asynchronous
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="el"></param>
        /// <returns></returns>
        public virtual async Task<T> SendAsync<T>(XmppXElement el)
             where T : XmppXElement
        {
            return await SendAsync<T>(el, XmppStanzaHandler.DefaultTimeout);
        }

        /// <summary>
        /// Send a XmppXElement asynchronous
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="el"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public virtual async Task<T> SendAsync<T>(XmppXElement el, int timeout)
             where T : XmppXElement
        {
            return await XmppStanzaHandler.SendAsync<T>(el, timeout);
        }

        /// <summary>
        /// Send a XmppXElement asynchronous
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="el"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<T> SendAsync<T>(XmppXElement el, CancellationToken cancellationToken)
             where T : XmppXElement
        {
            return await SendAsync<T>(el, XmppStanzaHandler.DefaultTimeout, cancellationToken);
        }

        /// <summary>
        /// Send a XmppXElement asynchronous
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="el"></param>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<T> SendAsync<T>(XmppXElement el, int timeout, CancellationToken cancellationToken)
             where T : XmppXElement
        {
            return await XmppStanzaHandler.SendAsync<T>(el, timeout, cancellationToken);
        }

        /// <summary>
        /// Send a XmppXElement asynchronous
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="el"></param>
        /// <returns></returns>
        public virtual async Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el)
            where T1 : XmppXElement
            where T2 : XmppXElement
        {
            return await SendAsync<T1, T2>(el, XmppStanzaHandler.DefaultTimeout);
        }

        public virtual async Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el, int timeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
        {
            return await XmppStanzaHandler.SendAsync<T1, T2>(el, timeout);
        }

        public virtual async Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el, CancellationToken cancellationToken)
            where T1 : XmppXElement
            where T2 : XmppXElement
        {
            return await SendAsync<T1, T2>(el, XmppStanzaHandler.DefaultTimeout, cancellationToken);
        }

        public virtual async Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el, int timeout, CancellationToken cancellationToken)
            where T1 : XmppXElement
            where T2 : XmppXElement
        {
            return await XmppStanzaHandler.SendAsync<T1, T2>(el, timeout, cancellationToken);
        }

        public virtual async Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
        {
            return await SendAsync<T1, T2, T3>(el, XmppStanzaHandler.DefaultTimeout);
        }

        public virtual async Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el, int timeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
        {
            return await XmppStanzaHandler.SendAsync<T1, T2, T3>(el, timeout);
        }

        public virtual async Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el, CancellationToken cancellationToken)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
        {
            return await SendAsync<T1, T2, T3>(el, XmppStanzaHandler.DefaultTimeout, cancellationToken);
        }

        public virtual async Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el, int timeout, CancellationToken cancellationToken)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
        {
            return await XmppStanzaHandler.SendAsync<T1, T2, T3>(el, timeout, cancellationToken);
        }

        public virtual async Task<XmppXElement> SendAsync(XmppXElement el, Func<XmppXElement, bool> predicate)
        {
            return await SendAsync(el, predicate, XmppStanzaHandler.DefaultTimeout, CancellationToken.None);
        }
        public virtual async Task<XmppXElement> SendAsync(XmppXElement el, Func<XmppXElement, bool> predicate, CancellationToken cancellationToken)
        {
            return await SendAsync(el, predicate, XmppStanzaHandler.DefaultTimeout, cancellationToken);
        }

        public virtual async Task<XmppXElement> SendAsync(XmppXElement el, Func<XmppXElement, bool> predicate, int timeout)
        {
            return await SendAsync(el, predicate, timeout, CancellationToken.None);
        }

        public virtual async Task<XmppXElement> SendAsync(XmppXElement el, Func<XmppXElement, bool> predicate, int timeout, CancellationToken cancellationToken)
        {
            return await XmppStanzaHandler.SendAsync<XmppXElement>(el, predicate, timeout, cancellationToken);
        }
        #endregion

        #region << SendIqAsync members >>
        public async Task<T> SendIqAsync<T>(Xmpp.Base.Iq iq, int timeout, CancellationToken cancellationToken)
          where T : Xmpp.Base.Iq
        {
            return await XmppStanzaHandler.SendIqAsync<T>(iq, timeout, cancellationToken);
        }
        #endregion

        #endregion

        public async Task<StreamFeatures> ResetStreamAsync(CancellationToken cancellationToken)
        {
            Pipeline.Get<XmlStreamDecoder>().Reset();
            return await SendStreamHeaderAsync(cancellationToken);
        }

        protected async Task<StreamFeatures> SendStreamHeaderAsync()
        {
            return await SendStreamHeaderAsync(XmppStanzaHandler.DefaultTimeout);
        }

        /// <summary>
        /// Sends the XMPP stream header and awaits the reply.
        /// </summary>
        /// <exception cref="StreamErrorException">
        /// Throws a StreamErrorException when the server returns a stream error
        /// </exception>
        /// <returns></returns>
        protected async Task<StreamFeatures> SendStreamHeaderAsync(int timeout)
        {
            return await SendStreamHeaderAsync(timeout, CancellationToken.None);
        }

        protected async Task<StreamFeatures> SendStreamHeaderAsync(CancellationToken cancellationToken)
        {
            return await SendStreamHeaderAsync(XmppStanzaHandler.DefaultTimeout, cancellationToken);
        }

        protected async Task<StreamFeatures> SendStreamHeaderAsync(int timeout, CancellationToken cancellationToken)
        {
            var streamHeader = new Stream
            {
                To = new Jid(XmppDomain),
                Version = "1.0"
            };

            var res = await SendAsync<StreamFeatures, Xmpp.Stream.Error>(streamHeader.StartTag(), timeout, cancellationToken);

            if (res.OfType<StreamFeatures>())
            {
                return res.Cast<StreamFeatures>();
            }                
            else //if (res.OfType<Xmpp.Stream.Error>())
            {
                throw new StreamErrorException(res.Cast<Xmpp.Stream.Error>());
            }                
        }

        /// <summary>
        /// Close the XMPP connection
        /// </summary>
        /// <param name="sendStreamFooter">
        /// Sends the stream footer to the server when set to true.
        /// Usually a stream footer should be sent to the server when closing the connection.
        /// But there are cases where we may not want to sent one. For example with 
        /// stream management when we want to resume the stream later.
        /// </param>
        /// <param name="timeout">the timeout</param>
        /// <returns></returns>
        public async Task<bool> DisconnectAsync(bool sendStreamFooter = true, int timeout = 2000)
        {
            IDisposable anonymousSubscription = null;
            var resultCompletionSource = new TaskCompletionSource<bool>();

            if (sendStreamFooter)
            {
                await SendAsync(new Stream().EndTag());
            }

            anonymousSubscription = XmlStreamEventObserver.Subscribe(
                evt =>
                {
                    if (evt.XmlStreamEventType == XmlStreamEventType.StreamEnd)
                    {
                        anonymousSubscription?.Dispose();
                        resultCompletionSource.SetResult(true);
                    }
                });

            if (resultCompletionSource.Task ==
                await Task.WhenAny(resultCompletionSource.Task, Task.Delay(timeout)))
            {
                await TryCloseAsync();
                return await resultCompletionSource.Task;
            }

            // timed out
            anonymousSubscription.Dispose();
            await TryCloseAsync();

            return true;
        }

        private async Task TryCloseAsync()
        {
            if (Pipeline.Channel.Active)
            {
                await Pipeline.CloseAsync();
            }
        }

        #region << IDisposable implementation >>
        public void Dispose()
        {
            Task.Run(async () =>
            {
                await eventLoopGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
            })
            .Wait();
        }
        #endregion
    }
}
