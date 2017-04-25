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

namespace Matrix.Xmpp.PubSub
{
    [XmppTag(Name = "subscription", Namespace = Namespaces.Pubsub)]
    public class Subscription : Base.Subscription
    {
        #region Xml sample
        /*
         * Example 31. Service replies with success

            <iq type='result'
                from='pubsub.shakespeare.lit'
                to='francisco@denmark.lit/barracks'
                id='sub1'>
              <pubsub xmlns='http://jabber.org/protocol/pubsub'>
                <subscription
                    node='princely_musings'
                    jid='francisco@denmark.lit'
                    subid='ba49252aaa4f5d320c24d3766f0bdcade78c78d3'
                    subscription='subscribed'/>
              </pubsub>
            </iq>
        */
        #endregion

        #region Schema
        /* 
           <xs:element name='subscription'>
                <xs:complexType>
                  <xs:sequence>
                    <xs:element ref='subscribe-options' minOccurs='0'/>
                  </xs:sequence>
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
                </xs:complexType>
              </xs:element>
         */
        #endregion

        #region << Constructors >>
        public Subscription()
            : base(Namespaces.Pubsub)
        {
        }
        #endregion

        /// <summary>
        /// Gets or sets the node.
        /// </summary>
        /// <value>The node.</value>
        public string Node
        {
            get { return GetAttribute("node"); }
            set { SetAttribute("node", value); }
        }
       
        /// <summary>
        /// Gets or sets the subscribe options.
        /// </summary>
        /// <value>The subscribe options.</value>
        public SubscribeOptions SubscribeOptions
        {
            get { return Element<SubscribeOptions>(); }
            set { Replace(value); }
        }
    }
}
