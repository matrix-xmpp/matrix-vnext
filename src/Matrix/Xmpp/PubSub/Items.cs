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
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.PubSub
{
    [XmppTag(Name = "items", Namespace = Namespaces.Pubsub)]
    public class Items : XmppXElementWithItemCollection<Item>
    {
        #region << Constrictors >>
        public Items() : base(Namespaces.Pubsub, "items")
        {
        }
        #endregion

        /// <summary>
        /// Gets or sets the node.
        /// </summary>
        /// <value>The node.</value>
        public string Node
        {
            get { return GetAttribute("node"); }
            set { SetAttribute("node", value); }
        }

        /// <summary>
        /// Gets or sets the subscription id.
        /// </summary>
        /// <value>The subscription id.</value>
        public string Id
        {
            get { return GetAttribute("subid"); }
            set { SetAttribute("subid", value); }
        }
        
        /// <summary>
        /// A service MAY allow entities to request the most recent N items. 
        /// When max_items is used, implementations SHOULD return the N most recent 
        /// (as opposed to the N oldest) items.
        /// </summary>
        /// <value>The max items.</value>
        public int MaxItems
        {
            get { return GetAttributeInt("max_items"); }
            set { SetAttribute("max_items", value); }
        }
    }
}
