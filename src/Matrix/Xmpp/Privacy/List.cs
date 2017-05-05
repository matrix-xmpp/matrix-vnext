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

namespace Matrix.Xmpp.Privacy
{
    /// <summary>
    /// 
    /// </summary>
    [XmppTag(Name = "list", Namespace = Namespaces.IqPrivacy)]
    public class List : Base.XmppXElementWithNameAttribute
    {
        public List() : base(Namespaces.IqPrivacy, "list")
        {
        }

        public List(string name)
            : this()
        {
            Name = name;
        }
        
        /// <summary>
        /// Gets all Rules (Items) when available
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Item> GetItems()
        {
            return Elements<Item>();
        }

        /// <summary>
        /// Adds an item.
        /// </summary>
        /// <returns></returns>
        public Item AddItem()
        {
            var item = new Item();
            Add(item);

            return item;
        }

        /// <summary>
        /// Add Item
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            Add(item);
        }

        /// <summary>
        /// Add multiple Items
        /// </summary>
        /// <param name="items"></param>
        public void AddItems(Item[] items)
        {
            foreach (Item item in items)
                Add(item);
        }

        /// <summary>
        /// Sets the Items.
        /// </summary>
        /// <param name="items">The Items.</param>
        public void SetItems(IEnumerable<Item> items)
        {
            RemoveAllItems();
            foreach (var item in items)
                AddItem(item);
        }

        /// <summary>
        /// Remove all items/rules of this list
        /// </summary>
        public void RemoveAllItems()
        {
            RemoveAll<Item>();
        }
    }
}
