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

using Matrix.Attributes;

namespace Matrix.Xmpp.PubSub
{
    public enum SubscriptionState
    {
        /// <summary>
        /// The node MUST NOT send event notifications or payloads to the Entity.
        /// </summary>
        [Name("none")]
        None,

        /// <summary>
        /// An entity has requested to subscribe to a node and the request 
        /// has not yet been approved by a node owner. The node MUST NOT 
        /// send event notifications or payloads to the entity while it 
        /// is in this state.
        /// </summary>
        [Name("pending")]
        Pending,

        /// <summary>
        /// An entity has subscribed but its subscription options have not yet 
        /// been configured. The node MAY send event notifications or payloads 
        /// to the entity while it is in this state. 
        /// The service MAY timeout unconfigured subscriptions.
        /// </summary>
        [Name("unconfigured")]
        Unconfigured,

        /// <summary>
        /// An entity is subscribed to a node. The node MUST send all event 
        /// notifications (and, if configured, payloads) to the entity while 
        /// it is in this state (subject to subscriber configuration and 
        /// content filtering).
        /// </summary>
        [Name("subscribed")]
        Subscribed
    }
}
