using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Matrix.Core;
using Matrix.Network;
using Matrix.Network.Codecs;
using Matrix.Network.Handlers;
using Matrix.Network.Resolver;
using Matrix.Xml;
using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Ping;
using Matrix.Xmpp.Stream;

namespace Matrix
{
    public abstract class XmppConnection
    {        
        protected   Bootstrap                   Bootstrap              = new Bootstrap();
        protected   IChannelPipeline            Pipeline;
        readonly    MultithreadEventLoopGroup   eventLoopGroup         = new MultithreadEventLoopGroup();
        readonly    XmlStreamDecoder            xmlStreamDecoder       = new XmlStreamDecoder();
        readonly    XmppStreamEventHandler      xmppStreamEventHandler = new XmppStreamEventHandler();
        private     INameResolver               resolver               = new SrvNameResolver();        

        protected XmppConnection()
        {
            XmlStreamEvent.Subscribe(OnXmlStreamEvent);

            //XmppXElementStream
            //    .Where(el => el is Iq)
            //    .Cast<Iq>()
            //    .Where(iq => iq.Query is Ping && iq.Type == IqType.Get)
            //    .Subscribe(async iq => await SendAsync(new Iq { Type = IqType.Result, Id = iq.Id, To = iq.From }));
                

            Bootstrap
                .Group(eventLoopGroup)
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Option(ChannelOption.SoKeepalive, true)
                .Resolver(HostnameResolver)
                .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                {
                    Pipeline = channel.Pipeline;

                    Pipeline.AddLast(new LoggingHandler());
                    Pipeline.AddLast(new KeepAliveHandler());
                    //Pipeline.AddLast(new XmppLoggingHandler());
                    

                    Pipeline.AddLast(xmlStreamDecoder);
                    Pipeline.AddLast(new XmppXElementEncoder());


                    Pipeline.AddLast(new StringEncoder());

                    //Pipeline.AddLast(xmppStreamEventHandler);

                    Pipeline.AddLast(new AutoReplyToPingHandler<Iq>());


                    Pipeline.AddLast(new StreamFooterHandler());
                    Pipeline.AddLast(xmppStreamEventHandler);
                    
                    
                    


                    Pipeline.AddLast(WaitForStanzaHandler);
                    Pipeline.AddLast(IqHandler);
                    
                    Pipeline.AddLast(new DisconnectHandler());

                    
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

        public IqHandler IqHandler { get; } = new IqHandler();

        public WaitForStanzaHandler WaitForStanzaHandler { get; } = new WaitForStanzaHandler();

        
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

        public async Task SendAsync(XmppXElement el)
        {
            await Pipeline.WriteAndFlushAsync(el.ToString(false));
        }

        internal async Task SendAsync(string s)
        {
            await Pipeline.WriteAndFlushAsync(s);
        }

        public async Task<StreamFeatures> ResetStreamAsync()
        {
            xmlStreamDecoder.Reset();
            return await SendStreamHeaderAsync();
        }

        public async Task<StreamFeatures> SendStreamHeaderAsync()
        {
            var streamHeader = new Stream
            {
                To = new Jid(XmppDomain),
                Version = "1.0"
            };

            return await WaitForStanzaHandler.SendAsync<StreamFeatures>(streamHeader.StartTag());
        }


        public async Task<bool> CloseAsync(int timeout = 2000)
        {
            var resultCompletionSource = new TaskCompletionSource<bool>();


            IDisposable anonymousSubscription = null;
            //anonymousSubscription = XmppXElementStreamStream.Subscribe(
            //       v => { },
            //       //In this example, the OnCompleted callback is also provided
            //    async () =>
            //    {
            //        await pipeline.CloseAsync();
            //        anonymousSubscription.Dispose();
            //        resultCompletionSource.SetResult(true);
            //    });


            //EventHandler<EventArgs> streamEnd = null;
            //streamEnd = async (sender, args) =>
            //{
            //    await pipeline.CloseAsync();
            //    OnStreamEnd -= streamEnd;
            //    resultCompletionSource.SetResult(true);
            //};

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
