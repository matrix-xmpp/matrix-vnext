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

namespace Matrix.Xmpp.ExtendedStanzaAddressing
{
    [XmppTag(Name = "address", Namespace = Namespaces.ExtendedStanzaAdressing)]
    public class Address : XmppXElement
    {
        public Address() : base(Namespaces.ExtendedStanzaAdressing, "address")
        {
        }

        /// <summary>
        /// Is used to specify a simple Jabber ID associated with this address. 
        /// If the Jid is specified, the Uri proprty MUST NOT be set.
        /// </summary>
        public Jid Jid
        {
            get { return GetAttributeJid("jid"); }
            set { SetAttribute("jid", value); }
        }

        /// <summary>
        /// The Description is used to specify human-readable information for this address. 
        /// This data may be used by clients to provide richer address-book integration. 
        /// </summary>
        public string Description
        {
            get { return GetAttribute("desc"); }
            set { SetAttribute("desc", value); }
        }
        
        /// <summary>
        /// This property is used to specify a sub-addressable unit at a particular JID, corresponding to a Service Discovery node.
        /// A node property MAY be set if a 'Jid' is specified. If a 'Uri' is specified, the 'Node' MUST NOT be specified.
        /// </summary>
        public string Node
        {
            get { return GetAttribute("node"); }
            set { SetAttribute("node", value); }
        }
        
        public Type Type
        {
            get { return GetAttributeEnum<Type>("type"); }
            set { SetAttribute("type", value.ToString().ToLower()); }
        }

        /// <summary>
        /// Is used to specify an external system address, such as a sip:, sips:, or im: URI.
        /// If the Uri is set, the Jid and Node peroperties MUST NOT be specified.
        /// </summary>
        public System.Uri Uri
        {
            get { return new System.Uri(GetAttribute("uri")); }
            set { SetAttribute("uri", value.ToString()); }
        }

        /// <summary>
        /// When a multicast service delivers the stanza to a non-bcc address, it MUST set Delivered = true.
        /// A multicast service MUST NOT deliver to an address that was marked with Delivered = true
        /// when the service received the stanza. 
        /// A multicast service SHOULD attempt to deliver to all addresses that are not marked with Delivered = true.
        /// </summary>
        public bool Delivered
        {
            get { return HasAttribute("delivered") && GetAttribute("delivered") == "true"; }
            set
            {
                if (value)
                    SetAttribute("delivered", "true");
                else
                    RemoveAttribute("delivered");
            }
        }
    }
}
