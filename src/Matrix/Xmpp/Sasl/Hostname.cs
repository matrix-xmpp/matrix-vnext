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

namespace Matrix.Xmpp.Sasl
{
    /// <summary>
    /// Hostname for XEP-0233: Domain-Based Service Names in XMPP SASL Negotiation.
    /// </summary>
    [XmppTag(Name = "hostname", Namespace = Namespaces.SaslHostname0)]
    [XmppTag(Name = "hostname", Namespace = Namespaces.SaslHostname1)]
    public class Hostname : XmppXElement
    {
        #region Xml sample
        /*
            <mechanisms xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>
              <mechanism>GSSAPI</mechanism>
              <mechanism>DIGEST-MD5</mechanism>
              <hostname xmlns='urn:xmpp:domain-based-name:1'>auth42.us.example.com</hostname>
            </mechanisms> 
        */
        #endregion

        public Hostname() : base(Namespaces.SaslHostname1, "hostname")
        {
        }
    }
}
