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
using System.Xml.Linq;
using Matrix.Xml;

namespace Matrix.Xmpp.Base
{
    public abstract class XmppXElementWithAddressAndIdAttributeAndItemCollection<T> : XmppXElementWithAddressAndIdAttribute where T : XmppXElement, new()
    {
         #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndIdAttributeAndItemCollection{T}"/> class.
        /// </summary>
        /// <param name="ns">The ns.</param>
        /// <param name="tagname">The tagname.</param>
        protected XmppXElementWithAddressAndIdAttributeAndItemCollection(XNamespace ns, string tagname)
            : base(ns + tagname)
        {            
        }

        //// check
        //public DirectionalStanza(XNamespace ns, string tagname, object content)
        //    : base(ns + tagname, content)
        //{
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndIdAttributeAndItemCollection{T}" /> class.
        /// </summary>
        /// <param name="ns">The ns.</param>
        /// <param name="tagname">The tagname.</param>
        protected XmppXElementWithAddressAndIdAttributeAndItemCollection(string ns, string tagname)
            : this("{" + ns + "}" + tagname)
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndIdAttributeAndItemCollection{T}" /> class.
        /// </summary>
        /// <param name="ns">The ns.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="tagname">The tagname.</param>
        protected XmppXElementWithAddressAndIdAttributeAndItemCollection(string ns, string prefix, string tagname)
            : this("{" + ns + "}" + tagname, new XAttribute(XNamespace.Xmlns + prefix, ns))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndIdAttributeAndItemCollection{T}"/> class.
        /// </summary>
        /// <param name="other">The other.</param>
        protected XmppXElementWithAddressAndIdAttributeAndItemCollection(XElement other) : base(other)
        {         
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndIdAttributeAndItemCollection{T}"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected XmppXElementWithAddressAndIdAttributeAndItemCollection(XName name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndIdAttributeAndItemCollection{T}"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="content">The content.</param>
        protected XmppXElementWithAddressAndIdAttributeAndItemCollection(XName name, object content)
            : base(name, content)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndIdAttributeAndItemCollection{T}"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="content">The content.</param>
        protected XmppXElementWithAddressAndIdAttributeAndItemCollection(XName name, params object[] content)
            : base(name, content)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndIdAttributeAndItemCollection{T}"/> class.
        /// </summary>
        /// <param name="other">An <see cref="T:System.Xml.Linq.XStreamingElement"/> that contains unevaluated queries that will be iterated for the contents of this <see cref="T:System.Xml.Linq.XElement"/>.</param>
        protected XmppXElementWithAddressAndIdAttributeAndItemCollection(XStreamingElement other)
            : base(other)            
        {
        }
        #endregion
        
        #region << Item properties >>
        /// <summary>
        /// Adds an item.
        /// </summary>
        /// <returns></returns>
        public T AddItem()
        {
            var item = new T();
            Add(item);

            return item;
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void AddItem(T item)
        {
            Add(item);
        }

        ///<summary>
        /// Adds multiple items.
        ///</summary>
        ///<param name="items"></param>
        public void AddItems(T[] items)
        {
            foreach (var item in items)
                Add(item);
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetItems()
        {
            return Elements<T>();
        }

        /// <summary>
        /// Sets the Items.
        /// </summary>
        /// <param name="items">The Items.</param>
        public void SetItems(IEnumerable<T> items)
        {
            RemoveAllItems();
            foreach (T item in items)
                AddItem(item);
        }

        /// <summary>
        /// Removes all Items.
        /// </summary>
        public void RemoveAllItems()
        {
            RemoveAll<T>();
        }
        #endregion
    }
}
