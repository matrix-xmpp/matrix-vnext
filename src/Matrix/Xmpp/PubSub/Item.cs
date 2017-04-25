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
using Matrix.Xml;

namespace Matrix.Xmpp.PubSub
{
    [XmppTag(Name = Tag.Item, Namespace = Namespaces.Pubsub)]
    public class Item : XmppXElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item() : base(Namespaces.Pubsub, "item")
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class and adds the given payload.
        /// </summary>
        /// <param name="payload">The payload.</param>
        public Item(XmppXElement payload) : this()
        {
            Add(payload);
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public string Id
        {
            get { return GetAttribute("id"); }
            set { SetAttribute("id", value); }
        }
    }
}
