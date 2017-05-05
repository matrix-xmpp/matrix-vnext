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

namespace Matrix.Xmpp.Jingle
{
    public enum CandidateType
    {
        #region Schema
        /*
         * <xs:attribute name='type' use='required'>
          <xs:simpleType>
            <xs:restriction base='xs:NCName'>
              <xs:enumeration value='host'/>
              <xs:enumeration value='prflx'/>
              <xs:enumeration value='relay'/>
              <xs:enumeration value='srflx'/>
            </xs:restriction>
          </xs:simpleType>
         */
        #endregion

        Host,
        Prflx,
        Relay,
        Srflx
    }
}
