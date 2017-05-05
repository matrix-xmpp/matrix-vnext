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

using System;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Delay
{
    /// <summary>
    /// XEP-0091: Delayed Delivery
    /// </summary>
    [XmppTag(Name = "x", Namespace = Namespaces.XDelay)]
    public class XDelay : XmppXElement
    {
        /*
         * <x xmlns="jabber:x:delay" from="lynx@ve.symlynx.com" stamp="20090222T18:39:51" />         
         */
        #region << Constructors >>>
        /// <summary>
        /// Initializes a new instance of the <see cref="XDelay"/> class.
        /// </summary>
        public XDelay()
            : base(Namespaces.XDelay, "x")
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XDelay"/> class.
        /// </summary>
        /// <param name="stamp">The stamp.</param>
        public XDelay(DateTime stamp)
            : this()
        {
            Stamp = stamp;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XDelay"/> class.
        /// </summary>
        /// <param name="stamp">The stamp.</param>
        /// <param name="from">From.</param>
        public XDelay(DateTime stamp, Jid from)
            : this(stamp)
        {
            From = from;
        }
        #endregion

        /// <summary>
        /// The Jabber ID of the entity that originally sent the XML stanza or that delayed the delivery of the stanza 
        /// (for example, the address of a multi-user chat room).
        /// </summary>
        /// <value>From.</value>
        public Jid From
        {
            get { return GetAttributeJid("from"); }
            set { SetAttribute("from", value); }
        }

        /// <summary>
        /// Gets or sets the stamp. The time when the XML stanza was originally sent.
        /// </summary>
        /// <value>The stamp.</value>
        public DateTime Stamp
        {
            get { return Matrix.Time.JabberDate(GetAttribute("stamp")); }
            set { SetAttribute("stamp", Matrix.Time.JabberDate(value)); }
        }
    }
}
