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

namespace Matrix.Xmpp.MessageArchiving
{
    [XmppTag(Name = "default", Namespace = Namespaces.Archiving)]
    public class Default : XmppXElement
    {
        #region Schema
        /*
          <xs:element name='default'>
            <xs:complexType>
              <xs:simpleContent>
                <xs:extension base='empty'>
                  <xs:attribute name='expire' type='xs:nonNegativeInteger' use='optional'/>
                  <xs:attribute name='otr' use='required'>
                    <xs:simpleType>
                      <xs:restriction base='xs:NCName'>
                        <xs:enumeration value='approve'/>
                        <xs:enumeration value='concede'/>
                        <xs:enumeration value='forbid'/>
                        <xs:enumeration value='oppose'/>
                        <xs:enumeration value='prefer'/>
                        <xs:enumeration value='require'/>
                      </xs:restriction>
                    </xs:simpleType>
                  </xs:attribute>
                  <xs:attribute name='save' use='required'>
                    <xs:simpleType>
                      <xs:restriction base='xs:NCName'>
                        <xs:enumeration value='body'/>
                        <xs:enumeration value='false'/>
                        <xs:enumeration value='message'/>
                        <xs:enumeration value='stream'/>
                      </xs:restriction>
                    </xs:simpleType>
                  </xs:attribute>
                  <xs:attribute name='unset' use='optional' type='xs:boolean'/>
                </xs:extension>
              </xs:simpleContent>
            </xs:complexType>
          </xs:element>
         */
        #endregion
       
        public Default() : base(Namespaces.Archiving, "default")
        {
        }

        /// <summary>
        /// Specifies the user's default setting for OTR Mode.
        /// </summary>
        public OtrType Otr
        {
            get { return GetAttributeEnum<OtrType>("otr"); }
            set { SetAttribute("otr", value.ToString().ToLower()); }
        }

        /// <summary>
        /// Specifies the user's default setting for Save Mode
        /// </summary>
        public new SaveType Save
        {
            get { return GetAttributeEnum<SaveType>("save"); }
            set { SetAttribute("save", value.ToString().ToLower()); }
        }

        /// <summary>
        /// If "Save" is not set to 'false' then is RECOMMENDED to also include an "Expire" value, which indicates 
        /// how many seconds after messages are archived that the server SHOULD delete them.
        /// </summary>
        public int Expire
        {
            get { return GetAttributeInt("expire"); }
            set { SetAttribute("expire", value); }
        }

        /// <summary>
        /// If the user has never set the default Modes, the 'Save' and 'Otr' values SHOULD specify the server's default settings,
        /// and the 'unset' value SHOULD be set to 'true'.
        /// Note: The 'unset' value defaults to 'false'.
        /// </summary>
        public bool Unset
        {
            get { return GetAttributeBool("unset"); }
            set { SetAttribute("unset", value); }
        }
    }
}
