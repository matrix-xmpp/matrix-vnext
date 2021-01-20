namespace Matrix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Transport;
    using Xml;
    using Xmpp;
    using Xmpp.Base;

    public class XmppXElementFilter
    {
        public static int DefaultTimeout { get; set; } = TimeConstants.TwoMinutes;

        private readonly Dictionary<Func<XmppXElement, bool>, Action<XmppXElement>> handlers 
            = new Dictionary<Func<XmppXElement, bool>, Action<XmppXElement>>();

        private readonly ITransport transport;

        public XmppXElementFilter(ITransport transport)
        {
            this.transport = transport;
            transport
                .XmlReceived
                .Subscribe(el =>
                {
                    // handle
                    var handlerList = handlers.Keys.ToList();
                    foreach (var predicate in handlerList)
                    {
                        if (el.IsMatch(predicate))
                        {
                            if (handlers.ContainsKey(predicate))
                            {
                                Task.Run(() =>
                                {
                                    handlers[predicate].Invoke(el);
                                });
                            }
                        }
                    }
                });
        }

        protected XmppXElementFilter Handle<T>(Action<XmppXElement> action) where T : XmppXElement
        {
            return Handle(el => el.OfType<T>(), action);
        }

        protected XmppXElementFilter Handle(Func<XmppXElement, bool> predicate, Action<XmppXElement> action)
        {
            if (predicate == null) return this;
            Contract.Requires<ArgumentNullException>(action != null, nameof(action));

            handlers.Add(predicate, action);
            return this;
        }

        protected XmppXElementFilter UnHandle(Func<XmppXElement, bool> predicate)
        {
            if (predicate == null) return this;

            if (handlers.ContainsKey(predicate))
                handlers.Remove(predicate);

            return this;
        }

        #region << SendAsync members >>

        #region << SendAsync XmppXElement members >>
        protected async Task SendAsync(XmppXElement el)
        {
            await transport.SendAsync(el).ConfigureAwait(false);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el, CancellationToken cancellationToken)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
        {
            return await SendAsync<T1, T2, T3>(el, DefaultTimeout, cancellationToken).ConfigureAwait(false);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el, int timeout, CancellationToken cancellationToken)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
        {
            static bool Predicate(XmppXElement e) => e.OfTypeAny<T1, T2, T3>();
            return await SendAsync<XmppXElement>(() => SendAsync(el), Predicate, timeout, cancellationToken).ConfigureAwait(false);
        }

        public async Task<T> SendAsync<T>(XmppXElement el)
            where T : XmppXElement
        {
            return await SendAsync<T>(el, DefaultTimeout).ConfigureAwait(false);
        }

        public async Task<T> SendAsync<T>(XmppXElement el, int timeout)
              where T : XmppXElement
        {
            static bool Predicate(XmppXElement e) => e.OfType<T>();
            return await SendAsync<T>(() => SendAsync(el), Predicate, timeout).ConfigureAwait(false);
        }

        public async Task<T> SendAsync<T>(XmppXElement el, CancellationToken cancellationToken)
             where T : XmppXElement
        {
            return await SendAsync<T>(el, DefaultTimeout, cancellationToken).ConfigureAwait(false);
        }

        public async Task<T> SendAsync<T>(XmppXElement el, int timeout, CancellationToken cancellationToken)
             where T : XmppXElement
        {
            static bool Predicate(XmppXElement e) => e.OfType<T>();
            return await SendAsync<T>(() => SendAsync(el), Predicate, timeout, cancellationToken).ConfigureAwait(false);
        }

        public async Task<T> SendAsync<T>(XmppXElement el, Func<XmppXElement, bool> predicate, int timeout, CancellationToken cancellationToken)
          where T : XmppXElement
        {
            return await SendAsync<T>(() => SendAsync(el), predicate, timeout, cancellationToken).ConfigureAwait(false);
        }

        public async Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el)
           where T1 : XmppXElement
           where T2 : XmppXElement
        {
            return await SendAsync<T1, T2>(el, DefaultTimeout).ConfigureAwait(false);
        }

        public async Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el, int timeout)
           where T1 : XmppXElement
           where T2 : XmppXElement
        {
            return await SendAsync<T1, T2>(el, timeout, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el, CancellationToken cancellationToken)
          where T1 : XmppXElement
          where T2 : XmppXElement
        {
            return await SendAsync<T1, T2>(el, DefaultTimeout, cancellationToken).ConfigureAwait(false);
        }

        public async Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el, int timeout, CancellationToken cancellationToken)
           where T1 : XmppXElement
           where T2 : XmppXElement
        {
            static bool Predicate(XmppXElement e) => e.OfTypeAny<T1, T2>();
            return await SendAsync<XmppXElement>(() => SendAsync(el), Predicate, timeout, cancellationToken).ConfigureAwait(false);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
        {
            return await SendAsync<T1, T2, T3>(el, DefaultTimeout).ConfigureAwait(false);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el, int timeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
        {
            static bool Predicate(XmppXElement e) => e.OfTypeAny<T1, T2, T3>();
            return await SendAsync<XmppXElement>(() => SendAsync(el), Predicate, timeout).ConfigureAwait(false);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3, T4>(XmppXElement el)
           where T1 : XmppXElement
           where T2 : XmppXElement
           where T3 : XmppXElement
           where T4 : XmppXElement
        {
            static bool Predicate(XmppXElement e) => e.OfTypeAny<T1, T2, T3, T4>();
            return await SendAsync<XmppXElement>(() => SendAsync(el), Predicate, DefaultTimeout).ConfigureAwait(false);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3, T4>(XmppXElement el, int timeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
            where T4 : XmppXElement
        {
            static bool Predicate(XmppXElement e) => e.OfTypeAny<T1, T2, T3, T4>();
            return await SendAsync<XmppXElement>(() => SendAsync(el), Predicate, timeout).ConfigureAwait(false);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3, T4, T5>(XmppXElement el)
           where T1 : XmppXElement
           where T2 : XmppXElement
           where T3 : XmppXElement
           where T4 : XmppXElement
           where T5 : XmppXElement
        {
            static bool Predicate(XmppXElement e) => e.OfTypeAny<T1, T2, T3, T4, T5>();
            return await SendAsync<XmppXElement>(() => SendAsync(el), Predicate, DefaultTimeout).ConfigureAwait(false);
        }
        public async Task<XmppXElement> SendAsync<T1, T2, T3, T4, T5>(XmppXElement el, int timeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
            where T4 : XmppXElement
            where T5 : XmppXElement
        {
            static bool Predicate(XmppXElement e) => e.OfTypeAny<T1, T2, T3, T4, T5>();
            return await SendAsync<XmppXElement>(() => SendAsync(el), Predicate, timeout).ConfigureAwait(false);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sendTask"></param>
        /// <param name="predicate"></param>
        /// <param name="timeout"></param>
        /// <exception cref="TimeoutException">Throws a TimeoutException when no response gets received within the given timeout.</exception>
        /// <returns></returns>
        private async Task<T> SendAsync<T>(
            Func<Task> sendTask,
            Func<XmppXElement, bool> predicate,
            int timeout)
            where T : XmppXElement

        {
            return await SendAsync<T>(sendTask, predicate, timeout, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sendTask"></param>
        /// <param name="predicate"></param>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="TimeoutException">Throws a TimeoutException when no response gets received within the given timeout.</exception>
        /// <exception cref="TaskCanceledException"></exception>
        /// <returns></returns>
        public async Task<T> SendAsync<T>(
            Func<Task> sendTask,
            Func<XmppXElement, bool> predicate,
            int timeout,
            CancellationToken cancellationToken
        )
           where T : XmppXElement

        {
            Exception exception = null;
            var resultCompletionSource = new TaskCompletionSource<T>();

            var action = new Action<XmppXElement>(
                (xel) =>
                {
                    UnHandle(predicate);
                    resultCompletionSource.SetResult(xel as T);
                });

            Handle(predicate, action);
            
            await sendTask().ConfigureAwait(false);
            
            try
            {
                if (resultCompletionSource.Task ==
                    await Task.WhenAny(resultCompletionSource.Task, Task.Delay(timeout, cancellationToken)).ConfigureAwait(false))
                {
                    return await resultCompletionSource.Task.ConfigureAwait(false);
                }
                cancellationToken.ThrowIfCancellationRequested();
            }
            catch (TaskCanceledException ex)
            {
                exception = ex;
            }
            catch (OperationCanceledException ex)
            {
                exception = ex;
            }


            // timed out, remove this iq from our dictionary
            exception ??= new TimeoutException();

            UnHandle(predicate);
            resultCompletionSource.SetException(exception);

            return await resultCompletionSource.Task.ConfigureAwait(false);
        }
        #endregion

        #region << Send Iq >>
        public async Task<T> SendIqAsync<T>(Iq iq)
            where T : Iq
        {
            return await SendIqAsync<T>(iq, DefaultTimeout).ConfigureAwait(false);
        }

        public async Task<T> SendIqAsync<T>(Iq iq, int timeout)
            where T : Iq
        {
            return await SendIqAsync<T>(iq, timeout, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<T> SendIqAsync<T>(Iq iq, int timeout, CancellationToken cancellationToken)
            where T : Iq
        {
            bool Predicate(XmppXElement e) =>
                e.OfType<T>()
                && e.Cast<T>().Id == iq.Id
                && (e.Cast<T>().Type.IsAnyOf(IqType.Error, IqType.Result));

            return await SendAsync<T>(() => SendAsync(iq), Predicate, timeout, cancellationToken).ConfigureAwait(false);
        }
        #endregion
    }
}
