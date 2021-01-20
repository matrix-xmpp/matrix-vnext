namespace Matrix.Transport.WebSocket
{
    using Matrix.Xml;
    using Matrix.Xmpp.Framing;
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.WebSockets;
    using System.Reactive.Subjects;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class WebSocketTransport : ITransport
    {
        // subjects
        private readonly ISubject<XmppXElement> xmlReceivedSubject = new Subject<XmppXElement>();
        private readonly ISubject<XmppXElement> beforeXmlSentSubject = new Subject<XmppXElement>();
        private readonly ISubject<XmppXElement> xmlSentSubject = new Subject<XmppXElement>();
        private readonly ISubject<byte[]> dataReceivedSubject = new Subject<byte[]>();
        private readonly ISubject<byte[]> dataSentSubject = new Subject<byte[]>();
        
        private TransportState TransportStateSubject { get; } = new TransportState();
        
        // observers
        public IObservable<XmppXElement> XmlReceived => xmlReceivedSubject;
        public IObservable<XmppXElement> BeforeXmlSent => beforeXmlSentSubject;
        public IObservable<XmppXElement> XmlSent => xmlSentSubject;
        public IObservable<byte[]> DataReceived => dataReceivedSubject;
        public IObservable<byte[]> DataSent => dataSentSubject;
        public IObservable<State> StateChanged => TransportStateSubject.ValueChanged;
        
        private readonly CancellationTokenSource receiverCts = new CancellationTokenSource();
        private readonly Func<Uri, CancellationToken, Task<WebSocket>> connectionFactory;
        
        private WebSocket websocket;
        private bool streamFooterSent;

        public WebSocketTransport(Func<Uri, CancellationToken, Task<WebSocket>> connectionFactory = null)
        {
            this.connectionFactory
                = connectionFactory
                  ?? (async (uri, token) =>
                  {
                      var client = new ClientWebSocket();
                      client.Options.AddSubProtocol("xmpp");

                      try
                      {
                          client.Options.KeepAliveInterval = new TimeSpan(0, 0, 0, 15);
                      }
                      catch (PlatformNotSupportedException)
                      {
                          // this is not supported on the Wasm runtime currently
                      }

                      await client.ConnectAsync(uri, token).ConfigureAwait(false);
                      return client;
                  });
        }

        public IResolver Resolver { get; set; } = new WebSocketUriResolver();
        public bool SupportsStartTls => false;

        public XmppXElement GetStreamHeader(string to, string version) => new Open{ To = to, Version = version };
        public XmppXElement GetStreamFooter() => new Close();

        public async Task ConnectAsync(string xmppDomain /*, CancellationToken cancellationToken */)
        {
            var uri = await Resolver.ResolveUriAsync(xmppDomain).ConfigureAwait(false);

            websocket = await connectionFactory(uri, CancellationToken.None).ConfigureAwait(false);

            TransportStateSubject.Value = State.Connected;
            streamFooterSent = false;
            _ = Task.Run(() => this.Receive(receiverCts.Token));
        }

        public async Task DisconnectAsync()
        {
            try
            {
                await websocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None).ConfigureAwait(false);
            }
            catch (Exception)
            {
                // ignore
            }
            finally
            {
                TransportStateSubject.Value = Transport.State.Disconnected;
            }
        }

        public Task InitTls(string xmppDomain)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Send a message on the websocket.
        /// This method assumes you've already connected via ConnectAsync
        /// </summary>
        /// <param name="xmppXElement">The data to send</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task SendAsync(XmppXElement xmppXElement, CancellationToken cancellationToken)
        {
            beforeXmlSentSubject.OnNext(xmppXElement);

            var bytes = Encoding.UTF8.GetBytes(xmppXElement.ToString());
            var buffer = new ArraySegment<byte>(bytes);
            await websocket.SendAsync(buffer, WebSocketMessageType.Text, true, cancellationToken).ConfigureAwait(false);

            // look for stream footer messages for updating the transport state
            if (xmppXElement.OfType<Close>())
            {
                TransportStateSubject.Value = Transport.State.StreamFooterSent;
                streamFooterSent = true;
            }

            dataSentSubject.OnNext(bytes);
            xmlSentSubject.OnNext(xmppXElement);
        }

        /// <summary>
        /// Send a message on the websocket.
        /// This method assumes you've already connected via ConnectAsync
        /// </summary>
        /// <param name="xmppXElement">The Xml element to send</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task SendAsync(XmppXElement xmppXElement)
        {
            await SendAsync(xmppXElement, CancellationToken.None).ConfigureAwait(false);
        }

        private async Task Receive(CancellationToken cancellationToken)
        {
            var buffer = new ArraySegment<byte>(new byte[1024 * 2]);
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    WebSocketReceiveResult result = null;
                    var ms = new MemoryStream();
                    do
                    {
                        result = await websocket.ReceiveAsync(buffer, cancellationToken).ConfigureAwait(false);
                        ms.Write(buffer.Array, buffer.Offset, result.Count);

                        dataReceivedSubject.OnNext(buffer.AsEnumerable().Take(result.Count).ToArray());

                    } while (!result.EndOfMessage);

                    // websocket close message
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        TransportStateSubject.Value = State.Disconnected;
                        break;
                    }

                    ms.Seek(0, SeekOrigin.Begin);
                    var xml = Encoding.UTF8.GetString(ms.ToArray());
                    var el = XmppXElement.LoadXml(xml);

                    xmlReceivedSubject.OnNext(el);

                    // look for stream footer messages for updating the transport state
                    if (el.OfType<Close>())
                    {
                        TransportStateSubject.Value = State.StreamFooterReceived;
                        if (!streamFooterSent)
                        {
                            await this.SendAsync(this.GetStreamFooter(), cancellationToken).ConfigureAwait(false);
                        }
                    }
                }
                catch (WebSocketException)
                {
                    await DisconnectAsync().ConfigureAwait(false);
                    break;
                }
            }
        }
    }
}
