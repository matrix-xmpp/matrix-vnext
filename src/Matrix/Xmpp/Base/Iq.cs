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

using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.XData;

namespace Matrix.Xmpp.Base
{
    /// <summary>
    /// Base Iq Stanza
    /// </summary>
    //[XmppTag(Name=Tag.Iq)]
    public abstract class Iq : XmppXElementWithAddressAndId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Iq"/> class.
        /// </summary>
        /// <param name="ns">The ns.</param>
        internal Iq(string ns) : base(ns, Tag.Iq)
        {            
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public IqType Type
        {
            get { return GetAttributeEnum<IqType>("type"); }
            set { SetAttribute("type", value.ToString().ToLower()); }
        }

        /// <summary>
        /// The query child of this Iq. Because the query tag can be in many different namespaces this
        /// member returns the first childnode which schould be the query in nearly all cases.
        /// Otherwise use XLinq routines to get the information.
        /// </summary>
        public XmppXElement Query
        {
            get { return Elements().FirstOrDefault() as XmppXElement; }
            set 
            {
                if (!Elements().Any())
                    Add(value);
            }
        }

        /// <summary>
        /// Gets or sets the Xdata object.
        /// </summary>
        /// <value>The X data.</value>
        public Data XData
        {
            get { return Element<Data>(); }
            set { Replace(value); }
        }
    }
}
