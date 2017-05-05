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
    #region
    /*     
      <xs:attribute name='save' use='optional'>
        <xs:simpleType>
            <xs:restriction base='xs:NCName'>
            <xs:enumeration value='body'/>
            <xs:enumeration value='false'/>
            <xs:enumeration value='message'/>
            <xs:enumeration value='stream'/>
            </xs:restriction>
        </xs:simpleType>
      </xs:attribute>      
     */
    #endregion

    public enum SaveType
    {
        /// <summary>
        /// the saving entity SHOULD save only <body/> elements. *
        /// </summary>
        Body,

        /// <summary>
        /// the saving entity MUST save nothing.
        /// </summary>
        False,

        /// <summary>
        /// the saving entity SHOULD save the full XML content of each <message/> element.
        /// </summary>
        Message,

        /// <summary>
        ///  the saving entity SHOULD save every byte that passes over the stream in either direction.
        /// </summary>
        Stream
    }
}
