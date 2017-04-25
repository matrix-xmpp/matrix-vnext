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
        <xs:attribute name='otr' use='optional'>
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
     */
    #endregion

    public enum OtrType
    {
        /// <summary>
        /// approve -- the user MUST explicitly approve off-the-record communication.
        /// </summary>
        Approve,        
        /// <summary>
        /// concede -- communications MAY be off the record if requested by another user.
        /// </summary>
        Concede,
        
        /// <summary>
        /// communications MUST NOT be off the record.
        /// </summary>
        Forbid,
        
        /// <summary>
        /// communications SHOULD NOT be off the record even if requested.
        /// </summary>
        Oppose,
        
        /// <summary>
        /// communications SHOULD be off the record if possible.
        /// </summary>
        Prefer,
                
        /// <summary>
        ///  communications MUST be off the record.
        /// </summary>
        Require
    }
}
