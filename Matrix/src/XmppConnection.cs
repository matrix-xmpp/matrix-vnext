using System;
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
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Stream;

namespace Matrix
{
    public abstract class XmppConnection
    {        
        protected   Bootstrap                   Bootstrap              = new Bootstrap();
        protected   IChannelPipeline            Pipeline;
        private     MultithreadEventLoopGroup   eventLoopGroup         = new MultithreadEventLoopGroup();
        readonly    XmlStreamDecoder            xmlStreamDecoder       = new XmlStreamDecoder();
        private     XmppStreamEventHandler      xmppStreamEventHandler = new XmppStreamEventHandler();
        private     INameResolver               resolver               = new SrvNameResolver();        

        protected XmppConnection()
        {
            Bootstrap
                .Group(eventLoopGroup)
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Resolver(HostnameResolver)
                
                .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                {
                    Pipeline = channel.Pipeline;

                    Pipeline.AddLast(new LoggingHandler());
                    //Pipeline.AddLast(new XmppLoggingHandler());
                    

                    Pipeline.AddLast(xmlStreamDecoder);
                    Pipeline.AddLast(new XmppXElementEncoder());


                    Pipeline.AddLast(new StringEncoder());

                    //Pipeline.AddLast(xmppStreamEventHandler);

                    

                    Pipeline.AddLast(xmppStreamEventHandler);


                    Pipeline.AddLast(WaitForStanzaHandler);
                    Pipeline.AddLast(IqHandler);
                    
                    Pipeline.AddLast(new DisconnectHandler());

                    
                }));
        }

        #region << Properties >>
        public SessionState SessionState { get; set; } = SessionState.Disconnected;
        public string XmppDomain { get; set; }

        public int Port { get; set; } = 5222;

        public ICertificateValidator CertificateValidator { get; set; } = new DefaultCertificateValidator();

        public IObservable<XmppXElement> XmppXElementStream => xmppStreamEventHandler.XmppXElementStream;

        public IqHandler IqHandler { get; } = new IqHandler();

        public WaitForStanzaHandler WaitForStanzaHandler { get; } = new WaitForStanzaHandler();

        public INameResolver HostnameResolver { get; set; } = new SrvNameResolver();
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
            //anonymousSubscription = XmppXElementStream.Subscribe(
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

                async () =>
                {
                    anonymousSubscription?.Dispose();
                    await Pipeline.CloseAsync();
                    resultCompletionSource.SetResult(true);
                });

            //OnStreamEnd += streamEnd;

            if (resultCompletionSource.Task ==
                await Task.WhenAny(resultCompletionSource.Task, Task.Delay(timeout)))
                return await resultCompletionSource.Task;

            await Pipeline.CloseAsync();

            var ret = await resultCompletionSource.Task;
            anonymousSubscription.Dispose();
            return ret;
        }

    }
}
