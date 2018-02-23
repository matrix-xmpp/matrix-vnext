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
            return await RequestRosterAsync(iqSender, version, DefaultTimeout, CancellationToken.None);
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
            return await RequestRosterAsync(iqSender, null, DefaultTimeout, CancellationToken.None);
        }

        /// <summary>
        /// Request the roster (contact list) asynchronous from the server.
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static async Task<Iq> RequestRosterAsync(this IClientIqSender iqSender, int timeout)
        {
            return await RequestRosterAsync(iqSender, null, timeout, CancellationToken.None);
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
            return await RequestRosterAsync(iqSender, version, timeout, CancellationToken.None);
        }

        /// <summary>
        /// Request the roster (contact list) asynchronous from the server.
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Iq> RequestRosterAsync(this IClientIqSender iqSender, CancellationToken cancellationToken)
        {
            return await RequestRosterAsync(iqSender, null, DefaultTimeout, cancellationToken);
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
            return await RequestRosterAsync(iqSender, version, DefaultTimeout, cancellationToken);
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
            return await iqSender.SendIqAsync(RosterBuilder.RequestRoster(version) , timeout, cancellationToken);            
        }

        /// <summary>
        /// Add a Rosteritem to the Roster
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The BARE jid of the rosteritem that should be removed</param>
        /// <returns></returns>
        public static async Task<Iq> AddRosterItemAsync(this IClientIqSender iqSender, Jid jid)
        {
            return await AddRosterItemAsync(iqSender, jid, null, new string[] { });
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
            return await AddRosterItemAsync(iqSender, jid, null, new string[] { }, timeout, CancellationToken.None);
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
            return await AddRosterItemAsync(iqSender, jid, null, new string[] { }, timeout, cancellationToken);
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
            return await AddRosterItemAsync(iqSender, jid, nickname, new string[] { });
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
            return await AddRosterItemAsync(iqSender, jid, nickname, new string[] { }, timeout, CancellationToken.None);
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
            return await AddRosterItemAsync(iqSender, jid, nickname, new string[] { }, timeout, cancellationToken);
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
            return await AddRosterItemAsync(iqSender, jid, nickname, !String.IsNullOrEmpty(group) ? new[] { group } : new string[] { });
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
            return await AddRosterItemAsync(iqSender, jid, nickname, !String.IsNullOrEmpty(group) ? new[] { group } : new string[] { }, timeout, CancellationToken.None);
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
            return await AddRosterItemAsync(iqSender, jid, nickname, !String.IsNullOrEmpty(group) ? new[] { group } : new string[] { }, timeout, cancellationToken);
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
            return await AddRosterItemAsync(iqSender, jid, nickname, groups, DefaultTimeout);
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
            return await AddRosterItemAsync(iqSender, jid, nickname, groups, timeout, CancellationToken.None);
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
            return await iqSender.SendIqAsync(riq, timeout, cancellationToken);
        }

        /// <summary>
        /// Update a Rosteritem
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The jid.</param>
        /// <returns></returns>
        public static async Task<Iq> UpdateRosterItemAsync(this IClientIqSender iqSender, Jid jid)
        {
            return await AddRosterItemAsync(iqSender, jid, null, new string[] { });
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
            return await AddRosterItemAsync(iqSender, jid, null, new string[] { }, timeout, CancellationToken.None);
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
            return await AddRosterItemAsync(iqSender, jid, null, new string[] { }, timeout, cancellationToken);
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
            return await AddRosterItemAsync(iqSender, jid, nickname, new string[] { });
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
            return await AddRosterItemAsync(iqSender, jid, nickname, new string[] { }, timeout, CancellationToken.None);
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
            return await AddRosterItemAsync(iqSender, jid, nickname, new string[] { }, timeout, cancellationToken);
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
            return await AddRosterItemAsync(iqSender, jid, nickname, new[] { group });
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
            return await AddRosterItemAsync(iqSender, jid, nickname, new[] { group }, timeout, CancellationToken.None);
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
            return await AddRosterItemAsync(iqSender, jid, nickname, new[] { group }, timeout, cancellationToken);
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
            return await AddRosterItemAsync(iqSender, jid, nickname, group);
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
            return await AddRosterItemAsync(iqSender, jid, nickname, group, timeout, CancellationToken.None);
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
            return await AddRosterItemAsync(iqSender, jid, nickname, group, timeout, cancellationToken);
        }
        
        /// <summary>
        /// Remove a contact from the contact list, aka roster
        /// </summary>
        /// <param name="iqSender"><see cref="IClientIqSender"/></param>
        /// <param name="jid">The jid to remove</param>        
        /// <returns></returns>
        public static async Task<Iq> RemoveRosterItemAsync(this IClientIqSender iqSender, Jid jid)
        {
            return await RemoveRosterItemAsync(iqSender, jid, DefaultTimeout);
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
            return await iqSender.SendIqAsync(RosterBuilder.RemoveRosterItem(jid), timeout, CancellationToken.None);
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
            return await iqSender.SendIqAsync(RosterBuilder.RemoveRosterItem(jid), timeout, cancellationToken);
        }
    }
}
