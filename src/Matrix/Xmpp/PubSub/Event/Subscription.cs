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

namespace Matrix.Xmpp.PubSub.Event
{
    [XmppTag(Name = "subscription", Namespace = Namespaces.PubsubEvent)]
    public class Subscription : Base.Subscription
    {
        #region Schema
        /*
          <xs:element name='subscription'>
            <xs:complexType>
              <xs:simpleContent>
                <xs:extension base='empty'>
                  <xs:attribute name='expiry' type='xs:dateTime' use='optional'/>
                  <xs:attribute name='jid' type='xs:string' use='required'/>
                  <xs:attribute name='node' type='xs:string' use='optional'/>
                  <xs:attribute name='subid' type='xs:string' use='optional'/>
                  <xs:attribute name='subscription' use='optional'>
                    <xs:simpleType>
                      <xs:restriction base='xs:NCName'>
                        <xs:enumeration value='none'/>
                        <xs:enumeration value='pending'/>
                        <xs:enumeration value='subscribed'/>
                        <xs:enumeration value='unconfigured'/>
                      </xs:restriction>
                    </xs:simpleType>
                  </xs:attribute>
                </xs:extension>
              </xs:simpleContent>
            </xs:complexType>
          </xs:element>
        */
        #endregion
        
        public Subscription() : base(Namespaces.PubsubEvent)
        {
        }

        public DateTime Expiry
        {
            get { return Matrix.Time.Iso8601Date(GetAttribute("expiry")); }    
            set { SetAttribute("expiry", Matrix.Time.Iso8601Date(value)); }
        }

        /// <summary>
        /// Gets or sets the node.
        /// </summary>
        /// <value>The node.</value>
        public string Node
        {
            get { return GetAttribute("node"); }
            set { SetAttribute("node", value); }
        }
    }
}
