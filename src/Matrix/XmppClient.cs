namespace Matrix
{
    using Sasl;
    using System;
    using System.Collections.Generic;
    using System.Net.Security;
    using System.Reactive.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xml;
    using Xmpp;
    using Xmpp.Client;
    using Xmpp.Register;
    using Xmpp.Sasl;
    using Xmpp.Stream;
    using Xmpp.Tls;

    /// <summary>
    /// Handles XMPP client connections
    /// </summary>
    public class XmppClient : XmppConnection, IXmppClient
    {
        private readonly List<XmppHandler> handlers = new List<XmppHandler>();

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppClient"/> class.
        /// </summary>
        public XmppClient(Action<Configuration> configurationAction)
            : this(
                configurationAction,
                null
                )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppClient"/> class.
        /// </summary>
        /// <param name="configurationAction"></param>
        /// <param name="handlerInitializerAction"></param>
        public XmppClient(
            Action<Configuration> configurationAction,
            Action<List<XmppHandler>, XmppClient> handlerInitializerAction)
            : base(configurationAction)
        {
            handlers.Add(new DisconnectHandler(this));
            
            if (Configuration.StreamManagement)
            {
                handlers.Add(new StreamManagementHandler(this));
            }

            if (Configuration.AutoReconnect)
            {
                handlers.Add(new ReconnectHandler(this));
            }

            handlerInitializerAction?.Invoke(handlers, this);
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
        /// Gets or sets a value indicating whether <see href="https://xmpp.org/extensions/xep-0138.html">XEP-0138: Stream Compression</see> should be used
        /// on this <see cref="XmppClient" />.
        /// </summary>
        /// <value>
        /// <c>true</c> if compression; otherwise, <c>false</c>.
        /// </value>
        //public bool Compression { get; set; } = true;      

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
        /// <exception cref="RegisterException">Throws a RegisterException when new account registration fails.</exception>
        public override async Task ConnectAsync()
        {
            await ConnectAsync(CancellationToken.None).ConfigureAwait(false);
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
        /// <exception cref="RegisterException">Throws aRegisterException when new account registration fails.</exception>
        public override async Task ConnectAsync(CancellationToken cancellationToken)
        {
            await Transport.ConnectAsync(XmppDomain).ConfigureAwait(false);

            XmppSessionStateSubject.Value = Matrix.SessionState.Connected;

            var feat = await SendStreamHeaderAsync(cancellationToken).ConfigureAwait(false);
            await HandleStreamFeaturesAsync(feat, cancellationToken).ConfigureAwait(false);
        }

        internal async Task HandleStreamFeaturesAsync(
            StreamFeatures features, 
            CancellationToken cancellationToken)
        {
            if (XmppSessionStateSubject.Value < Matrix.SessionState.Securing && features.SupportsStartTls && Tls)
            {
                await HandleStreamFeaturesAsync(
                    await DoStartTlsAsync(cancellationToken).ConfigureAwait(false),
                    cancellationToken
                    ).ConfigureAwait(false);
            }
            else if (XmppSessionStateSubject.Value < Matrix.SessionState.Registering && features.SupportsRegistration && RegistrationHandler?.RegisterNewAccount == true)
            {
                await DoRegisterAsync(cancellationToken).ConfigureAwait(false);
                await HandleStreamFeaturesAsync(features, cancellationToken).ConfigureAwait(false);
            }
            else if (XmppSessionStateSubject.Value < Matrix.SessionState.Authenticating)
            {
                var authRet = await DoAuthenicateAsync(features.Mechanisms, cancellationToken).ConfigureAwait(false);
                await HandleStreamFeaturesAsync(authRet, cancellationToken).ConfigureAwait(false);
            }
            else if (XmppSessionStateSubject.Value < Matrix.SessionState.Binding
                    && handlers.Contains<IStreamManagementHandler>()
                    && handlers.Get<IStreamManagementHandler>().CanResume
                    && handlers.Get<IStreamManagementHandler>().StreamId != null)
            {
                await handlers.Get<IStreamManagementHandler>().ResumeAsync(cancellationToken).ConfigureAwait(false);
                await HandleStreamFeaturesAsync(features, cancellationToken).ConfigureAwait(false);
            }
            else if (XmppSessionStateSubject.Value < Matrix.SessionState.Binding)
            {
                await DoBindAsync(features, cancellationToken).ConfigureAwait(false);
                // enable stream Management if supported by the server and enabled directly after resource binding
                if (handlers.Contains<IStreamManagementHandler>()
                    && handlers.Get<IStreamManagementHandler>().Supported
                    && !handlers.Get<IStreamManagementHandler>().IsEnabled)
                {
                    await handlers.Get<IStreamManagementHandler>().EnableAsync().ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Handle StartTls asynchronous
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<StreamFeatures> DoStartTlsAsync(CancellationToken cancellationToken)
        {
            XmppSessionStateSubject.Value = SessionState.Securing;

            await SendAsync<Proceed>(new StartTls(), cancellationToken).ConfigureAwait(false);
            await Transport.InitTls(XmppDomain).ConfigureAwait(false);
            
            var streamFeatures = await ResetStreamAsync(cancellationToken).ConfigureAwait(false);

            XmppSessionStateSubject.Value = SessionState.Secure;
            return streamFeatures;
        }

        private async Task<StreamFeatures> DoAuthenicateAsync(Mechanisms mechanisms, CancellationToken cancellationToken)
        {
            XmppSessionStateSubject.Value = Matrix.SessionState.Authenticating;
            var res = await SaslHandler.AuthenticateAsync(mechanisms, this, cancellationToken).ConfigureAwait(false);

            if (res is Success)
            {
                XmppSessionStateSubject.Value = Matrix.SessionState.Authenticated;
                return await ResetStreamAsync(cancellationToken).ConfigureAwait(false);
            }
            else //if (res is Failure)
            {
                throw new AuthenticationException(res);
            }
        }

        private async Task<Iq> DoBindAsync(StreamFeatures features, CancellationToken cancellationToken)
        {
            XmppSessionStateSubject.Value = Matrix.SessionState.Binding;

            var bIq = new BindIq { Type = IqType.Set, Bind = { Resource = Resource } };
            var resBindIq = await SendIqAsync(bIq, cancellationToken).ConfigureAwait(false);

            if (resBindIq.Type != IqType.Result)
                throw new BindException(resBindIq);

            if (features.SupportsSession && !features.Session.Optional)
            {
                var sessionIq = new SessionIq { Type = IqType.Set };
                await SendIqAsync(sessionIq, cancellationToken).ConfigureAwait(false);
            }

            XmppSessionStateSubject.Value = Matrix.SessionState.Binded;

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
            XmppSessionStateSubject.Value = Matrix.SessionState.Registering;
            var regInfoIqResult =
                await SendIqAsync(
                    new RegisterIq { Type = IqType.Get, To = XmppDomain },
                    cancellationToken)
                    .ConfigureAwait(false);

            if (regInfoIqResult.Type == IqType.Result && regInfoIqResult.Query is Register)
            {
                var regIq = new Iq { Type = IqType.Set, To = new Jid(XmppDomain) };
                regIq.GenerateId();
                if (RegistrationHandler != null)
                {
                    regIq.Query = await RegistrationHandler.RegisterAsync(regInfoIqResult.Query as Register);
                }
                
                var regResult = await SendIqAsync(regIq, cancellationToken).ConfigureAwait(false);
                if (regResult.Type == IqType.Result)
                {
                    XmppSessionStateSubject.Value = Matrix.SessionState.Registered;
                    return;
                }
                else
                    throw new RegisterException(regResult);
            }
            throw new RegisterException(regInfoIqResult);
        }

        //private async Task<StreamFeatures> DoEnableCompressionAsync(CancellationToken cancellationToken)
        //{
        //    XmppSessionState.Value = SessionState.Compressing;

        //    var ret = await SendAsync<Compresed, Xmpp.Compression.Failure>(new Compress(Methods.Zlib), cancellationToken);
        //    if (ret.OfType<Compresed>())
        //    {
        //        Pipeline.Get<ZlibEncoder>().Active = true;
        //        Pipeline.Get<ZlibDecoder>().Active = true;

        //        var streamFeatures = await ResetStreamAsync(cancellationToken);
        //        XmppSessionState.Value = SessionState.Compressed;
        //        return streamFeatures;
        //    }
        //    else if (ret.OfType<Xmpp.Compression.Failure>())
        //    {
        //        throw new CompressionException(ret.Cast<Xmpp.Compression.Failure>());
        //    }

        //    throw new XmppException(ret);
        //}

        public async Task<StreamFeatures> ResetStreamAsync(CancellationToken cancellationToken)
        {
            return await SendStreamHeaderAsync(cancellationToken).ConfigureAwait(false);
        }

        protected async Task<StreamFeatures> SendStreamHeaderAsync()
        {
            return await SendStreamHeaderAsync(Timeout).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends the XMPP stream header and awaits the reply.
        /// </summary>
        /// <exception cref="StreamErrorException">
        /// Throws a StreamErrorException when the server returns a stream error
        /// </exception>
        /// <returns></returns>
        protected async Task<StreamFeatures> SendStreamHeaderAsync(int timeout)
        {
            return await SendStreamHeaderAsync(timeout, CancellationToken.None).ConfigureAwait(false);
        }

        protected async Task<StreamFeatures> SendStreamHeaderAsync(CancellationToken cancellationToken)
        {
            return await SendStreamHeaderAsync(Timeout, cancellationToken).ConfigureAwait(false);
        }

        protected async Task<StreamFeatures> SendStreamHeaderAsync(int timeout, CancellationToken cancellationToken)
        {
            var streamHeader = Transport.GetStreamHeader(XmppDomain, "1.0");
            var res = await SendAsync<StreamFeatures, Xmpp.Stream.Error>(streamHeader, timeout, cancellationToken).ConfigureAwait(false);

            if (res.OfType<StreamFeatures>())
            {
                return res.Cast<StreamFeatures>();
            }
            else //if (res.OfType<Xmpp.Stream.Error>())
            {
                throw new StreamErrorException(res.Cast<Xmpp.Stream.Error>());
            }
        }

        #region << Send iq >>
        /// <summary>
        /// Send an Iq asynchronous to the server
        /// </summary>
        /// <param name="iq"></param>
        /// <returns>The server response Iq</returns>
        public async Task<Iq> SendIqAsync(Iq iq)
        {
            return await SendIqAsync(iq, XmppXElementFilter.DefaultTimeout).ConfigureAwait(false);
        }

        /// <summary>
        /// Send an Iq asynchronous to the server
        /// </summary>
        /// <param name="iq"></param>
        /// <param name="timeout"></param>
        /// <returns>The server response Iq</returns>
        public async Task<Iq> SendIqAsync(Iq iq, int timeout)
        {
            return await SendIqAsync(iq, timeout, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Send an Iq asynchronous to the server
        /// </summary>
        /// <param name="iq"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The server response Iq</returns>
        public async Task<Iq> SendIqAsync(Iq iq, CancellationToken cancellationToken)
        {
            return await SendIqAsync(iq, XmppXElementFilter.DefaultTimeout, cancellationToken).ConfigureAwait(false);
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

            return await SendIqAsync<Iq>(iq, timeout, cancellationToken).ConfigureAwait(false);
        }
        #endregion   
    }
}
