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

namespace Matrix.Xmpp.SecurityLabels
{
    /// <summary>
    /// 
    /// </summary>
    [XmppTag(Name = Tag.Item, Namespace = Namespaces.SecurityLabelCatalog)]
    public class Item : XmppXElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item() : base(Namespaces.SecurityLabelCatalog, "item")
        {
        }

        /// <summary>
        /// Gets or sets the selector.
        /// The selector represents the item's placement in a hierarchical organization of the items.
        /// If one item has a selecto, all items should have a selector.
        /// The value of the selector conforms to the selector-value ABNF production:
        /// </summary>
        /// <value>
        /// The selector.
        /// </value>
        public string Selector
        {
            get { return GetAttribute("selector"); }
            set { SetAttribute("selector", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Item"/> is the default.
        /// One and only one of the items may be the default item. The client should default the label selection to this item in cases where the user has not selected an item.
        /// </summary>
        /// <value>
        ///   <c>true</c> if default; otherwise, <c>false</c>.
        /// </value>
        public bool Default
        {
            get { return GetAttributeBool("default"); }
            set { SetAttribute("default", value); }
        }

        /// <summary>
        /// Gets or sets the security label.
        /// </summary>
        /// <value>
        /// The security label.
        /// </value>
        public SecurityLabel SecurityLabel
        {
            get { return Element<SecurityLabel>(); }
            set { Replace(value); }
        }
    }
}
