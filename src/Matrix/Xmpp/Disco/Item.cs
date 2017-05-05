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

namespace Matrix.Xmpp.Disco
{
    /// <summary>
    /// 
    /// </summary>
    [XmppTag(Name = Tag.Item, Namespace = Namespaces.DiscoItems)]
    public class Item : XmppXElement
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item()
            : base(Namespaces.DiscoItems, "item")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="jid">The jid.</param>
        public Item(Jid jid)
            : this()
        {
            Jid = jid;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="jid">The jid.</param>
        /// <param name="node">The node.</param>
        public Item(Jid jid, string node)
            : this(jid)
        {
            Node = node;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="jid">The jid.</param>
        /// <param name="node">The node.</param>
        /// <param name="name">The name.</param>
        public Item(Jid jid, string node, string name)
            : this(jid, node)
        {
            Name = name;
        }
        #endregion

        /// <summary>
        /// Gets or sets the Jid.
        /// </summary>
        /// <value>The Jid.</value>
        public Jid Jid
        {
            get { return new Jid(GetAttribute("jid")); }
            set { SetAttribute("jid", value.ToString()); }
        }

        /// <summary>
        /// Gets the name of this element.
        /// </summary>
        /// <value>The name</value>        
        public new string Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
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
    }
}
