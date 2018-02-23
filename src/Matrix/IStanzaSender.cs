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

namespace Matrix
{
    using Matrix.Xml;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

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
