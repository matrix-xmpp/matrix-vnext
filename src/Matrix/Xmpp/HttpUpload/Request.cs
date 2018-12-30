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
    [XmppTag(Name = "request", Namespace = Namespaces.HttpUpload)]
    public class Request : XmppXElement
    {
        public Request() : base(Namespaces.HttpUpload, "request")
        {
        }

        /// <summary>
        ///   Gets or sets the filename.
        /// </summary>
        /// <value>
        ///   The filename.
        /// </value>
        public string Filename
        {
            get { return GetAttribute("filename"); }
            set { SetAttribute("filename", value); }
        }

        /// <summary>
        ///   Gets or sets the size.
        /// </summary>
        /// <value>
        ///   The size.
        /// </value>
        public int Size
        {
            get { return GetAttributeInt("size"); }
            set { SetAttribute("size", value); }
        }

        /// <summary>
        ///   Gets or sets the content type.
        /// </summary>
        /// <value>
        ///   The content type.
        /// </value>
        public string ContentType
        {
            get { return GetAttribute("content-type"); }
            set { SetAttribute("content-type", value); }
        }     
    }
}
