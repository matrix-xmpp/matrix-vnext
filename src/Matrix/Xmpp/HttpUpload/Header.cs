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

namespace Matrix.Xmpp.HttpUpload
{
    [XmppTag(Name = "header", Namespace = Namespaces.HttpUpload)]
    public class Header : XmppXElement
    {
        public Header() : base(Namespaces.HttpUpload, "header")
        {
        }

        public Header(HeaderNames headerName, string val) : base(Namespaces.HttpUpload, "header")
        {
            this.HeaderName = headerName;
            this.Value = val;
        }

        /// <summary>
        ///   Gets or sets the name of the header.
        /// </summary>
        /// <value>
        ///   The header name.
        /// </value>
        public HeaderNames HeaderName
        {
            get { return GetAttributeEnumUsingNameAttrib<HeaderNames>("name"); }
            set { SetAttribute("name", value.GetName()); }
        }
    }
}
