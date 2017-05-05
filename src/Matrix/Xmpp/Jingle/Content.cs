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
using Matrix.Xmpp.Jingle.Apps.Rtp;
using Matrix.Xmpp.Jingle.Transports;

namespace Matrix.Xmpp.Jingle
{
    [XmppTag(Name = "content", Namespace = Namespaces.Jingle)]
    public class Content : XmppXElement
    {
        #region << XML schema >>
        /*
         <xs:complexType name='contentElementType'>
            <xs:sequence>
              <xs:any namespace='##other' minOccurs='0' maxOccurs='unbounded'/>
            </xs:sequence>
            <xs:attribute name='creator'
                          use='required'>
              <xs:simpleType>
                <xs:restriction base='xs:NCName'>
                  <xs:enumeration value='initiator'/>
                  <xs:enumeration value='responder'/>
                </xs:restriction>
              </xs:simpleType>
            </xs:attribute>
            <xs:attribute name='disposition'
                          use='optional'
                          type='xs:NCName'
                          default='session'/>
            <xs:attribute name='name'
                          use='required'
                          type='xs:string'/>
            <xs:attribute name='senders'
                          use='optional'
                          default='both'>
              <xs:simpleType>
                <xs:restriction base='xs:NCName'>
                  <xs:enumeration value='both'/>
                  <xs:enumeration value='initiator'/>
                  <xs:enumeration value='none'/>
                  <xs:enumeration value='responder'/>
                </xs:restriction>
              </xs:simpleType>
            </xs:attribute>
          </xs:complexType>
        */
        #endregion

        public Content() : base(Namespaces.Jingle, "content")
        {}

        /// <summary>
        /// Which party originally generated the content type (used to prevent race conditions regarding modifications);.
        /// The defined values are "Initiator" and "Responder" (where the default is "Initiator").
        /// The value of the 'creator' for a given content type MUST always match the party that originally generated the content type, 
        /// even for Jingle actions that are sent by the other party in relation to that content type
        ///  (e.g., subsequent content-modify or transport-info messages). 
        /// The combination of the 'creator' attribute and the 'name' attribute is unique among both parties to a Jingle session.
        /// </summary>
        public Creator Creator
        {
            get { return GetAttributeEnum<Creator>("creator"); }
            set { SetAttribute("creator", value.ToString().ToLower()); }
        }

        /// <summary>
        /// A unique name or identifier for the content type according to the creator, which MAY have meaning to a human user in order
        /// to differentiate this content type from other content types 
        /// (e.g., two content types containing video media could differentiate between "room-pan" and "slides"). 
        /// If there are two content types with the same value for the 'name' attribute, they shall understood as alternative
        ///  definitions for the same purpose (e.g., a legacy method and a standards-based method for establishing a voice call), 
        /// typically to smooth the transition from an older technology to Jingle.
        /// </summary>
        public new string Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }

        /// <summary>
        /// Which parties in the session will be generating content; the allowable values are 
        /// "initiator", "none", "responder", and "both" (where the default is "both").
        /// </summary>
        public Senders Senders
        {
            get { return GetAttributeEnum<Senders>("senders"); }
            set { SetAttribute("senders", value.ToString().ToLower()); }
        }

        /// <summary>
        /// ow the content definition is to be interpreted by the recipient.
        /// The meaning of this Property matches the "Content-Disposition" header as defined in RFC 2183 and applied to SIP by RFC 3261. 
        /// </summary>
        public Disposition Disposition
        {
            get { return GetAttributeEnumUsingNameAttrib<Disposition>("disposition"); }
            set { SetAttribute("disposition", value.GetName()); }
        }
        
        public TransportIceUdp TransportIceUdp
        {
            get { return Element<TransportIceUdp>(); }
            set { Replace(value); }
        }

        public TransportRawUdp TransportRawUdp
        {
            get { return Element<TransportRawUdp>(); }
            set { Replace(value); }
        }

        public Description Description
        {
            get { return Element<Description>(); }
            set { Replace(value); }
        }
    }
}
