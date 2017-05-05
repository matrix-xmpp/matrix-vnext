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
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.Search
{
    [XmppTag(Name = Tag.Item, Namespace = Namespaces.IqSearch)]
    public class SearchItem : XmppXElementWithJidAttribute
    {
        #region Schema
        /*
          <xs:element name='item'>
            <xs:complexType>
              <xs:all>
                <xs:element name='first' type='xs:string'/>
                <xs:element name='last' type='xs:string'/>
                <xs:element name='nick' type='xs:string'/>
                <xs:element name='email' type='xs:string'/>
              </xs:all>
              <xs:attribute name='jid' type='xs:string' use='required'/>
            </xs:complexType>
          </xs:element>
        */
        #endregion

        public SearchItem() : base(Namespaces.IqSearch, "item")
        {}

        /// <summary>
        /// Gets or sets the firstname.
        /// </summary>
        /// <value>
        /// The firstname.
        /// </value>
        public string First
        {
            get { return GetTag("first"); }
            set { SetTag("first", value); }
        }
        
        /// <summary>
        /// Gets or sets the lastname.
        /// </summary>
        /// <value>
        /// The lastname.
        /// </value>
        public string Last
        {
            get { return GetTag("last"); }
            set { SetTag("last", value); }
        }
        
        /// <summary>
        /// Gets or sets the nickname.
        /// </summary>
        /// <value>
        /// The nickname.
        /// </value>
        public string Nick
        {
            get { return GetTag("nick"); }
            set { SetTag("nick", value); }
        }
        
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email
        {
            get { return GetTag("email"); }
            set { SetTag("email", value); }
        }
    }
}
