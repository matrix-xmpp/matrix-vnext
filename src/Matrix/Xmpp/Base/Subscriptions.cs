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
using Matrix.Xml;

namespace Matrix.Xmpp.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class Subscriptions : XmppXElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Subscriptions"/> class.
        /// </summary>
        /// <param name="ns">The ns.</param>
        internal protected Subscriptions(string ns) : base(ns, "subscriptions")
        {
        }

        /// <summary>
        /// Gets or sets the node.
        /// </summary>
        /// <value>The node.</value>
        public string Node
        {
            get { return GetAttributeJid("node"); }
            set { SetAttribute("node", value); }
        }


        /// <summary>
        /// Adds a subscription.
        /// </summary>
        /// <param name="subcription">The sub.</param>
        public void AddSubscription(Subscription subcription)
        {
            Add(subcription);
        }

        /// <summary>
        /// Sets the subscriptions.
        /// </summary>
        /// <param name="subcriptions">The subcriptions.</param>
        public void SetSubscriptions(IEnumerable<Subscription> subcriptions)
        {
            RemoveAllSubscriptions();
            foreach (Subscription sub in subcriptions)
                AddSubscription(sub);
        }

        /// <summary>
        /// Removes all subscriptions.
        /// </summary>
        public void RemoveAllSubscriptions()
        {
            RemoveAll<Subscription>();
        }
    }
}
