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
using Matrix.Xmpp.Register;
using Matrix.Network;

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
            : base(pipelineInitializerAction)
        {
        }

        private int priority;
        private string resource = "MatriX";

        #region << Properties >>
        /// <summary>
        /// Gets or sets the username for the XMPP connection.
        /// </summary>
        public string Username { get; set; }


        /// <summary>
        /// Gets or sets the password for the XMPP connection.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether the stream should be secured over TLS or not when supported and advertised by the server.
        /// </summary>
        public bool Tls { get; set; } = true;

        /// <summary>
        /// Gets or sets weather legacy old jabber style ssl should be used.
        /// </summary>
        public bool LegacySsl { get; set; } = false;

        /// <summary>
        /// Gets or sets the <see cref="ITlsSettingsProvider"/>.
        /// By setting a custom provider default settings can be customized.
        /// </summary>
        public ITlsSettingsProvider TlsSettingsProvider { get; set; } = new DefaultClientTlsSettingsProvider();

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

        public IRegister RegistrationHandler { get; set; } = null;
        #endregion

        /// <summary>
        /// Connect to the XMPP server.
        /// This establishes the connection to the server, including TLS, authentication, resource binding and
        /// compression.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="AuthenticationException">Thrown when the authentication fails.</exception>
        /// <exception cref="BindException">Thrown when resource binding fails.</exception>
        /// <exception cref="StreamErrorException">Throws a StreamErrorException when the server returns a stream error.</exception>
        /// <exception cref="CompressionException">Throws a CompressionException when establishing stream compression fails.</exception>
        /// <exception cref="RegisterException">Throws a RegisterException when new account registration fails.</exception>
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
        /// <exception cref="CompressionException">Throws a CompressionException when establishing stream compression fails.</exception>
        /// <exception cref="RegisterException">Throws aRegisterException when new account registration fails.</exception>
        public async Task<IChannel> ConnectAsync(CancellationToken cancellationToken)
        {
            var iChannel = await Bootstrap.ConnectAsync(XmppDomain, Port);
            XmppSessionState.Value = SessionState.Connected;

            if (LegacySsl)
            {
                await DoSslAsync(cancellationToken);
            }

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
            else if (XmppSessionState.Value < SessionState.Registering && features.SupportsRegistration && RegistrationHandler?.RegisterNewAccount == true)
            {
                await DoRegisterAsync(cancellationToken);
                await HandleStreamFeaturesAsync(features, cancellationToken);
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

        /// <summary>
        /// Handle StartTls asynchronous
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<StreamFeatures> DoStartTlsAsync(CancellationToken cancellationToken)
        {
            XmppSessionState.Value = SessionState.Securing;

            var tlsSettingsProvider = TlsSettingsProvider.ProvideAsync(this).GetAwaiter().GetResult();
            var tlsHandler =
                new TlsHandler(stream
                => new SslStream(stream,
                true,
                (sender, certificate, chain, errors) => CertificateValidator.RemoteCertificateValidationCallback(sender, certificate, chain, errors)),
                tlsSettingsProvider);

            await SendAsync<Proceed>(new StartTls(), cancellationToken);
            Pipeline.AddFirst(tlsHandler);
            var streamFeatures = await ResetStreamAsync(cancellationToken);
            XmppSessionState.Value = SessionState.Secure;

            return streamFeatures;
        }

        /// <summary>
        /// Starts SSL/TLS on a connection. This can be used for old Jabber style SSL.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task DoSslAsync(CancellationToken cancellationToken)
        {
            await Task.Factory.StartNew(() =>
            {
                var tlsSettingsProvider = TlsSettingsProvider.ProvideAsync(this).GetAwaiter().GetResult();
                XmppSessionState.Value = SessionState.Securing;
                var tlsHandler =
                    new TlsHandler(stream
                    => new SslStream(stream,
                    true,
                    (sender, certificate, chain, errors) => CertificateValidator.RemoteCertificateValidationCallback(sender, certificate, chain, errors)),
                    tlsSettingsProvider);

                Pipeline.AddFirst(tlsHandler);
                XmppSessionState.Value = SessionState.Secure;
            }, cancellationToken);
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

        /// <summary>
        /// Registers a new account on the XMPP server.
        /// compression.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="RegisterException">Thrown when the registration fails.</exception>
        private async Task DoRegisterAsync(CancellationToken cancellationToken)
        {
            XmppSessionState.Value = SessionState.Registering;
            var regInfoIqResult =
                await SendIqAsync(
                    new RegisterIq { Type = IqType.Get, To = XmppDomain },
                    cancellationToken);

            if (regInfoIqResult.Type == IqType.Result && regInfoIqResult.Query is Register)
            {
                var regIq = new Iq { Type = IqType.Set, To = new Jid(XmppDomain) };
                regIq.GenerateId();
                regIq.Query = await RegistrationHandler?.RegisterAsync(regInfoIqResult.Query as Register);

                var regResult = await SendIqAsync(regIq, cancellationToken);
                if (regResult.Type == IqType.Result)
                {
                    XmppSessionState.Value = SessionState.Registered;
                    return;
                }
                else
                    throw new RegisterException(regResult);
            }
            throw new RegisterException(regInfoIqResult);
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
            else if (ret.OfType<Xmpp.Compression.Failure>())
            {
                throw new CompressionException(ret.Cast<Xmpp.Compression.Failure>());
            }

            throw new XmppException(ret);
        }

        #region << Request Roster >>
        /// <summary>
        /// Request the roster (contact list) asynchronous from the server.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public async Task<Iq> RequestRosterAsync(string version = null)
        {
            return await RequestRosterAsync(version, XmppStanzaHandler.DefaultTimeout, CancellationToken.None);
        }

        /// <summary>
        /// Request the roster (contact list) asynchronous from the server.
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Iq> RequestRosterAsync(int timeout, CancellationToken cancellationToken)
        {
            return await RequestRosterAsync(null, XmppStanzaHandler.DefaultTimeout, CancellationToken.None);
        }

        /// <summary>
        /// Request the roster (contact list) asynchronous from the server.
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public async Task<Iq> RequestRosterAsync(int timeout)
        {
            return await RequestRosterAsync(null, timeout, CancellationToken.None);
        }

        /// <summary>
        /// Request the roster (contact list) asynchronous from the server.
        /// </summary>
        /// <param name="version"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public async Task<Iq> RequestRosterAsync(string version, int timeout)
        {
            return await RequestRosterAsync(version, timeout, CancellationToken.None);
        }

        /// <summary>
        /// Request the roster (contact list) asynchronous from the server.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Iq> RequestRosterAsync(CancellationToken cancellationToken)
        {
            return await RequestRosterAsync(null, XmppStanzaHandler.DefaultTimeout, cancellationToken);
        }

        /// <summary>
        /// Request the roster (contact list) asynchronous from the server.
        /// </summary>
        /// <param name="version"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Iq> RequestRosterAsync(string version, CancellationToken cancellationToken)
        {
            return await RequestRosterAsync(version, XmppStanzaHandler.DefaultTimeout, cancellationToken);
        }

        /// <summary>
        /// Request the roster (contact list) asynchronous from the server.
        /// </summary>
        /// <param name="version"></param>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Iq> RequestRosterAsync(string version, int timeout, CancellationToken cancellationToken)
        {
            var riq = new RosterIq()
            {
                Type = IqType.Get
            };

            if (version != null)
                riq.Roster.Version = version;

            var resIq = await SendIqAsync(riq, timeout, cancellationToken);
            return resIq as Iq;
        }
        #endregion

        #region << Send iq >>
        /// <summary>
        /// Send an Iq asynchronous to the server
        /// </summary>
        /// <param name="iq"></param>
        /// <returns>The server response Iq</returns>
        public async Task<Iq> SendIqAsync(Iq iq)
        {
            return await SendIqAsync(iq, XmppStanzaHandler.DefaultTimeout);
        }

        /// <summary>
        /// Send an Iq asynchronous to the server
        /// </summary>
        /// <param name="iq"></param>
        /// <param name="timeout"></param>
        /// <returns>The server response Iq</returns>
        public async Task<Iq> SendIqAsync(Iq iq, int timeout)
        {
            return await SendIqAsync(iq, timeout, CancellationToken.None);
        }

        /// <summary>
        /// Send an Iq asynchronous to the server
        /// </summary>
        /// <param name="iq"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The server response Iq</returns>
        public async Task<Iq> SendIqAsync(Iq iq, CancellationToken cancellationToken)
        {
            return await SendIqAsync(iq, XmppStanzaHandler.DefaultTimeout, cancellationToken);
        }

        /// <summary>
        /// Send an Iq asynchronous to the server
        /// </summary>
        /// <param name="iq"></param>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The server response Iq</returns>
        public async Task<Iq> SendIqAsync(Iq iq, int timeout, CancellationToken cancellationToken)
        {
            Contract.Requires<ArgumentNullException>(iq != null, $"{nameof(iq)} cannot be null");

            return await SendIqAsync<Iq>(iq, timeout, cancellationToken);
        }
        #endregion      

        #region << SendPresence >>
        /// <summary>
        /// Send a presence update to the server.
        /// </summary>
        /// <param name="pres">The presence stanza which gets sent to the server.</param>
        /// <returns></returns>
        public async Task SendPresenceAsync(Presence pres)
        {
            Contract.Requires<ArgumentNullException>(pres != null, $"{nameof(pres)} cannot be null");

            await SendAsync(pres);
        }

        /// <summary>
        /// Send a presence update to the server. Use the properies Show, Status and priority to update presence information
        /// </summary>
        /// <param name="show"></param>
        /// <returns></returns>
        public async Task SendPresenceAsync(Show show)
        {
            Show = show;
            await SendPresenceAsync();
        }

        /// <summary>
        /// Send a presence update to the server. Use the properies Show, Status and priority to update presence information
        /// </summary>
        /// <param name="show"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task SendPresenceAsync(Show show, string status)
        {
            Show = show;
            Status = status;

            await SendPresenceAsync();
        }

        /// <summary>
        /// Send a presence update to the server. Use the properies Show, Status and priority to update presence information
        /// </summary>
        /// <param name="show"></param>
        /// <param name="status"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public async Task SendPresenceAsync(Show show, string status, int priority)
        {
            Show = show;
            Status = status;
            Priority = priority;

            await SendPresenceAsync();
        }

        /// <summary>
        /// Send a presence update to the server. Use the properies Show, Status and priority to update presence information
        /// </summary>
        /// <returns></returns>
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
