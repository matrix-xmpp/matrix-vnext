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
            return await DiscoverItemsAsync(iqSender, to, null, DefaultTimeout, CancellationToken.None).ConfigureAwait(false);
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
            return await DiscoverItemsAsync(iqSender, to, null, timeout, CancellationToken.None).ConfigureAwait(false);
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
            return await DiscoverItemsAsync(iqSender, to, node, DefaultTimeout, CancellationToken.None).ConfigureAwait(false);
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
            return await DiscoverItemsAsync(iqSender, to, node, timeout, CancellationToken.None).ConfigureAwait(false);
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
            return await iqSender.SendIqAsync(DiscoBuilder.DiscoverItems(to, node), timeout, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Discover information about an entity asynchronous
        /// </summary>
        /// <param name="iqSender">The <see cref="IClientIqSender"/></param>
        /// <param name="to">The <see cref="Jid"/> to discover</param>
        /// <returns></returns>
        public static async Task<Iq> DiscoverInformationAsync(this IClientIqSender iqSender, Jid to)
        {
            return await DiscoverInformationAsync(iqSender, to, null, DefaultTimeout, CancellationToken.None).ConfigureAwait(false);
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
            return await DiscoverInformationAsync(iqSender, to, null, timeout, CancellationToken.None).ConfigureAwait(false);
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
            return await DiscoverInformationAsync(iqSender, to, node, DefaultTimeout, CancellationToken.None).ConfigureAwait(false);
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
            return await DiscoverInformationAsync(iqSender, to, node, timeout, CancellationToken.None).ConfigureAwait(false);
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
            return await iqSender.SendIqAsync(DiscoBuilder.DiscoverInformation(to, node), timeout, cancellationToken).ConfigureAwait(false);
        }
    }
}
