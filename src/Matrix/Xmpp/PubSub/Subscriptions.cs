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

using System.Collections.Generic;
using Matrix.Attributes;

namespace Matrix.Xmpp.PubSub
{
    /// <summary>
    /// 
    /// </summary>
    [XmppTag(Name = "subscriptions", Namespace = Namespaces.Pubsub)]
    public class Subscriptions : Base.Subscriptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Subscriptions"/> class.
        /// </summary>
        public Subscriptions()
            : base(Namespaces.Pubsub)
        {
        }

        /// <summary>
        /// Adds a subscription.
        /// </summary>
        /// <returns></returns>
        public Subscription AddSubscription()
        {
            var sub = new Subscription();
            Add(sub);

            return sub;
        }

        /// <summary>
        /// Gets the subscriptions.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Subscription> GetSubscriptions()
        {
            return Elements<Subscription>();
        }
    }
}
