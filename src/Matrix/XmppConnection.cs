using System;
using System.Threading.Tasks;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Matrix.Network;
using Matrix.Network.Codecs;
using Matrix.Network.Handlers;
using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Stream;
using System.Threading;

namespace Matrix
{
    public abstract class XmppConnection
    {        
        protected   Bootstrap                   Bootstrap              = new Bootstrap();
        protected   IChannelPipeline            Pipeline;
        readonly    MultithreadEventLoopGroup   eventLoopGroup         = new MultithreadEventLoopGroup();
        readonly    XmlStreamDecoder            xmlStreamDecoder       = new XmlStreamDecoder();
        readonly    XmppStreamEventHandler      xmppStreamEventHandler = new XmppStreamEventHandler();
        private     INameResolver               resolver               = new DefaultNameResolver();

        protected XmppConnection()
        {
            XmlStreamEvent.Subscribe(OnXmlStreamEvent);
       
            Bootstrap
                .Group(eventLoopGroup)
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Option(ChannelOption.SoKeepalive, true)
                .Resolver(HostnameResolver)
                .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                {
                    Pipeline = channel.Pipeline;

                    Pipeline.AddLast(new ZlibDecoder());

                    Pipeline.AddLast(new LoggingHandler());
                    Pipeline.AddLast(new KeepAliveHandler());
                    //Pipeline.AddLast(new XmppLoggingHandler());

                    
                    Pipeline.AddLast(xmlStreamDecoder);
                    
                    //Pipeline.AddLast(new StringEncoder());
                    Pipeline.AddLast(new ZlibEncoder());
                    Pipeline.AddLast(new XmppXElementEncoder());
                    Pipeline.AddLast(new UTF8StringEncoder());

                    Pipeline.AddLast(new AutoReplyToPingHandler<Iq>());

                    Pipeline.AddLast(xmppStreamEventHandler);
                    Pipeline.AddLast(new StreamFooterHandler());
                    //Pipeline.AddLast(xmppStreamEventHandler);

                    Pipeline.AddLast(xmppStanzaHandler);

                    Pipeline.AddLast(CatchAllXmppStanzaHandler.Name, new CatchAllXmppStanzaHandler());
                    //AddHandler(new AutoReplyToPingHandler<Iq>());

                    Pipeline.AddLast(new DisconnectHandler());

                    //ChannelInitializer?.Initialize(channel.Pipeline);
                }));
        }

        private void OnXmlStreamEvent(XmlStreamEvent xmlStreamEvent)
        {
            //throw new NotImplementedException();
        }

        #region << Properties >>
        public SessionState SessionState { get; set; } = SessionState.Disconnected;
        public string XmppDomain { get; set; }

        public int Port { get; set; } = 5222;

        public ICertificateValidator CertificateValidator { get; set; } = new DefaultCertificateValidator();

        public IObservable<XmppXElement> XmppXElementStream => xmppStreamEventHandler.XmppXElementStream;

        private IObservable<XmlStreamEvent> XmlStreamEvent => xmppStreamEventHandler.XmlStreamEvent;

        private readonly XmppStanzaHandler xmppStanzaHandler = new XmppStanzaHandler();
        
        public INameResolver HostnameResolver
        {
            get { return resolver; }
            set
            {
                resolver = value;
                Bootstrap.Resolver(resolver);
            }
        }

        //public IChannelInitializer ChannelInitializer { get; set; }
        #endregion

        #region << Send members >>
        protected async Task<T> SendAsync<T>(string s, int timeout = XmppStanzaHandler.DefaultTimeout)
           where T : XmppXElement
        {
            
            return await xmppStanzaHandler.SendAsync<T>(s, timeout);
        }

        protected async Task<XmppXElement> SendAsync<T1, T2>(string s, int timeout = XmppStanzaHandler.DefaultTimeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
        {

            return await xmppStanzaHandler.SendAsync<T1, T2>(s, timeout);
        }

        protected async Task<XmppXElement> SendAsync<T1, T2>(string s, CancellationToken cancellationToken, int timeout = XmppStanzaHandler.DefaultTimeout)
           where T1 : XmppXElement
           where T2 : XmppXElement
        {

            return await xmppStanzaHandler.SendAsync<T1, T2>(s, cancellationToken, timeout);
        }

        public async Task SendAsync(XmppXElement el)
        {
            await Pipeline.WriteAndFlushAsync(el.ToString(false));
        }

        public async Task<T> SendAsync<T>(XmppXElement el, int timeout = XmppStanzaHandler.DefaultTimeout)
             where T : XmppXElement
        {
            return await xmppStanzaHandler.SendAsync<T>(el, timeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el, int timeout = XmppStanzaHandler.DefaultTimeout)
           where T1 : XmppXElement
           where T2 : XmppXElement
        {
            return await xmppStanzaHandler.SendAsync<T1, T2>(el, timeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el, CancellationToken cancellationToken, int timeout = XmppStanzaHandler.DefaultTimeout)
          where T1 : XmppXElement
          where T2 : XmppXElement
        {
            return await xmppStanzaHandler.SendAsync<T1, T2>(el, cancellationToken, timeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el, int timeout = XmppStanzaHandler.DefaultTimeout)
          where T1 : XmppXElement
          where T2 : XmppXElement
          where T3 : XmppXElement
        {
            return await xmppStanzaHandler.SendAsync<T1, T2, T3>(el, timeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el, CancellationToken cancellationToken, int timeout = XmppStanzaHandler.DefaultTimeout)
         where T1 : XmppXElement
         where T2 : XmppXElement
         where T3 : XmppXElement
        {
            return await xmppStanzaHandler.SendAsync<T1, T2, T3>(el, cancellationToken, timeout);
        }
        #endregion

        protected async Task SendAsync(string s)
        {
            await Pipeline.WriteAndFlushAsync(s);            
        }

        public async Task<StreamFeatures> ResetStreamAsync(CancellationToken cancellationToken)
        {
            xmlStreamDecoder.Reset();
            return await SendStreamHeaderAsync(cancellationToken);
        }

        /// <summary>
        /// Sends the XMPP stream header and awaits the reply.
        /// </summary>
        /// <exception cref="StreamErrorException">
        /// Throws a StreamErrorException when the server returns a stream error
        /// </exception>
        /// <returns></returns>
        protected async Task<StreamFeatures> SendStreamHeaderAsync(int timeout = XmppStanzaHandler.DefaultTimeout)
        {
            return await SendStreamHeaderAsync(CancellationToken.None, timeout);
        }

        protected async Task<StreamFeatures> SendStreamHeaderAsync(CancellationToken cancellationToken, int timeout = XmppStanzaHandler.DefaultTimeout)
        {
            var streamHeader = new Stream
            {
                To = new Jid(XmppDomain),
                Version = "1.0"
            };

            var res = await SendAsync<StreamFeatures, Xmpp.Stream.Error>(streamHeader.StartTag(), timeout);

            if (res.OfType<StreamFeatures>())
                return res.Cast<StreamFeatures>();
            else //if (res.OfType<Xmpp.Stream.Error>())
                throw new StreamErrorException(res.Cast<Xmpp.Stream.Error>());
        }


        public async Task<bool> CloseAsync(int timeout = 2000)
        {
            IDisposable anonymousSubscription = null;
            var resultCompletionSource = new TaskCompletionSource<bool>();
            
            await SendAsync(new Stream().EndTag());

            anonymousSubscription = XmppXElementStream.Subscribe(
                v => { },

                () =>
                {
                    anonymousSubscription?.Dispose();
                    ////if (Pipeline.Channel.Open)
                    ////    await Pipeline.CloseAsync();
                    resultCompletionSource.SetResult(true);
                });

           
            if (resultCompletionSource.Task ==
                await Task.WhenAny(resultCompletionSource.Task, Task.Delay(timeout)))
                return await resultCompletionSource.Task;

            // timed out
            anonymousSubscription.Dispose();
            if (Pipeline.Channel.Active)
                await Pipeline.CloseAsync();

            return true;
        }
    }
}
