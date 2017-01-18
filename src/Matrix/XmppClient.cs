using System;
using System.Net.Security;
using System.Security.Authentication;
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

namespace Matrix
{
    /// <summary>
    /// Handles XMPP client connections
    /// </summary>
    public class XmppClient : XmppConnection
    {
        private int priority;
        #region << Properties >>

        public string Username { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the resource identifier.
        /// </summary>
        public string Resource { get; set; } = "MatriX";

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
                if (value < -127 || value > 127)
                    throw new ArgumentOutOfRangeException("The value must be an integer between -128 and +127.");

                priority = value;
            }
        }

        public IAuthenticate SalsHandler { get; set; } = new DefaultSaslHandler();
        #endregion

        public async Task<IChannel> ConnectAsync()
        {
            var iChannel = await Bootstrap.ConnectAsync(XmppDomain, Port);
            var feat = await SendStreamHeaderAsync();
            await HandleStreamFeaturesAsync(feat);
            return iChannel;
        }

        private async Task HandleStreamFeaturesAsync(StreamFeatures features)
        {
            if (SessionState < SessionState.Securing && features.SupportsStartTls && Tls)
            {
                await HandleStreamFeaturesAsync(await DoStartTlsAsync());
            }
            else if (SessionState < SessionState.Authenticating)
            {
                var authRet = await DoAuthenicateAsync(features.Mechanisms);
                await HandleStreamFeaturesAsync(authRet);
            }
            else if (SessionState < SessionState.Compressing && features.SupportsZlibCompression && Compression)
            {
                await HandleStreamFeaturesAsync(await DoEnableCompressionAsync());
            }
            else if (SessionState < SessionState.Binding)
            {
                await DoBindAsync();
            }

        }

        private async Task<StreamFeatures> DoStartTlsAsync()
        {
            SessionState = SessionState.Securing;
            var tlsHandler =
                new TlsHandler(stream
                => new SslStream(stream,
                true,
                (sender, certificate, chain, errors) => CertificateValidator.RemoteCertificateValidationCallback(sender, certificate, chain, errors)),
                new ClientTlsSettings(XmppDomain));

            await SendAsync<Proceed>(new StartTls());
            Pipeline.AddFirst(tlsHandler);
            var streamFeatures = await ResetStreamAsync();
            SessionState = SessionState.Secure;

            return streamFeatures;
        }

        private async Task<StreamFeatures> DoAuthenicateAsync(Mechanisms mechanisms)
        {
            SessionState = SessionState.Authenticating;
            var res = await SalsHandler.AuthenticateAsync(mechanisms, this);

            if (res is Success)
            {
                SessionState = SessionState.Authenticated;
                return await ResetStreamAsync();
            }
            else //if (res is Failure)
            {
                throw new AuthenticationException();
            }
        }

        private async Task<Iq> DoBindAsync()
        {
            SessionState = SessionState.Binding;
            var bIq = new BindIq { Type = IqType.Set, Bind = { Resource = Resource } };
            var resIq = await SendIqAsync(bIq);
            SessionState = SessionState.Binded;
            return resIq as Iq;
        }

        private async Task<StreamFeatures> DoEnableCompressionAsync()
        {
            SessionState = SessionState.Compressing;
            var ret = await SendAsync<Compresed, Xmpp.Compression.Failure>(new Compress(Methods.Zlib));
            if (ret.OfType<Compresed>())
            {
                Pipeline.Get<ZlibEncoder>().Active = true;
                Pipeline.Get<ZlibDecoder>().Active = true;
            }

            var streamFeatures = await ResetStreamAsync();
            SessionState = SessionState.Compressed;
            return streamFeatures;
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
        public async Task<Iq> SendIqAsync(Iq iq, int timeout = XmppStanzaHandler.DefaultTimeout)
        {
            return await SendAsync<Iq>(iq, timeout);
        }
        #endregion

        #region << SendPresence >>
        public async Task SendPresenceAsync(Presence pres)
        {
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
