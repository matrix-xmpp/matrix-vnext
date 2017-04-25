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

namespace Matrix.Xmpp.Jingle
{
    public enum Action
    {
        #region Schema
        /*
        <xs:restriction base='xs:NCName'>
            <xs:enumeration value='content-accept'/>
            <xs:enumeration value='content-add'/>
            <xs:enumeration value='content-modify'/>
            <xs:enumeration value='content-reject'/>
            <xs:enumeration value='content-remove'/>
            <xs:enumeration value='description-info'/>
            <xs:enumeration value='security-info'/>
            <xs:enumeration value='session-accept'/>
            <xs:enumeration value='session-info'/>
            <xs:enumeration value='session-initiate'/>
            <xs:enumeration value='session-terminate'/>
            <xs:enumeration value='transport-accept'/>
            <xs:enumeration value='transport-info'/>
            <xs:enumeration value='transport-reject'/>
            <xs:enumeration value='transport-replace'/>
          </xs:restriction>
        </xs:simpleType>
        */
        #endregion

        [Name("content-accept")]
        ContentAccept,

        [Name("content-add")]
        ContentAdd,

        [Name("content-modify")]
        ContentModify,

        [Name("content-reject")]
        ContentReject,

        [Name("content-remove")]
        ContentRemove,

        [Name("description-info")]
        DescriptionInfo,

        [Name("description-info")]
        SecurityInfo,

        [Name("session-accept")]
        SessionAccept,

        [Name("session-info")]
        SessionInfo,

        [Name("session-initiate")]
        SessionInitiate,

        [Name("session-terminate")]
        SessionTerminate,

        [Name("transport-accept")]
        TransportAccept,

        [Name("transport-info")]
        TransportInfo,

        [Name("transport-reject")]
        TransportReject,

        [Name("transport-replace")]
        TransportReplace
    }
}
