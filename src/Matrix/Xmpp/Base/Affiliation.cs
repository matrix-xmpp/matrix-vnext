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

using Matrix.Xml;
using Matrix.Xmpp.PubSub;

namespace Matrix.Xmpp.Base
{
    public abstract class Affiliation : XmppXElement
    {
        #region << Constructors >>
        protected Affiliation(string ns) : base(ns, "affiliation")
        {
        }
        #endregion

        #region << Properties >>
        
        /// <summary>
        /// the message type (chat, groupchat, normal, headline or error).
        /// </summary>
        public AffiliationType AffiliationType
        {
            /*
                <xs:restriction base='xs:NCName'>
                    <xs:enumeration value='member'/>
                    <xs:enumeration value='none'/>
                    <xs:enumeration value='outcast'/>
                    <xs:enumeration value='owner'/>
                    <xs:enumeration value='publisher'/>
                    <xs:enumeration value='publish-only'/>
                </xs:restriction>
             
                we cannot use the Enum functions here because of the
                dash in 'publish-only which is not a valid enum
            */
            get { return GetAttributeEnumUsingNameAttrib<AffiliationType>("affiliation"); }
            set { SetAttribute("affiliation", value.GetName());
            }
        }
        #endregion
    }
}
