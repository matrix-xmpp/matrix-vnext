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
using Matrix.Xml;

namespace Matrix.Xmpp.PubSub
{
    [XmppTag(Name = "publish", Namespace = Namespaces.Pubsub)]
    public class Publish : XmppXElement
    {
        internal Publish(string tag) : base(Namespaces.Pubsub, tag)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Publish"/> class.
        /// </summary>
        public Publish() : base(Namespaces.Pubsub, "publish")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Publish"/> class and adds the given item.
        /// </summary>
        /// <param name="item">The item.</param>
        public Publish(Item item): this()
        {
            AddItem(item);
        }

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
        /// Adds an Item.
        /// </summary>
        /// <returns></returns>
        public Item AddItem()
        {
            var item = new Item();
            Add(item);

            return item;
        }

        /// <summary>
        /// Adds an Item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void AddItem(Item item)
        {
            Add(item);
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Item> GetItems()
        {
            return Elements<Item>();
        }
    }
}
