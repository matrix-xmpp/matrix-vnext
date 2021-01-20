namespace Matrix
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Xml;

    public interface IStanzaSender
    {        
        /// <summary>
        /// Send a XmppXElement asynchronous
        /// </summary>
        /// <param name="el"></param>
        /// <returns></returns>
        Task SendAsync(XmppXElement el);

        /// <summary>
        /// Send a XmppXElement asynchronous
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="el"></param>
        /// <returns></returns>
        Task<T> SendAsync<T>(XmppXElement el) where T 
            : XmppXElement;

        /// <summary>
        /// Send a XmppXElement asynchronous
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="el"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        Task<T> SendAsync<T>(XmppXElement el, int timeout)
             where T : XmppXElement;

        /// <summary>
        /// Send a XmppXElement asynchronous
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="el"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T> SendAsync<T>(XmppXElement el, CancellationToken cancellationToken)
             where T : XmppXElement;


        /// <summary>
        /// Send a XmppXElement asynchronous
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="el"></param>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T> SendAsync<T>(XmppXElement el, int timeout, CancellationToken cancellationToken)
             where T : XmppXElement;

        /// <summary>
        /// Send a XmppXElement asynchronous
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="el"></param>
        /// <returns></returns>
        Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el)
            where T1 : XmppXElement
            where T2 : XmppXElement;

        Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el, int timeout)
            where T1 : XmppXElement
            where T2 : XmppXElement;

        Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el, CancellationToken cancellationToken)
            where T1 : XmppXElement
            where T2 : XmppXElement;

        Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el, int timeout, CancellationToken cancellationToken)
            where T1 : XmppXElement
            where T2 : XmppXElement;

        Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement;

        Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el, int timeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement;

        Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el, CancellationToken cancellationToken)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement;

        Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el, int timeout, CancellationToken cancellationToken)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement;

        Task<XmppXElement> SendAsync(XmppXElement el, Func<XmppXElement, bool> predicate);

        Task<XmppXElement> SendAsync(XmppXElement el, Func<XmppXElement, bool> predicate, CancellationToken cancellationToken);

        Task<XmppXElement> SendAsync(XmppXElement el, Func<XmppXElement, bool> predicate, int timeout);

        Task<XmppXElement> SendAsync(XmppXElement el, Func<XmppXElement, bool> predicate, int timeout, CancellationToken cancellationToken);        
    }
}
