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
using System.Net;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Jingle.Candidates
{
    [XmppTag(Name = "candidate", Namespace = Namespaces.JingleTransportRawUdp)]
    public class CandidateRawUdp : XmppXElement
    {
        #region < XML schema >>
        /*
         <xs:complexType name='candidateElementType'>
            <xs:simpleContent>
              <xs:extension base='empty'>
                <xs:attribute name='component' type='xs:unsignedByte' use='required'/>
                <xs:attribute name='generation' type='xs:unsignedByte' use='required'/>
                <xs:attribute name='id' type='xs:NCName' use='required'/>
                <xs:attribute name='ip' type='xs:string' use='required'/>
                <xs:attribute name='port' type='xs:unsignedShort' use='required'/>
                <xs:attribute name='type' use='required'>
                  <xs:simpleType>
                    <xs:restriction base='xs:NCName'>
                      <xs:enumeration value='host'/>
                      <xs:enumeration value='prflx'/>
                      <xs:enumeration value='relay'/>
                      <xs:enumeration value='srflx'/>
                    </xs:restriction>
                  </xs:simpleType>
                </xs:attribute>
              </xs:extension>
            </xs:simpleContent>
          </xs:complexType>
        */
        #endregion
        internal CandidateRawUdp(string ns) : base(ns, "candidate")
        {
            
        }
        public CandidateRawUdp() : this(Namespaces.JingleTransportRawUdp)
        {
        }

        public string Id
        {
            get { return GetAttribute("id"); }
            set { SetAttribute("id", value); }
        }

        public int Component
        {
            get { return GetAttributeInt("component"); }
            set { SetAttribute("component", value); }
        }

        public int Generation
        {
            get { return GetAttributeInt("generation"); }
            set { SetAttribute("generation", value); }
        }

        public int Port
        {
            get { return GetAttributeInt("port"); }
            set { SetAttribute("port", value); }
        }

        public CandidateType Type
        {
            get { return GetAttributeEnum<CandidateType>("type"); }
            set { SetAttribute("type", value.ToString().ToLower()); }
        }

        public IPAddress IPAddress
        {
            get { return GetAttributeIPAddress("ip"); }
            set { SetAttribute("ip", value.ToString()); }
        }

        /// <summary>
        /// generates a new unique Sid
        /// </summary>
        public void GenerateId()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
