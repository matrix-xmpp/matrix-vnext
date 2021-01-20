namespace Matrix.Extensions.Client.Presence
{
    using Matrix.Xmpp.Client;
    using System.Threading.Tasks;
    using System;
    using Matrix.Xmpp;

    public static class PresenceExtensions
    {
        /// <summary>
        /// Send a presence update to the server.
        /// </summary>
        /// <param name="stanzaSender"><see cref="IStanzaSender"/></param>
        /// <param name="pres">The presence stanza which gets sent to the server.</param>
        /// <returns></returns>
        public static async Task SendPresenceAsync(this IStanzaSender stanzaSender, Presence pres)
        {
            Contract.Requires<ArgumentNullException>(pres != null, $"{nameof(pres)} cannot be null");

            await stanzaSender.SendAsync(pres).ConfigureAwait(false);
        }

        /// <summary>
        /// Send a presence update to the server. Use the properies Show, Status and priority to update presence information
        /// </summary>
        /// <param name="stanzaSender"><see cref="IStanzaSender"/></param>
        /// <param name="show"></param>
        /// <returns></returns>
        public static async Task SendPresenceAsync(this IStanzaSender stanzaSender, Show show)
        {
            await stanzaSender.SendAsync(new Presence
            {
                Show = show
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Send a presence update to the server. Use the properies Show, Status and priority to update presence information
        /// </summary>
        /// <param name="stanzaSender"><see cref="IStanzaSender"/></param>
        /// <param name="show"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static async Task SendPresenceAsync(this IStanzaSender stanzaSender, Show show, string status)
        {
            await stanzaSender.SendAsync(new Presence
            {
                Show = show,
                Status = status
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Send a presence update to the server. Use the properies Show, Status and priority to update presence information
        /// </summary>
        /// <param name="stanzaSender"><see cref="IStanzaSender"/></param>
        /// <param name="show"></param>
        /// <param name="status"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public static async Task SendPresenceAsync(this IStanzaSender stanzaSender, Show show, string status, int priority)
        {
            await stanzaSender.SendAsync(new Presence
            {
                Show = show,
                Status = status,
                Priority = priority
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Send a presence update to the server. Use the properies Show, Status and priority to update presence information
        /// </summary>
        /// <param name="stanzaSender"><see cref="IStanzaSender"/></param>
        /// <returns></returns>
        public static async Task SendPresenceAsync(this IStanzaSender stanzaSender)
        {
            await stanzaSender.SendAsync(new Presence()).ConfigureAwait(false);
        }
    }
}
