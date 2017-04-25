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

namespace Matrix.Xmpp.Shim
{
    /// <summary>
    ///<a href="http://xmpp.org/extensions/xep-0131.html">XEP-0131</a> SHIM Header
    /// </summary>
    [XmppTag(Name = "header", Namespace = Namespaces.Shim)]
    public class Header : XmppXElement
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Header"/> class.
        /// </summary>
        public Header(): base(Namespaces.Shim, "header")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Header"/> class.
        /// </summary>
        /// <param name="name">The header name.</param>
        /// <param name="val">The header value.</param>
        public Header(HeaderNames name, string val = null)
            : this(name.GetName(), val) { }
        

        /// <summary>
        /// Initializes a new instance of the <see cref="Header"/> class.
        /// </summary>
        /// <param name="name">The header name.</param>
        /// <param name="val">The header value.</param>
        public Header(string name, string val = null) 
            : this()      
		{
			Name	= name;

            if (!string.IsNullOrEmpty(val))
			    Value	= val;
		}
        #endregion

        /// <summary>
        /// Gets the name of this header.
        /// </summary>
        /// <value>The header name.</value>
        public new string Name
		{
			get { return GetAttribute("name"); }
			set { SetAttribute("name", value); }
		}
    }
}
