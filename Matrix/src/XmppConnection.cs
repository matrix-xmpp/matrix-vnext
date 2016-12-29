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
using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Stream;

namespace Matrix
{
    public abstract class XmppConnection
    {
        
        protected Bootstrap _bootstrap = new Bootstrap();
        protected IChannelPipeline _pipeline;

        readonly XmlStreamDecoder _xmlStreamDecoder = new XmlStreamDecoder();

        XmppStreamEventHandler _xmppStreamEventHandler = new XmppStreamEventHandler();

        INameResolver _resolver = new SrvNameResolver();

        protected XmppConnection()
        {
            _bootstrap
                .Group(new MultithreadEventLoopGroup())
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Resolver(HostnameResolver)
                
                .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                {
                    _pipeline = channel.Pipeline;


                    _pipeline.AddLast(new LoggingHandler());
                    _pipeline.AddLast(new DisconnectHandler());

                    _pipeline.AddLast(_xmlStreamDecoder);
                    _pipeline.AddLast(new XmppXElementEncoder());


                    _pipeline.AddLast(new StringEncoder());

                    _pipeline.AddLast(_xmppStreamEventHandler);

                    _pipeline.AddLast(IqHandler);
                    _pipeline.AddLast(WaitForStanzaHandler);



                }));
        }

        #region << Properties >>
        public SessionState SessionState { get; set; } = SessionState.Disconnected;
        public string XmppDomain { get; set; }

        public int Port { get; set; } = 5222;

        public ICertificateValidator CertificateValidator { get; set; } = new DefaultCertificateValidator();


        public IObservable<XmppXElement> XmppXElementStream => _xmppStreamEventHandler.XmppXElementStream;

        public IqHandler IqHandler { get; } = new IqHandler();

        public WaitForStanzaHandler WaitForStanzaHandler { get; } = new WaitForStanzaHandler();

        public INameResolver HostnameResolver { get; set; } = new SrvNameResolver();
        #endregion

        public async Task SendAsync(XmppXElement el)
        {
            await _pipeline.WriteAndFlushAsync(el.ToString(false));
        }

        internal async Task SendAsync(string s)
        {
            await _pipeline.WriteAndFlushAsync(s);
        }

        //private async Task StreamReset()
        //{
        //    _xmlStreamDecoder.Reset();
        //    await SendStreamHeader();
        //}

        public async Task<StreamFeatures> ResetStreamAsync()
        {
            _xmlStreamDecoder.Reset();
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


        //public async Task<bool> CloseAsync(int timeout)
        //{
        //    var resultCompletionSource = new TaskCompletionSource<bool>();


        //    IDisposable anonymousSubscription = null;
        //    //anonymousSubscription = XmppXElementStream.Subscribe(
        //    //       v => { },
        //    //       //In this example, the OnCompleted callback is also provided
        //    //    async () =>
        //    //    {
        //    //        await pipeline.CloseAsync();
        //    //        anonymousSubscription.Dispose();
        //    //        resultCompletionSource.SetResult(true);
        //    //    });


        //    //EventHandler<EventArgs> streamEnd = null;
        //    //streamEnd = async (sender, args) =>
        //    //{
        //    //    await pipeline.CloseAsync();
        //    //    OnStreamEnd -= streamEnd;
        //    //    resultCompletionSource.SetResult(true);
        //    //};

        //    await SendAsync(new Stream().EndTag());

        //    anonymousSubscription = XmppXElementStream.Subscribe(
        //        v => { },

        //        async () =>
        //        {
        //            anonymousSubscription?.Dispose();
        //            await _pipeline.CloseAsync();
        //            resultCompletionSource.SetResult(true);
        //        });

        //    //OnStreamEnd += streamEnd;

        //    if (resultCompletionSource.Task ==
        //        await Task.WhenAny(resultCompletionSource.Task, Task.Delay(timeout)))
        //        return await resultCompletionSource.Task;

        //    await _pipeline.CloseAsync();

        //    var ret = await resultCompletionSource.Task;
        //    anonymousSubscription.Dispose();
        //    return ret;
        //}

    }
}
