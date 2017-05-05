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

namespace Matrix.Xmpp.MessageArchiving
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

    public enum MethodType
    {        
        /// <summary>
        ///  preferences for use of automatic archiving on the user's server.
        /// </summary>
        Auto,
              
        /// <summary>
        ///  Preferences for use of local archiving to a file or database on the user's machine or device.
        /// </summary>
        Local,

        /// <summary>
        /// Preferences for use of manual archiving by the user's client to the user's server.
        /// </summary>
        Manual
    }
}
