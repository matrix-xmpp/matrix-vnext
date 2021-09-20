namespace Matrix
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Transport;
    using Xml;

    public abstract class XmppConnection : IStanzaSender
    {
        private readonly XmppXElementFilter xmppXElementFilter;
        internal Configuration Configuration { get; } = new Configuration();

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppConnection"/> class.
        /// </summary>
        protected XmppConnection(
            Action<Configuration> configurationAction
            )
        {
            configurationAction?.Invoke(this.Configuration);
            this.xmppXElementFilter = new XmppXElementFilter(Transport);
        }

        #region << Properties >>
        internal XmppSessionState XmppSessionStateSubject { get; } = new XmppSessionState();
        
        public int Timeout { get; set; } = TimeConstants.TwoMinutes;

        public IObservable<SessionState> StateChanged => XmppSessionStateSubject.ValueChanged;
        
        public ITransport Transport => Configuration.Transport;

        public IObservable<XmppXElement> XmppXElementReceived => Transport.XmlReceived;

        public string XmppDomain { get; set; }
        #endregion

        #region << Send members >>
        #region << SendAsync XmppXElement members >>
        /// <summary>
        /// Send a XmppXElement asynchronous
        /// </summary>
        /// <param name="el"></param>
        /// <returns></returns>
        public virtual async Task SendAsync(XmppXElement el)
        {
            await Transport.SendAsync(el, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Send a XmppXElement asynchronous
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="el"></param>
        /// <returns></returns>
        public virtual async Task<T> SendAsync<T>(XmppXElement el)
             where T : XmppXElement
        {
            return await SendAsync<T>(el, Timeout).ConfigureAwait(false);
        }

        /// <summary>
        /// Send a XmppXElement asynchronous
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="el"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public virtual async Task<T> SendAsync<T>(XmppXElement el, int timeout)
             where T : XmppXElement
        {
            return await xmppXElementFilter.SendAsync<T>(el, timeout).ConfigureAwait(false);
        }

        /// <summary>
        /// Send a XmppXElement asynchronous
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="el"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<T> SendAsync<T>(XmppXElement el, CancellationToken cancellationToken)
             where T : XmppXElement
        {
            return await SendAsync<T>(el, Timeout, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Send a XmppXElement asynchronous
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="el"></param>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<T> SendAsync<T>(XmppXElement el, int timeout, CancellationToken cancellationToken)
             where T : XmppXElement
        {
            return await xmppXElementFilter.SendAsync<T>(el, timeout, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Send a XmppXElement asynchronous
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="el"></param>
        /// <returns></returns>
        public virtual async Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el)
            where T1 : XmppXElement
            where T2 : XmppXElement
        {
            return await SendAsync<T1, T2>(el, Timeout).ConfigureAwait(false);
        }

        public virtual async Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el, int timeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
        {
            return await xmppXElementFilter.SendAsync<T1, T2>(el, timeout).ConfigureAwait(false);
        }

        public virtual async Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el, CancellationToken cancellationToken)
            where T1 : XmppXElement
            where T2 : XmppXElement
        {
            return await SendAsync<T1, T2>(el, Timeout, cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el, int timeout, CancellationToken cancellationToken)
            where T1 : XmppXElement
            where T2 : XmppXElement
        {
            return await xmppXElementFilter.SendAsync<T1, T2>(el, timeout, cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
        {
            return await SendAsync<T1, T2, T3>(el, Timeout).ConfigureAwait(false);
        }

        public virtual async Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el, int timeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
        {
            return await xmppXElementFilter.SendAsync<T1, T2, T3>(el, timeout).ConfigureAwait(false);
        }

        public virtual async Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el, CancellationToken cancellationToken)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
        {
            return await SendAsync<T1, T2, T3>(el, Timeout, cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el, int timeout, CancellationToken cancellationToken)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
        {
            return await xmppXElementFilter.SendAsync<T1, T2, T3>(el, timeout, cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task<XmppXElement> SendAsync(XmppXElement el, Func<XmppXElement, bool> predicate)
        {
            return await SendAsync(el, predicate, Timeout, CancellationToken.None).ConfigureAwait(false);
        }
        public virtual async Task<XmppXElement> SendAsync(XmppXElement el, Func<XmppXElement, bool> predicate, CancellationToken cancellationToken)
        {
            return await SendAsync(el, predicate, Timeout, cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task<XmppXElement> SendAsync(XmppXElement el, Func<XmppXElement, bool> predicate, int timeout)
        {
            return await SendAsync(el, predicate, timeout, CancellationToken.None).ConfigureAwait(false);
        }

        public virtual async Task<XmppXElement> SendAsync(XmppXElement el, Func<XmppXElement, bool> predicate, int timeout, CancellationToken cancellationToken)
        {
            return await xmppXElementFilter.SendAsync<XmppXElement>(el, predicate, timeout, cancellationToken).ConfigureAwait(false);
        }
        #endregion

        #region << SendIqAsync members >>
        public async Task<T> SendIqAsync<T>(Xmpp.Base.Iq iq, int timeout, CancellationToken cancellationToken)
          where T : Xmpp.Base.Iq
        {
            return await xmppXElementFilter.SendIqAsync<T>(iq, timeout, cancellationToken).ConfigureAwait(false);
        }
        #endregion

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
        public virtual Task ConnectAsync()
        {
            return default;
        }

        /// <summary>
        /// Connect to the XMPP server.
        /// This establishes the connection to the server, including TLS, authentication, resource binding and
        /// compression.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>       
        public virtual Task ConnectAsync(CancellationToken cancellationToken)
        {
            return default;
        }

        /// <summary>
        /// Close the XMPP connection
        /// </summary>
        /// <param name="sendStreamFooter">
        /// Sends the stream footer to the server when set to true.
        /// Usually a stream footer should be sent to the server when closing the connection.
        /// But there are cases where we may not want to sent one. For example with 
        /// stream management when we want to resume the stream later.
        /// </param>
        /// <param name="timeout">the timeout</param>
        /// <returns></returns>
        public async Task<bool> DisconnectAsync(bool sendStreamFooter = true, int timeout = 2000)
        {
            XmppSessionStateSubject.Value = SessionState.Disconnecting;
            
            IDisposable anonymousSubscription = null;
            var resultCompletionSource = new TaskCompletionSource<bool>();

            if (sendStreamFooter)
            {
                var streamFooter = Transport.GetStreamFooter();
                await SendAsync(streamFooter).ConfigureAwait(false);
            }

            anonymousSubscription = Transport.StateChanged.Subscribe(
                state =>
                {
                    if (state == State.StreamFooterReceived)
                    {
                        anonymousSubscription?.Dispose();
                        resultCompletionSource.SetResult(true);
                    }
                });

            if (resultCompletionSource.Task ==
                await Task.WhenAny(resultCompletionSource.Task, Task.Delay(timeout)).ConfigureAwait(false))
            {
                await Transport.DisconnectAsync().ConfigureAwait(false);
                return await resultCompletionSource.Task.ConfigureAwait(false);
            }

            // timed out
            anonymousSubscription.Dispose();
            await Transport.DisconnectAsync().ConfigureAwait(false);

            return true;
        }
    }
}
