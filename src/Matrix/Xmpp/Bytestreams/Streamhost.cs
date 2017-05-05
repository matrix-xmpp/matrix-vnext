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

namespace Matrix.Xmpp.Bytestreams
{
    /*
      <xs:element name='streamhost'>
        <xs:complexType>
          <xs:simpleContent>
            <xs:extension base='empty'>
              <xs:attribute name='jid' type='xs:string' use='required'/>
              <xs:attribute name='host' type='xs:string' use='required'/>
              <xs:attribute name='zeroconf' type='xs:string' use='optional'/>
              <xs:attribute name='port' type='xs:string' use='optional'/>
            </xs:extension>
          </xs:simpleContent>
        </xs:complexType>
      </xs:element>
    */
    [XmppTag(Name = "streamhost", Namespace = Namespaces.Bytestreams)]
    public class Streamhost : StreamhostUsed
    {
        public Streamhost() : base("streamhost")
        {
        }

        /// <summary>
        /// the hostname or IP address of the StreamHost for SOCKS5 communications over TCP
        /// </summary>
        public string Host
        {
            get { return GetAttribute("host");}
            set { SetAttribute("host", value);}
        }

        /// <summary>
        /// specifies the zero-configuration service available for bytestreaming.
        /// This value SHOULD be present. The value SHOULD be '_jabber.bytestreams'.
        /// </summary>
        public string Zeroconf
        {
            get { return GetAttribute("zeroconf"); }
            set { SetAttribute("zeroconf", value); }
        }

        /// <summary>
        /// a port associated with the hostname or IP address for SOCKS5 communications over TCP
        /// </summary>
        public int Port
        {
            get { return GetAttributeInt("port"); }
            set { SetAttribute("port", value); }
        }
    }
}
