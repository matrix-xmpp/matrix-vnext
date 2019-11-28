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
using System.Reactive.Linq;
using System.Threading;
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
using Matrix.Xmpp.Register;
using Matrix.Xmpp.Sasl;
using Matrix.Xmpp.Stream;
using Matrix.Xmpp.Tls;
using Matrix.Network;

namespace Matrix
{
    /// <summary>
    /// Handles XMPP client connections
    /// </summary>
    public class XmppClient : XmppConnection, IClientIqSender
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmppClient"/> class.
        /// </summary>
        public XmppClient()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppClient"/> class.
        /// </summary>
        /// <param name="pipelineInitializerAction">The pipeline initializer action.</param>
        public XmppClient(Action<IChannelPipeline, ISession> pipelineInitializerAction)
            : base(pipelineInitializerAction)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppClient"/> class.
        /// </summary>
        /// <param name="pipelineInitializerAction">The pipeline initializer action.</param>
        /// <param name="eventLoopGroup">The event loop group.</param>
        public XmppClient(Action<IChannelPipeline, ISession> pipelineInitializerAction, IEventLoopGroup eventLoopGroup)
           : base(pipelineInitializerAction, eventLoopGroup)
        {
        }

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
        /// Gets or sets the <see cref="ITlsSettingsProvider"/>.
        /// By setting a custom provider default settings can be customized.
        /// </summary>
        public ITlsSettingsProvider TlsSettingsProvider { get; set; } = new DefaultClientTlsSettingsProvider();


        /// <summary>
        /// Gets or sets the <see cref="ITlsHandlerProvider"/>
        /// This allows to use customs Tls handler implementations.
        /// </summary>
        public ITlsHandlerProvider TlsHandlerProvider { get; set; } = new DefaultClientTlsHandlerProvider();

        /// <summary>
        /// Gets or sets a value indicating whether <see href="https://xmpp.org/extensions/xep-0138.html">XEP-0138: Stream Compression</see> should be used
        /// on this <see cref="XmppClient" />.
        /// </summary>
        /// <value>
        /// <c>true</c> if compression; otherwise, <c>false</c>.
        /// </value>
        public bool Compression { get; set; } = true;      

        public IAuthenticate SaslHandler { get; set; } = new DefaultSaslHandler();

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
        public override async Task<IChannel> ConnectAsync()
        {
            return await ConnectAsync(CancellationToken.None);
        }

        /// <summary>
        /// Connect to the XMPP server.
        /// This establishes the connection to the server, including TLS, authentication, resource binding and
        /// compression.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="AuthenticationException">Thrown when the authentication fails.</exception>
        /// <exception cref="BindException">Thrown when resource binding fails.</exception>
        /// <exception cref="StreamErrorException">Throws a StreamErrorException when the server returns a stream error.</exception>
        /// <exception cref="CompressionException">Throws a CompressionException when establishing stream compression fails.</exception>
        /// <exception cref="RegisterException">Throws aRegisterException when new account registration fails.</exception>
        public override async Task<IChannel> ConnectAsync(CancellationToken cancellationToken)
        {
            var iChannel = await Bootstrap.ConnectAsync(XmppDomain, Port);
            XmppSessionState.Value = SessionState.Connected;

            if (HostnameResolver.Implements<IDirectTls>() 
                && HostnameResolver.Cast<IDirectTls>().DirectTls == true)
            {
                await DoSslAsync(cancellationToken);
            }

            var feat = await SendStreamHeaderAsync(cancellationToken);
            await HandleStreamFeaturesAsync(feat, cancellationToken);
            return iChannel;
        }

        internal async Task HandleStreamFeaturesAsync(
            StreamFeatures features, 
            CancellationToken cancellationToken)
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
            else if (XmppSessionState.Value < SessionState.Binding
                && Pipeline.Contains<StreamManagementHandler>()
                && Pipeline.Get<StreamManagementHandler>().CanResume
                && Pipeline.Get<StreamManagementHandler>().StreamId != null)
            {
                await Pipeline.Get<StreamManagementHandler>().ResumeAsync(cancellationToken);
                await HandleStreamFeaturesAsync(features, cancellationToken);
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
            
            var tlsHandler = await TlsHandlerProvider.ProvideAsync(this);
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
            await Task.Run(async () =>
            {
                var tlsHandler = await TlsHandlerProvider.ProvideAsync(this);
                XmppSessionState.Value = SessionState.Securing;

                Pipeline.AddFirst(tlsHandler);
                XmppSessionState.Value = SessionState.Secure;
            }, cancellationToken);
        }

        private async Task<StreamFeatures> DoAuthenicateAsync(Mechanisms mechanisms, CancellationToken cancellationToken)
        {
            XmppSessionState.Value = SessionState.Authenticating;
            var res = await SaslHandler.AuthenticateAsync(mechanisms, this, cancellationToken);

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
    }
}
