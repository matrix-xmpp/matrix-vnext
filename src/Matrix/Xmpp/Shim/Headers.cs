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
using System.Linq;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Shim
{
    /// <summary>
    /// <a href="http://xmpp.org/extensions/xep-0131.html">XEP-0131</a> Stanza Header collection onject
    /// </summary>
    [XmppTag(Name = "headers", Namespace = Namespaces.Shim)]
    public class Headers : XmppXElement
    {
        // <headers xmlns='http://jabber.org/protocol/shim'>
        //	 <header name='In-Reply-To'>123456789@capulet.com</header>
        // <header name='Keywords'>shakespeare,&lt;xmpp/&gt;</header>
        // </headers>
        /// <summary>
        /// Initializes a new instance of the <see cref="Headers"/> class.
        /// </summary>
        public Headers()
            : base(Namespaces.Shim, "headers")
        {
        }

        /// <summary>
        /// Adds a new Header
        /// </summary>
        /// <returns>a new Header.</returns>
        public Header AddHeader()
        {
            var h = new Header();
            Add(h);
            return h;
        }

        /// <summary>
        /// Adds the given Header
        /// </summary>
        /// <param name="header">The header.</param>
        /// <returns>returns the given Header</returns>
        public Header AddHeader(Header header)
        {
            Add(header);
            return header;
        }
        
        public Header AddHeader(HeaderNames name, string val = null)
        {
            return AddHeader(name.GetName(), val);
        }

        /// <summary>
        /// Adds a new Header
        /// </summary>
        /// <param name="name">header name</param>
        /// <param name="val">header value</param>
        /// <returns>returns the new added header</returns>
        public Header AddHeader(string name, string val = null)
        {
            var header = new Header(name, val);
            Add(header);
            return header;
        }

        /// <summary>
        /// Sets the header.
        /// </summary>
        /// <param name="name">The header name.</param>
        /// <param name="val">The header value.</param>
        public void SetHeader(HeaderNames name, string val = null)
        {
            SetHeader(name.GetName(), val);
        }

        /// <summary>
        /// Sets the header.
        /// </summary>
        /// <param name="name">The header name.</param>
        /// <param name="val">The header value.</param>
        public void SetHeader(string name, string val = null)
        {
            var header = GetHeader(name);
            if (header != null)
                header.Value = val;
            else
                AddHeader(name, val);
        }

        /// <summary>
        /// Gets the <see cref="Header"/> with the specified name.
        /// </summary>
        /// <param name="name">The header name.</param>
        /// <returns>the Header.</returns>
        public Header this[HeaderNames name] => HasHeader(name.GetName()) ? GetHeader(name.GetName()) : AddHeader(name.GetName());

        /// <summary>
        /// Gets the <see cref="Header"/> with the specified name.
        /// </summary>
        /// <param name="name">The header name.</param>
        /// <returns>the Header.</returns>
        public Header this[string name]
        {
            get { return HasHeader(name) ? GetHeader(name) : AddHeader(name); }
        }

        /// <summary>
        /// Determines whether a header with the specified name exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns><c>true</c> if the header exists; otherwise, <c>false</c>.</returns>
        public bool HasHeader(HeaderNames name)
        {
            return HasHeader(name.GetName());
        }

        /// <summary>
        /// Determines whether a header with the specified name exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns><c>true</c> if the header exists; otherwise, <c>false</c>.</returns>
        public bool HasHeader(string name)
        {
            return Elements<Header>().Any(h => h.Name == name);
        }

        /// <summary>
        /// Gets a value indicating whether this instance has any headers.
        /// </summary>
        /// <value><c>true</c> if this instance has headers; otherwise, <c>false</c>.</value>
        public bool HasHeaders => Elements<Header>() != null;

        /// <summary>
        /// Gets the header with the given header name.
        /// </summary>
        /// <param name="name">The header name.</param>
        /// <returns>Header.</returns>
        public Header GetHeader(HeaderNames name)
        {
            return GetHeader(name.GetName());
        }

        /// <summary>
        /// Gets the header with the given header name.
        /// </summary>
        /// <param name="name">The header name.</param>
        /// <returns>Header.</returns>
        public Header GetHeader(string name)
        {
            return Elements<Header>().FirstOrDefault(h => h.Name == name);
        }

        /// <summary>
        /// Gets all headers.
        /// </summary>
        /// <returns>IEnumerable&lt;Header&gt;.</returns>
        public IEnumerable<Header> GetHeaders()
        {
            return Elements<Header>();
        }
    }
}
