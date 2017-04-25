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

using System.Xml.Linq;

namespace Matrix.Xmpp.Base
{
    /// <summary>
    /// This class represents a XMPP Stanza
    /// </summary>
    public abstract class XmppXElementWithAddressAndId : XmppXElementWithAddress
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndId"/> class.
        /// </summary>
        /// <param name="ns">The ns.</param>
        /// <param name="tagname">The tagname.</param>
        protected XmppXElementWithAddressAndId(XNamespace ns, string tagname) : base(ns + tagname)
        {            
        }

        //// check
        //public DirectionalStanza(XNamespace ns, string tagname, object content)
        //    : base(ns + tagname, content)
        //{
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndId"/> class.
        /// </summary>
        /// <param name="ns">The ns.</param>
        /// <param name="tagname">The tagname.</param>
        protected XmppXElementWithAddressAndId(string ns, string tagname) : this("{" + ns + "}" + tagname)
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndId"/> class.
        /// </summary>
        /// <param name="ns">The ns.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="tagname">The tagname.</param>
        protected XmppXElementWithAddressAndId(string ns, string prefix, string tagname)
            : this("{" + ns + "}" + tagname, new XAttribute(XNamespace.Xmlns + prefix, ns))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndId"/> class.
        /// </summary>
        /// <param name="other">The other.</param>
        protected XmppXElementWithAddressAndId(XElement other) : base(other)
        {         
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndId"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected XmppXElementWithAddressAndId(XName name) : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndId"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="content">The content.</param>
        protected XmppXElementWithAddressAndId(XName name, object content)
            : base(name, content)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndId"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="content">The content.</param>
        protected XmppXElementWithAddressAndId(XName name, params object[] content)
            : base(name, content)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndId"/> class.
        /// </summary>
        /// <param name="other">An <see cref="T:System.Xml.Linq.XStreamingElement"/> that contains unevaluated queries that will be iterated for the contents of this <see cref="T:System.Xml.Linq.XElement"/>.</param>
        protected XmppXElementWithAddressAndId(XStreamingElement other)
            : base(other)            
        {
        }
        #endregion
                             
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public string Id
        {
            get { return GetAttribute("id"); }
            set { SetAttribute("id", value); }
        }

        /// <summary>
        /// Generates a automatic id for the packet.
        /// !!! Overwrites existing Ids
        /// </summary>
        /// <returns></returns>
        public void GenerateId()
        {
            string sId = Matrix.Id.GetNextId();
            Id = sId;
        }
    }
}
