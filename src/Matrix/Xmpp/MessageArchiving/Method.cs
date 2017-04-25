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
    [XmppTag(Name = "method", Namespace = Namespaces.Archiving)]
    public class Method : XmppXElement
    {
        #region Schema
        /*
          <xs:element name='method'>
            <xs:complexType>
              <xs:simpleContent>
                <xs:extension base='empty'>
                  <xs:attribute name='type' type='xs:string' use='required'/>
                  <xs:attribute name='use' use='required'>
                    <xs:simpleType>
                      <xs:restriction base='xs:NCName'>
                        <xs:enumeration value='concede'/>
                        <xs:enumeration value='forbid'/>
                        <xs:enumeration value='prefer'/>
                      </xs:restriction>
                    </xs:simpleType>
                  </xs:attribute>
                </xs:extension>
              </xs:simpleContent>
            </xs:complexType>
          </xs:element>
         */
        #endregion

        #region Xml samples
        /*
            <method type='auto' use='concede'/>
            <method type='local' use='concede'/>
            <method type='manual' use='concede'/>
         */
        #endregion
        public Method() : base(Namespaces.Archiving, "method")
        {
        }

        public MethodType Type
        {
            get { return GetAttributeEnum<MethodType>("type"); }
            set { SetAttribute("type", value.ToString().ToLower()); }
        }
        
        public UseType Use
        {
            get { return GetAttributeEnum<UseType>("use"); }
            set { SetAttribute("use", value.ToString().ToLower()); }
        }
    }
}
