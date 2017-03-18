using System;
using System.Net.Security;
using System.Threading.Tasks;
using DotNetty.Handlers.Tls;
using DotNetty.Transport.Channels;
using Matrix.Network.Codecs;
using Matrix.Network.Handlers;
using Matrix.Sasl;
using Matrix.Xml;
using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Compression;
using Matrix.Xmpp.Sasl;
using Matrix.Xmpp.Stream;
using Matrix.Xmpp.Tls;
using System.Threading;

namespace Matrix
{
    /// <summary>
    /// Handles XMPP client connections
    /// </summary>
    public class XmppClient : XmppConnection
    {
        public XmppClient() 
            : this(null)
        {
        }

        public XmppClient(Action<IChannelPipeline> pipelineInitializerAction)
            :base(pipelineInitializerAction)
        {
        }

        private int priority;
        private string resource = "MatriX";
        #region << Properties >>

        public string Username { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the resource identifier.
        /// </summary>
        public string Resource
        {
            get { return resource; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null, $"{nameof(Resource)} cannot be null");
                resource = value;
            }
        }

        public bool Tls { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether <see href="https://xmpp.org/extensions/xep-0138.html">XEP-0138: Stream Compression</see> should be used
        /// on this <see cref="XmppClient" />.
        /// </summary>
        /// <value>
        /// <c>true</c> if compression; otherwise, <c>false</c>.
        /// </value>
        public bool Compression { get; set; } = true;

        /// <summary>
        /// The OPTIONAL show element contains non-human-readable XML character data that specifies the particular availability
        /// status of an entity or specific resource.
        /// </summary>    
        public Show Show { get; set; } = Show.None;

        /// <summary>
        /// The OPTIONAL status contains a natural-language description of availability status. 
        /// It is normally used in conjunction with the show element to provide a detailed description of an availability state 
        /// (e.g., "In a meeting").
        /// </summary>
        public string Status { get; set; } = "online";

        /// <summary>
        /// The priority level of the resource. The value MUST be an integer between -128 and +127. 
        /// If no priority is provided, a server SHOULD consider the priority to be zero.         
        /// </summary>
        /// <remarks>
        /// For information regarding the semantics of priority values in stanza routing 
        /// within instant messaging and presence applications, refer to Server Rules 
        /// for Handling XML StanzasServer Rules for Handling XML Stanzas.
        /// </remarks>
        public int Priority
        {
            get { return priority; }
            set
            {
                Contract.Requires<ArgumentException>(value.IsInRange(-127, 127), "The value must be an integer between - 128 and + 127.");
                priority = value;
            }
        }

        public IAuthenticate SalsHandler { get; set; } = new DefaultSaslHandler();
        #endregion

        /// <summary>
        /// Connect to the XMPP server.
        /// This establishes the connection to teh server, including TLS, authentication, resource binding and
        /// compression.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="AuthenticationException">Thrown when the authentication fails.</exception>
        /// <exception cref="BindException">Thrown when resource binding fails.</exception>
        /// <exception cref="StreamErrorException">Throws a StreamErrorException when the server returns a stream error.</exception>
        /// <exception cref="CompressionException">Throwws a CompressionException when establishing stream compression fails.</exception>
        public async Task<IChannel> ConnectAsync()
        {
            return await ConnectAsync(CancellationToken.None);
        }

        /// <summary>
        /// Connect to the XMPP server.
        /// This establishes the connection to teh server, including TLS, authentication, resource binding and
        /// compression.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="AuthenticationException">Thrown when the authentication fails.</exception>
        /// <exception cref="BindException">Thrown when resource binding fails.</exception>
        /// <exception cref="StreamErrorException">Throws a StreamErrorException when the server returns a stream error.</exception>
        /// /// <exception cref="CompressionException">Throwws a CompressionException when establishing stream compression fails.</exception>
        public async Task<IChannel> ConnectAsync(CancellationToken cancellationToken)
        {
            var iChannel = await Bootstrap.ConnectAsync(XmppDomain, Port);
            XmppSessionState.Value = SessionState.Connected;
            
            var feat = await SendStreamHeaderAsync(cancellationToken);
            await HandleStreamFeaturesAsync(feat, cancellationToken);
            return iChannel;
        }

        private async Task HandleStreamFeaturesAsync(StreamFeatures features, CancellationToken cancellationToken)
        {
            if (XmppSessionState.Value < SessionState.Securing && features.SupportsStartTls && Tls)
            {
                await HandleStreamFeaturesAsync(await DoStartTlsAsync(cancellationToken), cancellationToken);
            }
            else if (XmppSessionState.Value < SessionState.Authenticating)
            {
                var authRet = await DoAuthenicateAsync(features.Mechanisms, cancellationToken);
                await HandleStreamFeaturesAsync(authRet, cancellationToken);
            }
            else if (XmppSessionState.Value < SessionState.Compressing && features.SupportsZlibCompression && Compression)
            {
                await HandleStreamFeaturesAsync(await DoEnableCompressionAsync(cancellationToken), cancellationToken);
            }
            else if (XmppSessionState.Value < SessionState.Binding)
            {
                await DoBindAsync(features, cancellationToken);
            }
        }

        private async Task<StreamFeatures> DoStartTlsAsync(CancellationToken cancellationToken)
        {
            XmppSessionState.Value = SessionState.Securing;
            var tlsHandler =
                new TlsHandler(stream
                => new SslStream(stream,
                true,
                (sender, certificate, chain, errors) => CertificateValidator.RemoteCertificateValidationCallback(sender, certificate, chain, errors)),
                new ClientTlsSettings(XmppDomain));

            await SendAsync<Proceed>(new StartTls(), cancellationToken);
            Pipeline.AddFirst(tlsHandler);
            var streamFeatures = await ResetStreamAsync(cancellationToken);
            XmppSessionState.Value = SessionState.Secure;

            return streamFeatures;
        }

        private async Task<StreamFeatures> DoAuthenicateAsync(Mechanisms mechanisms, CancellationToken cancellationToken)
        {
            XmppSessionState.Value = SessionState.Authenticating;
            var res = await SalsHandler.AuthenticateAsync(mechanisms, this, cancellationToken);

            if (res is Success)
            {
                XmppSessionState.Value = SessionState.Authenticated;
                return await ResetStreamAsync(cancellationToken);
            }
            else //if (res is Failure)
            {
                throw new AuthenticationException(res);
            }
        }

        private async Task<Iq> DoBindAsync(StreamFeatures features, CancellationToken cancellationToken)
        {
            XmppSessionState.Value = SessionState.Binding;

            var bIq = new BindIq { Type = IqType.Set, Bind = { Resource = Resource } };
            var resBindIq = await SendIqAsync(bIq, cancellationToken);

            if (resBindIq.Type != IqType.Result)
                throw new BindException(resBindIq);

            if (features.SupportsSession && !features.Session.Optional)
            {
                var sessionIq = new SessionIq { Type = IqType.Set };
                var resSessionIq = await SendIqAsync(sessionIq, cancellationToken);
            }

            XmppSessionState.Value = SessionState.Binded;

            return resBindIq;
        }

        private async Task<StreamFeatures> DoEnableCompressionAsync(CancellationToken cancellationToken)
        {
            XmppSessionState.Value = SessionState.Compressing;

            var ret = await SendAsync<Compresed, Xmpp.Compression.Failure>(new Compress(Methods.Zlib), cancellationToken);
            if (ret.OfType<Compresed>())
            {
                Pipeline.Get<ZlibEncoder>().Active = true;
                Pipeline.Get<ZlibDecoder>().Active = true;

                var streamFeatures = await ResetStreamAsync(cancellationToken);
                XmppSessionState.Value = SessionState.Compressed;
                return streamFeatures;
            }
            else if(ret.OfType<Xmpp.Compression.Failure>())
            {
                throw new CompressionException(ret.Cast<Xmpp.Compression.Failure>());               
            }

            throw new XmppException(ret);
        }

        public async Task<Iq> RequestRosterAsync(string version = null)
        {
            var riq = new RosterIq()
            {
                Type = IqType.Get
            };

            if (version != null)
                riq.Roster.Version = version;

            var resIq = await SendIqAsync(riq);
            return resIq as Iq;
        }

        #region << Send iq >>
        public async Task<Iq> SendIqAsync(Iq iq)
        {
            return await SendIqAsync(iq, XmppStanzaHandler.DefaultTimeout);
        }

        public async Task<Iq> SendIqAsync(Iq iq, int timeout)
        {
            return await SendIqAsync(iq, timeout, CancellationToken.None);
        }

        public async Task<Iq> SendIqAsync(Iq iq, CancellationToken cancellationToken)
        {
            return await SendIqAsync(iq, XmppStanzaHandler.DefaultTimeout, cancellationToken);
        }

        public async Task<Iq> SendIqAsync(Iq iq, int timeout, CancellationToken cancellationToken)
        {
            Contract.Requires<ArgumentNullException>(iq != null, $"{nameof(iq)} cannot be null");

            return await SendAsync<Iq>(iq, timeout, cancellationToken);
        }
        #endregion

        #region << SendPresence >>
        public async Task SendPresenceAsync(Presence pres)
        {
            Contract.Requires<ArgumentNullException>(pres != null, $"{nameof(pres)} cannot be null");

            await SendAsync(pres);
        }

        public async Task SendPresenceAsync(Show show)
        {
            Show = show;
            await SendPresenceAsync();
        }

        public async Task SendPresenceAsync(Show show, string status)
        {
            Show = show;
            Status = status;

            await SendPresenceAsync();
        }

        public async Task SendPresenceAsync(Show show, string status, int priority)
        {
            Show = show;
            Status = status;
            Priority = priority;

            await SendPresenceAsync();
        }

        public async Task SendPresenceAsync()
        {
            await SendAsync(new Presence
            {
                Show = Show,
                Status = Status,
                Priority = Priority
            });
        }
        #endregion
    }
}
