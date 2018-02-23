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

namespace Matrix.Extensions.Client.Disco
{
    using Matrix.Xmpp.Client; 
    using System.Threading;
    using System.Threading.Tasks;

    public static class DiscoExtensions
    {
        public static int DefaultTimeout { get; set; } = TimeConstants.TwoMinutes;

        /// <summary>
        /// Discover an entity asynchronous
        /// </summary>
        /// <param name="iqSender">The <see cref="IClientIqSender"/></param>
        /// <param name="to">The <see cref="Jid"/> to discover</param>
        /// <returns></returns>
        public static async Task<Iq> DiscoverItemsAsync(this IClientIqSender iqSender, Jid to)
        {
            return await DiscoverItemsAsync(iqSender, to, null, DefaultTimeout, CancellationToken.None);
        }

        /// <summary>
        /// Discover an entity asynchronous
        /// </summary>
        /// <param name="iqSender">The <see cref="IClientIqSender"/></param>
        /// <param name="to">The <see cref="Jid"/> to discover</param>
        /// <param name="timeout">The timeout in milliseconds</param>
        /// <returns></returns>
        public static async Task<Iq> DiscoverItemsAsync(this IClientIqSender iqSender, Jid to, int timeout)
        {
            return await DiscoverItemsAsync(iqSender, to, null, timeout, CancellationToken.None);
        }

        /// <summary>
        /// Discover an entity asynchronous
        /// </summary>
        /// <param name="iqSender">The <see cref="IClientIqSender"/></param>
        /// <param name="to">The <see cref="Jid"/> to discover</param>
        /// <param name="node">The node to discover</param>
        /// <returns></returns>
        public static async Task<Iq> DiscoverItemsAsync(this IClientIqSender iqSender, Jid to, string node)
        {
            return await DiscoverItemsAsync(iqSender, to, node, DefaultTimeout, CancellationToken.None);
        }

        /// <summary>
        /// Discover an entity asynchronous
        /// </summary>
        /// <param name="iqSender">The <see cref="IClientIqSender"/></param>
        /// <param name="to">The <see cref="Jid"/> to discover</param>
        /// <param name="node">The node to discover</param>
        /// <param name="timeout">The timeout in milliseconds</param>
        /// <returns></returns>
        public static async Task<Iq> DiscoverItemsAsync(this IClientIqSender iqSender, Jid to, string node, int timeout)
        {
            return await DiscoverItemsAsync(iqSender, to, node, timeout, CancellationToken.None);
        }

        /// <summary>
        /// Discover an entity asynchronous
        /// </summary>
        /// <param name="iqSender">The <see cref="IClientIqSender"/></param>
        /// <param name="to">The <see cref="Jid"/> to discover</param>
        /// <param name="node">The node to discover</param>
        /// <param name="timeout">The timeout in milliseconds</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the request.</param>
        /// <returns></returns>
        public static async Task<Iq> DiscoverItemsAsync(this IClientIqSender iqSender, Jid to, string node, int timeout, CancellationToken cancellationToken)
        {
            return await iqSender.SendIqAsync(DiscoBuilder.DiscoverItems(to, node), timeout, cancellationToken);
        }

        /// <summary>
        /// Discover information about an entity asynchronous
        /// </summary>
        /// <param name="iqSender">The <see cref="IClientIqSender"/></param>
        /// <param name="to">The <see cref="Jid"/> to discover</param>
        /// <returns></returns>
        public static async Task<Iq> DiscoverInformationAsync(this IClientIqSender iqSender, Jid to)
        {
            return await DiscoverInformationAsync(iqSender, to, null, DefaultTimeout, CancellationToken.None);
        }

        /// <summary>
        /// Discover information about an entity asynchronous
        /// </summary>
        /// <param name="iqSender">The <see cref="IClientIqSender"/></param>
        /// <param name="to">The <see cref="Jid"/> to discover</param>
        /// <param name="timeout">The timeout in milliseconds</param>
        /// <returns></returns>
        public static async Task<Iq> DiscoverInformationAsync(this IClientIqSender iqSender, Jid to, int timeout)
        {
            return await DiscoverInformationAsync(iqSender, to, null, timeout, CancellationToken.None);
        }

        /// <summary>
        /// Discover information about an entity asynchronous
        /// </summary>
        /// <param name="iqSender">The <see cref="IClientIqSender"/></param>
        /// <param name="to">The <see cref="Jid"/> to discover</param>
        /// <param name="node">The node to discover</param>
        /// <returns></returns>
        public static async Task<Iq> DiscoverInformationAsync(this IClientIqSender iqSender, Jid to, string node)
        {
            return await DiscoverInformationAsync(iqSender, to, node, DefaultTimeout, CancellationToken.None);
        }

        /// <summary>
        /// Discover information about an entity asynchronous
        /// </summary>
        /// <param name="iqSender">The <see cref="IClientIqSender"/></param>
        /// <param name="to">The <see cref="Jid"/> to discover</param>
        /// <param name="node">The node to discover</param>
        /// <param name="timeout">The timeout in milliseconds</param>
        /// <returns></returns>
        public static async Task<Iq> DiscoverInformationAsync(this IClientIqSender iqSender, Jid to, string node, int timeout)
        {
            return await DiscoverInformationAsync(iqSender, to, node, timeout, CancellationToken.None);
        }

        /// <summary>
        /// Discover information about an entity asynchronous
        /// </summary>
        /// <param name="iqSender">The <see cref="IClientIqSender"/></param>
        /// <param name="to">The <see cref="Jid"/> to discover</param>
        /// <param name="node">The node to discover</param>
        /// <param name="timeout">The timeout in milliseconds</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the request.</param>
        /// <returns></returns>
        public static async Task<Iq> DiscoverInformationAsync(this IClientIqSender iqSender, Jid to, string node, int timeout, CancellationToken cancellationToken)
        {
            return await iqSender.SendIqAsync(DiscoBuilder.DiscoverInformation(to, node), timeout, cancellationToken);
        }
    }
}
