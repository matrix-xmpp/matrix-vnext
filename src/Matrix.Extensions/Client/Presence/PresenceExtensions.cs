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

            await stanzaSender.SendAsync(pres);
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
            });
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
            });
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
            });
        }

        /// <summary>
        /// Send a presence update to the server. Use the properies Show, Status and priority to update presence information
        /// </summary>
        /// <param name="stanzaSender"><see cref="IStanzaSender"/></param>
        /// <returns></returns>
        public static async Task SendPresenceAsync(this IStanzaSender stanzaSender)
        {
            await stanzaSender.SendAsync(new Presence());
        }
    }
}
