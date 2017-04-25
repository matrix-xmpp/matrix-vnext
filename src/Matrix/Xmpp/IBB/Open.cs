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

namespace Matrix.Xmpp.IBB
{
    [XmppTag(Name = "open", Namespace = Namespaces.Ibb)]
    public class Open : Close
    {
        public Open() : base("open")
        {
        }

        internal Open(string ns, string tagname) : base(ns, tagname)
        {
        }

        /// <summary>
        /// Block size
        /// </summary>
        public long BlockSize
        {
            get { return GetAttributeLong("block-size"); }
            set { SetAttribute("block-size", value); }
        }

        /// <summary>
        /// Defines whether the data will be sent using iq stanzas or message stanzas.
        /// </summary>
        public StanzaType Stanza
        {
            get { return GetAttributeEnum<StanzaType>("stanza"); }
            set { SetAttribute("stanza", value.ToString().ToLower()); }
        }
    }
}
