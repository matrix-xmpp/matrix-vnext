namespace Matrix.Extensions.Client.Roster
{
    using Matrix.Xmpp.Client;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public static class XmppClientExtensions
    {
        public static int DefaultTimeout { get; set; } = TimeConstants.TwoMinutes;

        /// <summary>
        /// Request the roster (contact list) asynchronous from the server.
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static async Task<Iq> RequestRosterAsync(this IClientIqSender iqSender, string version = null)
        {
            return await RequestRosterAsync(iqSender, version, DefaultTimeout, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Request the roster (contact list) asynchronous from the server.
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Iq> RequestRosterAsync(this IClientIqSender iqSender, int timeout, CancellationToken cancellationToken)
        {
            return await RequestRosterAsync(iqSender, null, DefaultTimeout, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Request the roster (contact list) asynchronous from the server.
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static async Task<Iq> RequestRosterAsync(this IClientIqSender iqSender, int timeout)
        {
            return await RequestRosterAsync(iqSender, null, timeout, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Request the roster (contact list) asynchronous from the server.
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="version"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static async Task<Iq> RequestRosterAsync(this IClientIqSender iqSender, string version, int timeout)
        {
            return await RequestRosterAsync(iqSender, version, timeout, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Request the roster (contact list) asynchronous from the server.
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Iq> RequestRosterAsync(this IClientIqSender iqSender, CancellationToken cancellationToken)
        {
            return await RequestRosterAsync(iqSender, null, DefaultTimeout, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Request the roster (contact list) asynchronous from the server.
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="version"></param>
        /// <param name="cancellationToken">The cancellation token used to cancel the request.</param>
        /// <returns></returns>
        public static async Task<Iq> RequestRosterAsync(this IClientIqSender iqSender, string version, CancellationToken cancellationToken)
        {
            return await RequestRosterAsync(iqSender, version, DefaultTimeout, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Request the roster (contact list) asynchronous from the server.
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="version"></param>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken">The cancellation token used to cancel the request.</param>
        /// <returns></returns>
        public static async Task<Iq> RequestRosterAsync(this IClientIqSender iqSender, string version, int timeout, CancellationToken cancellationToken)
        {
            return await iqSender.SendIqAsync(RosterBuilder.RequestRoster(version) , timeout, cancellationToken).ConfigureAwait(false);            
        }

        /// <summary>
        /// Add a Rosteritem to the Roster
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The BARE jid of the rosteritem that should be removed</param>
        /// <returns></returns>
        public static async Task<Iq> AddRosterItemAsync(this IClientIqSender iqSender, Jid jid)
        {
            return await AddRosterItemAsync(iqSender, jid, null, new string[] { }).ConfigureAwait(false);
        }

        /// <summary>
        /// Add a Rosteritem to the Roster
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The BARE jid of the rosteritem that should be removed</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <returns></returns>
        public static async Task<Iq> AddRosterItemAsync(this IClientIqSender iqSender, Jid jid, int timeout)
        {
            return await AddRosterItemAsync(iqSender, jid, null, new string[] { }, timeout, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Add a Rosteritem to the Roster
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The BARE jid of the rosteritem that should be removed</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the request.</param>
        /// <returns></returns>
        public static async Task<Iq> AddRosterItemAsync(this IClientIqSender iqSender, Jid jid, int timeout, CancellationToken cancellationToken)
        {
            return await AddRosterItemAsync(iqSender, jid, null, new string[] { }, timeout, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Add a Rosteritem to the Roster
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The BARE jid of the rosteritem that should be removed</param>
        /// <param name="nickname">Nickname for the RosterItem</param>
        /// <returns></returns>
        public static async Task<Iq> AddRosterItemAsync(this IClientIqSender iqSender, Jid jid, string nickname)
        {
            return await AddRosterItemAsync(iqSender, jid, nickname, new string[] { }).ConfigureAwait(false);
        }

        /// <summary>
        /// Add a Rosteritem to the Roster
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The BARE jid of the rosteritem that should be removed</param>
        /// <param name="nickname">Nickname for the RosterItem</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <returns></returns>
        public static async Task<Iq> AddRosterItemAsync(this IClientIqSender iqSender, Jid jid, string nickname, int timeout)
        {
            return await AddRosterItemAsync(iqSender, jid, nickname, new string[] { }, timeout, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Add a Rosteritem to the Roster
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The BARE jid of the rosteritem that should be removed</param>
        /// <param name="nickname">Nickname for the RosterItem</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the request.</param>
        /// <returns></returns>
        public static async Task<Iq> AddRosterItemAsync(this IClientIqSender iqSender, Jid jid, string nickname, int timeout, CancellationToken cancellationToken)
        {
            return await AddRosterItemAsync(iqSender, jid, nickname, new string[] { }, timeout, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Add a Rosteritem to the Roster
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The BARE jid of the rosteritem that should be removed</param>
        /// <param name="nickname">Nickname for the RosterItem</param>
        /// <param name="group">The group to which the roteritem should be added</param>
        /// <returns></returns>
        public static async Task<Iq> AddRosterItemAsync(this IClientIqSender iqSender, Jid jid, string nickname, string group)
        {
            return await AddRosterItemAsync(iqSender, jid, nickname, !String.IsNullOrEmpty(group) ? new[] { group } : new string[] { }).ConfigureAwait(false);
        }

        /// <summary>
        /// Add a Rosteritem to the Roster
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The BARE jid of the rosteritem that should be removed</param>
        /// <param name="nickname">Nickname for the RosterItem</param>
        /// <param name="group">The group to which the roteritem should be added</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <returns></returns>
        public static async Task<Iq> AddRosterItemAsync(this IClientIqSender iqSender, Jid jid, string nickname, string group, int timeout)
        {
            return await AddRosterItemAsync(iqSender, jid, nickname, !String.IsNullOrEmpty(group) ? new[] { group } : new string[] { }, timeout, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Add a Rosteritem to the Roster
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The BARE jid of the rosteritem that should be removed</param>
        /// <param name="nickname">Nickname for the RosterItem</param>
        /// <param name="group">The group to which the roteritem should be added</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the request.</param>
        /// <returns></returns>
        public static async Task<Iq> AddRosterItemAsync(this IClientIqSender iqSender, Jid jid, string nickname, string group, int timeout, CancellationToken cancellationToken)
        {
            return await AddRosterItemAsync(iqSender, jid, nickname, !String.IsNullOrEmpty(group) ? new[] { group } : new string[] { }, timeout, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Add a Rosteritem to the Roster
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The BARE jid of the rosteritem that should be removed</param>
        /// <param name="nickname">Nickname for the RosterItem</param>
        /// <param name="group">An Array of groups when you want to add the Rosteritem to multiple groups</param>
        /// <returns></returns>
        public static async Task<Iq> AddRosterItemAsync(this IClientIqSender iqSender, Jid jid, string nickname, string[] groups)
        {
            return await AddRosterItemAsync(iqSender, jid, nickname, groups, DefaultTimeout).ConfigureAwait(false);
        }

        /// <summary>
        /// Add a Rosteritem to the Roster
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The BARE jid of the rosteritem that should be removed</param>
        /// <param name="nickname">Nickname for the RosterItem</param>
        /// <param name="group">An Array of groups when you want to add the Rosteritem to multiple groups</param>
        /// <param name="timeout">The timeout in milliseconds.</param>        
        /// <returns></returns>
        public static async Task<Iq> AddRosterItemAsync(this IClientIqSender iqSender, Jid jid, string nickname, string[] groups, int timeout)
        {
            return await AddRosterItemAsync(iqSender, jid, nickname, groups, timeout, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Add a Rosteritem to the Roster
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The BARE jid of the rosteritem that should be removed</param>
        /// <param name="nickname">Nickname for the RosterItem</param>
        /// <param name="group">An Array of groups when you want to add the Rosteritem to multiple groups</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the request.</param>
        /// <returns></returns>
        public static async Task<Iq> AddRosterItemAsync(this IClientIqSender iqSender, Jid jid, string nickname, string[] groups, int timeout, CancellationToken cancellationToken)
        {
            var riq = RosterBuilder.AddRosterItem(jid, nickname, groups);
            return await iqSender.SendIqAsync(riq, timeout, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a Rosteritem
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The jid.</param>
        /// <returns></returns>
        public static async Task<Iq> UpdateRosterItemAsync(this IClientIqSender iqSender, Jid jid)
        {
            return await AddRosterItemAsync(iqSender, jid, null, new string[] { }).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a Rosteritem
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The jid.</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <returns></returns>
        public static async Task<Iq> UpdateRosterItemAsync(this IClientIqSender iqSender, Jid jid, int timeout)
        {
            return await AddRosterItemAsync(iqSender, jid, null, new string[] { }, timeout, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a Rosteritem
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The jid.</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the request.</param>
        /// <returns></returns>
        public static async Task<Iq> UpdateRosterItemAsync(this IClientIqSender iqSender, Jid jid, int timeout, CancellationToken cancellationToken)
        {
            return await AddRosterItemAsync(iqSender, jid, null, new string[] { }, timeout, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a Rosteritem
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The jid.</param>
        /// <param name="nickname">The nickname.</param>
        /// <returns></returns>
        public static async Task<Iq> UpdateRosterItemAsync(this IClientIqSender iqSender, Jid jid, string nickname)
        {
            return await AddRosterItemAsync(iqSender, jid, nickname, new string[] { }).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a Rosteritem
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The jid.</param>
        /// <param name="nickname">The nickname.</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <returns></returns>
        public static async Task<Iq> UpdateRosterItemAsync(this IClientIqSender iqSender, Jid jid, string nickname, int timeout)
        {
            return await AddRosterItemAsync(iqSender, jid, nickname, new string[] { }, timeout, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a Rosteritem
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The jid.</param>
        /// <param name="nickname">The nickname.</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the request.</param>
        /// <returns></returns>
        public static async Task<Iq> UpdateRosterItemAsync(this IClientIqSender iqSender, Jid jid, string nickname, int timeout, CancellationToken cancellationToken)
        {
            return await AddRosterItemAsync(iqSender, jid, nickname, new string[] { }, timeout, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a Rosteritem
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The jid.</param>
        /// <param name="nickname">The nickname.</param>
        /// <param name="group">The group.</param>
        /// <returns></returns>
        public static async Task<Iq> UpdateRosterItemAsync(this IClientIqSender iqSender, Jid jid, string nickname, string group)
        {
            return await AddRosterItemAsync(iqSender, jid, nickname, new[] { group }).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a Rosteritem
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The jid.</param>
        /// <param name="nickname">The nickname.</param>
        /// <param name="group">The group.</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <returns></returns>
        public static async Task<Iq> UpdateRosterItemAsync(this IClientIqSender iqSender, Jid jid, string nickname, string group, int timeout)
        {
            return await AddRosterItemAsync(iqSender, jid, nickname, new[] { group }, timeout, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a Rosteritem
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The jid.</param>
        /// <param name="nickname">The nickname.</param>
        /// <param name="group">The group.</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the request.</param>
        /// <returns></returns>
        public static async Task<Iq> UpdateRosterItemAsync(this IClientIqSender iqSender, Jid jid, string nickname, string group, int timeout, CancellationToken cancellationToken)
        {
            return await AddRosterItemAsync(iqSender, jid, nickname, new[] { group }, timeout, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a Rosteritem
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The jid.</param>
        /// <param name="nickname">The nickname.</param>
        /// <param name="group">The group.</param>
        /// <returns></returns>
        public static async Task<Iq> UpdateRosterItemAsync(this IClientIqSender iqSender, Jid jid, string nickname, string[] group)
        {
            return await AddRosterItemAsync(iqSender, jid, nickname, group).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a Rosteritem
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The jid.</param>
        /// <param name="nickname">The nickname.</param>
        /// <param name="group">The group.</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <returns></returns>
        public static async Task<Iq> UpdateRosterItemAsync(this IClientIqSender iqSender, Jid jid, string nickname, string[] group, int timeout)
        {
            return await AddRosterItemAsync(iqSender, jid, nickname, group, timeout, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a Rosteritem
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The jid.</param>
        /// <param name="nickname">The nickname.</param>
        /// <param name="group">The group.</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the request.</param>
        /// <returns></returns>
        public static async Task<Iq> UpdateRosterItemAsync(this IClientIqSender iqSender, Jid jid, string nickname, string[] group, int timeout, CancellationToken cancellationToken)
        {
            return await AddRosterItemAsync(iqSender, jid, nickname, group, timeout, cancellationToken).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Remove a contact from the contact list, aka roster
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The jid to remove</param>        
        /// <returns></returns>
        public static async Task<Iq> RemoveRosterItemAsync(this IClientIqSender iqSender, Jid jid)
        {
            return await RemoveRosterItemAsync(iqSender, jid, DefaultTimeout).ConfigureAwait(false);
        }

        /// <summary>
        /// Remove a contact from the contact list, aka roster
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The jid to remove</param>
        /// <param name="timeout">The timeout</param>
        /// <returns></returns>
        public static async Task<Iq> RemoveRosterItemAsync(this IClientIqSender iqSender, Jid jid, int timeout)
        {
            return await iqSender.SendIqAsync(RosterBuilder.RemoveRosterItem(jid), timeout, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Remove a contact from the contact list, aka roster
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The jid to remove</param>
        /// <param name="timeout">The timeout</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the request.</param>
        /// <returns></returns>
        public static async Task<Iq> RemoveRosterItemAsync(this IClientIqSender iqSender, Jid jid, int timeout, CancellationToken cancellationToken)
        {            
            return await iqSender.SendIqAsync(RosterBuilder.RemoveRosterItem(jid), timeout, cancellationToken).ConfigureAwait(false);
        }
    }
}
