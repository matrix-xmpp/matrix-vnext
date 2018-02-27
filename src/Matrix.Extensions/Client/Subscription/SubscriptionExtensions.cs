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

namespace Matrix.Extensions.Client.Subscription
{
    using Matrix.Xmpp;
    using Matrix.Xmpp.Client;
    using System;
    using System.Threading.Tasks;

    public static class SubscriptionExtensions
    {
        /// <summary>
        /// Approve a subscription request
        /// </summary>
        /// <param name="stanzaSender"><see cref="IStanzaSender"/></param>
        /// <param name="to">The <see cref="Jid"/> to approve</param>
        /// <returns></returns>
        public static async Task ApproveSubscriptionRequestAsync(this IStanzaSender stanzaSender, Jid to)
        {
            var pres = new Presence { Type = PresenceType.Subscribed, To = to };

            await stanzaSender.SendAsync(pres);
        }

        /// <summary>
        /// Deny a subscription request
        /// </summary>
        /// <param name="stanzaSender"><see cref="IStanzaSender"/></param>
        /// <param name="to">The <see cref="Jid"/> to deny</param>
        /// <returns></returns>
        public static async Task DenySubscriptionRequestAsync(this IStanzaSender stanzaSender, Jid to)
        {
            var pres = new Presence { Type = PresenceType.Unsubscribed, To = to };

            await stanzaSender.SendAsync(pres);
        }

        /// <summary>
        /// Send a request to subscribe to a users's presence
        /// </summary>
        /// <param name="stanzaSender"><see cref="IStanzaSender"/></param>
        /// <param name="to">The users bare jid we want to subscribe to</param>
        /// <param name="message">optional message we want to send to the user with the request</param>
        /// <param name="nickname">our nickname which we can send optional to the user</param>
        /// <returns></returns>
        public static async Task SubscribeAsync(this IStanzaSender stanzaSender, Jid to, string message = null, string nickname = null)
        {
            var pres = new Presence { Type = PresenceType.Subscribe, To = to };

            if (!String.IsNullOrEmpty(nickname))
                pres.Nick = nickname;

            if (!String.IsNullOrEmpty(message))
                pres.Status = message;

            await stanzaSender.SendAsync(pres);
        }

        /// <summary>
        /// Cancel a previously-granted subscription request to a user    
        /// </summary>
        /// <param name="stanzaSender"><see cref="IStanzaSender"/></param>
        /// <param name="to">The <see cref="Jid"/> to cancel</param>
        /// <returns></returns>
        public static async Task CancelSubscriptionAsync(this IStanzaSender stanzaSender, Jid to)
        {
            await DenySubscriptionRequestAsync(stanzaSender, to);
        }

        /// <summary>
        /// Unsubscribe from the presence of a user
        /// </summary>
        /// <param name="stanzaSender"><see cref="IStanzaSender"/></param>
        /// <param name="to">The <see cref="Jid"/> to unsubscribe</param>
        /// <returns></returns>
        public static async Task UnsubscribeAsync(this IStanzaSender stanzaSender, Jid to)
        {
            var pres = new Presence { Type = PresenceType.Unsubscribe, To = to };

            await stanzaSender.SendAsync(pres);
        }
    }
}
