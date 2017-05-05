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

namespace Matrix.Xmpp.Base
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Item : XmppXElementWithJidAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="ns">The ns.</param>
        protected Item(string ns) : base(ns, Tag.Item)
        {            
        }

        /// <summary>
        /// Gets the namespace.
        /// </summary>
        /// <value>The namespace.</value>
        internal string Namespace
        {
            get { return base.Name.Namespace.NamespaceName; }
        }
        
        /// <summary>
        /// Gets the name of this element.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// An <see cref="T:System.Xml.Linq.XName"/> that contains the name of this element.
        /// </returns>
        public new string Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }
    }
}
