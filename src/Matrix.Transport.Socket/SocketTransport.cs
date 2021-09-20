namespace Matrix.Transport.Socket
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Security;
    using System.Net.Sockets;
    using System.Reactive.Subjects;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Srv;
    using Xml;
    using Xml.Parser;

    public class SocketTransport : ITransport
    {
        // subjects
        private readonly ISubject<XmppXElement> xmlReceivedSubject = new Subject<XmppXElement>();
        private readonly ISubject<XmppXElement> beforeXmlSentSubject = new Subject<XmppXElement>();
        private readonly ISubject<XmppXElement> xmlSentSubject = new Subject<XmppXElement>();
        private readonly ISubject<byte[]> dataReceivedSubject = new Subject<byte[]>();
        private readonly ISubject<byte[]> dataSentSubject = new Subject<byte[]>();

        private TransportState TransportStateSubject { get; } = new TransportState();

        private bool streamFooterSent;
        private bool streamHeaderSent;

        private TcpClient tcpClient;
        private Stream networkStream;
        private bool isSecure;
        private readonly StreamParser streamParser = new StreamParser();

        private const string Whitespace = " ";
        private Timer keepAliveTimer;
        private int KeepAliveInterval = TimeConstants.FifteenSeconds;

        public SocketTransport()
        {
            HandleXmlStream();
            HandleKeepAlives();

            XmlSent.Subscribe(el =>
            {
                if (el.OfType<Xmpp.Client.Stream>())
                {
                    if (el.Cast<Xmpp.Base.Stream>().IsStartTag)
                    {
                        streamHeaderSent = true;
                    }
                    else if (el.Cast<Xmpp.Base.Stream>().IsEndTag)
                    {
                        TransportStateSubject.Value = State.StreamFooterSent;
                        streamFooterSent = true;
                    }
                }
            });
        }

        private void HandleXmlStream()
        {
            XmppXElement streamHeader = null;
            streamParser.OnStreamStart += (el) =>
            {
                streamHeader = el;
                el.IsStartTag = true;
                xmlReceivedSubject.OnNext(el);
            };

            streamParser.OnStreamElement += (el) =>
            {
                xmlReceivedSubject.OnNext(el);
            };

            streamParser.OnStreamEnd += async () =>
            {
                streamHeader.IsEndTag = true;
                xmlReceivedSubject.OnNext(streamHeader);

                TransportStateSubject.Value = State.StreamFooterReceived;
                if (!streamFooterSent)
                {
                    await this.SendAsync(this.GetStreamFooter()).ConfigureAwait(false);
                }
            };

            streamParser.OnStreamError += (ex) =>
            {
                xmlReceivedSubject.OnError(ex);
            };
        }

        private void HandleKeepAlives()
        {
            StateChanged
                .Subscribe(st =>
                {
                    if (st == State.Disconnected)
                    {
                        if (keepAliveTimer != null)
                        {
                            keepAliveTimer.Dispose();
                            keepAliveTimer = null;
                        }
                    }

                    if (st == State.Connected)
                    {
                        keepAliveTimer = new Timer(
                            async _ =>
                            {
                                try
                                {
                                    await SendAsync(Whitespace).ConfigureAwait(false);
                                }
                                catch (Exception)
                                {
                                    // ignore
                                }
                            },
                            null,
                            KeepAliveInterval,
                            KeepAliveInterval);
                    }
                });

            XmlSent
                .Subscribe(el =>
                {
                    if (KeepAliveInterval > 0)
                    {
                        keepAliveTimer?.Change(KeepAliveInterval, KeepAliveInterval);
                    }
                }

                );
        }

        // observers
        public IObservable<XmppXElement> XmlReceived => xmlReceivedSubject;
        public IObservable<XmppXElement> BeforeXmlSent => beforeXmlSentSubject;
        public IObservable<XmppXElement> XmlSent => xmlSentSubject;
        public IObservable<byte[]> DataReceived => dataReceivedSubject;
        public IObservable<byte[]> DataSent => dataSentSubject;
        public IObservable<State> StateChanged => TransportStateSubject.ValueChanged;

        public IResolver Resolver { get; set; } = new SocketUriResolver();
        public bool SupportsStartTls => false;

        public ICertificateValidator CertificateValidator { get; set; } = new DefaultCertificateValidator();

        public XmppXElement GetStreamHeader(string to, string version) => new Xmpp.Client.Stream { To = to, Version = version, IsStartTag = true };
        public XmppXElement GetStreamFooter() => new Xmpp.Client.Stream { IsEndTag = true };

        public async Task ConnectAsync(string xmppDomain /*, CancellationToken cancellationToken */)
        {
            isSecure = false;
            tcpClient = new TcpClient();
            var uri = await Resolver.ResolveUriAsync(xmppDomain).ConfigureAwait(false);
            var addresses = await Dns.GetHostAddressesAsync(uri.Host).ConfigureAwait(false);

            await tcpClient.ConnectAsync(addresses, uri.Port).ConfigureAwait(false);

            networkStream = tcpClient.GetStream();

            if (uri.Scheme == Schemes.Tcps)
            {
                await InitTls(xmppDomain).ConfigureAwait(false);
            }
            else
            {
                StartReceiverTask();
            }

            TransportStateSubject.Value = State.Connected;
        }

        public async Task DisconnectAsync()
        {
            try
            {
                await Task.Run(() => tcpClient.Client.Disconnect(false)).ConfigureAwait(false);
            }
            catch (Exception)
            {
                // ignore
            }
            finally
            {
                TransportStateSubject.Value = State.Disconnected;
                networkStream?.Dispose();
                tcpClient?.Dispose();
            }
        }

        /// <summary>
        /// Send a message on the websocket.
        /// This method assumes you've already connected via ConnectAsync
        /// </summary>
        /// <param name="xmppXElement">the data to send</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="InvalidOperationException">Throws when the underlying TcpClient is not connected to a server.</exception>
        public async Task SendAsync(XmppXElement xmppXElement, CancellationToken cancellationToken)
        {
            if (!tcpClient.Client.Connected)
            {
                throw new InvalidOperationException("Cannot send data when the socket client is not connected.");
            }

            if (xmppXElement.OfType<Xmpp.Client.Stream>()
                && xmppXElement.Cast<Xmpp.Base.Stream>().IsStartTag
                && streamHeaderSent)
            {
                streamParser.Reset();
            }

            beforeXmlSentSubject.OnNext(xmppXElement);

            var bytes = Encoding.UTF8.GetBytes(xmppXElement.ToString());            

            await networkStream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);

            dataSentSubject.OnNext(bytes);
            xmlSentSubject.OnNext(xmppXElement);
        }

        /// <summary>
        /// Send data on the websocket.
        /// This method assumes you've already connected via ConnectAsync
        /// </summary>
        /// <param name="data">the data to send</param>
        /// <returns></returns>
        private async Task SendAsync(string data)
        {
            await SendAsync(data, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Send data on the websocket.
        /// This method assumes you've already connected via ConnectAsync
        /// </summary>
        /// <param name="data">the data to send</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task SendAsync(string data, CancellationToken cancellationToken)
        {
            if (!tcpClient.Client.Connected)
            {
                throw new InvalidOperationException("Cannot send data when the socket client is not connected.");
            }

            var bytes = Encoding.UTF8.GetBytes(data);
            await networkStream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
            dataSentSubject.OnNext(bytes);
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

        private void StartReceiverTask()
        {
            _ = Task.Run(this.Receive);
        }

        private async Task Receive(/*CancellationToken cancellationToken*/)
        {
            bool cancelRead = false;
            var bytes = new byte[1024 * 2];
            //while (!cancellationToken.IsCancellationRequested)
            while (!cancelRead)
            {
                try
                {
                    var bytesRead = await networkStream.ReadAsync(bytes, 0, bytes.Length).ConfigureAwait(false);

                    if (bytesRead == 0)
                    {
                        TransportStateSubject.Value = State.Disconnected;
                        break;
                    }

                    if (!isSecure)
                    {
                        var text = Encoding.UTF8.GetString(bytes, 0, bytesRead);
                        if (text.StartsWith("<proceed"))
                        {
                            cancelRead = true;
                        }
                    }

                    dataReceivedSubject.OnNext(bytes.Take(bytesRead).ToArray());
                    streamParser.Write(bytes, 0, bytesRead);

                }
                catch (IOException)
                {
                    /*
                     * exception occurs usually when the underlying socket was
                     * closed or aborted remotely or by the host.
                     * Inner exception could tell us more about the reason. For now
                     * we don't care and forward the reason, because the outcome is always the same.
                     */
                    TransportStateSubject.Value = State.Disconnected;
                    break;
                }
            }
        }

        public async Task InitTls(string xmppDomain)
        {
            /*
            // new NET5 APIs, we can use this instead when netstandard 2.0 gets retired
            networkStream = new SslStream(networkStream, true);
            await ((SslStream)networkStream).AuthenticateAsClientAsync(
                new SslClientAuthenticationOptions()
                {
                    RemoteCertificateValidationCallback = CertificateValidator.RemoteCertificateValidationCallback,
                    TargetHost = xmppDomain
                },
               default
            ).ConfigureAwait(false);
            */


            networkStream = new SslStream(
              networkStream,
              true,
              CertificateValidator.RemoteCertificateValidationCallback,
              null
           );

            await ((SslStream)networkStream).AuthenticateAsClientAsync(
                xmppDomain,
                null,
                System.Security.Authentication.SslProtocols.None,
                true
            ).ConfigureAwait(false);

            isSecure = true;

            StartReceiverTask();
        }

        /*
        // once everything is on NET5 we can replace all keepalive logic with this
        public Action<Socket> SocketConfiguration { get; set; } = socket =>
        {
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
#if !NETSTANDARD2_1
            socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.TcpKeepAliveTime, 10);
            socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.TcpKeepAliveInterval, 5);
            socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.TcpKeepAliveRetryCount, 2);
#endif
        };
        */

        }
    }
