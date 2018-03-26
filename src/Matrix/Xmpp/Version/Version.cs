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

namespace Matrix.Xmpp.Version
{
    /// <summary>
    /// XEP-0092: Software Version
    /// </summary>
    [XmppTag(Name = Tag.Query, Namespace = Namespaces.IqVersion)]
    public class Version : XmppXElement
    {
        public Version() : base(Namespaces.IqVersion, Tag.Query)
        {
        }

        //
        /// <summary>
        /// Gets the name of the software product.
        /// </summary>
        /// <returns>An <see cref="T:System.Xml.Linq.XName" /> that contains the name of this element.</returns>
        public new string Name
        {
            set { SetTag("name", value); }
            get { return GetTag("name"); }
        }

        /// <summary>
        /// Gets or sets the software version information.
        /// </summary>
        /// <value>
        /// The ver.
        /// </value>
        public string Ver
        {
            set { SetTag("version", value); }
            get { return GetTag("version"); }
        }

        /// <summary>
        /// Gets or sets the operating system of the software.
        /// </summary>
        /// <value>
        /// The os.
        /// </value>
        public string Os
        {
            set { SetTag("os", value); }
            get { return GetTag("os"); }
        }
    }
}
