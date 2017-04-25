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
    [XmppTag(Name = Tag.Query, Namespace = Namespaces.IqSearch)]
    public class Search : XmppXElementWithResultSetAndXDataAndItemCollection<SearchItem>
    {
        #region Schema
        /*
        <xs:element name='query'>
            <xs:complexType>
              <xs:choice>
                <xs:all xmlns:xdata='jabber:x:data'>
                  <xs:element name='instructions' type='xs:string'/>
                  <xs:element name='first' type='xs:string'/>
                  <xs:element name='last' type='xs:string'/>
                  <xs:element name='nick' type='xs:string'/>
                  <xs:element name='email' type='xs:string'/>
                  <xs:element ref='xdata:x' minOccurs='0'/>
                </xs:all>
                <xs:element ref='item' minOccurs='0' maxOccurs='unbounded'/>
              </xs:choice>
            </xs:complexType>
          </xs:element>
        */
        #endregion

        public Search()
            : base(Namespaces.IqSearch, Tag.Query)
        {
        }
        
        /// <summary>
        /// Gets or sets the instructions.
        /// </summary>
        /// <value>
        /// The instructions.
        /// </value>
        public string Instructions
        {
            get { return GetTag("instructions"); }
            set { SetTag("instructions", value); }
        }

        /// <summary>
        /// Gets or sets the firstname.
        /// </summary>
        /// <value>
        /// The first.
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
        /// The last.
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
        /// The nick.
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
