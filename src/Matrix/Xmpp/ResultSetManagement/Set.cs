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

namespace Matrix.Xmpp.ResultSetManagement
{
    /// <summary>
    /// XEP-0059: Result Set Management
    /// </summary>
    [XmppTag(Name = "set", Namespace = Namespaces.Rsm)]
    public class Set : XmppXElement
    {
        #region Schema
        /*
         <xs:element name='set'>
            <xs:complexType>
              <xs:sequence>
                <xs:element name='after' type='xs:string' minOccurs='0' maxOccurs='1'/>
                <xs:element name='before' type='xs:string' minOccurs='0' maxOccurs='1'/>
                <xs:element name='count' type='xs:int' minOccurs='0' maxOccurs='1'/>
                <xs:element ref='first' minOccurs='0' maxOccurs='1'/>
                <xs:element name='index' type='xs:int' minOccurs='0' maxOccurs='1'/>
                <xs:element name='last' type='xs:string' minOccurs='0' maxOccurs='1'/>
                <xs:element name='max' type='xs:int' minOccurs='0' maxOccurs='1'/>
              </xs:sequence>
            </xs:complexType>
         </xs:element>
        */
        #endregion

        public Set() : base(Namespaces.Rsm, "set")
        {}

        /// <summary>
        /// Gets or sets the maximum for limiting the number of items.
        /// </summary>
        /// <value>
        /// The maximum.
        /// </value>
        public int Maximum
        {
            get { return GetTagInt("max"); }
            set { SetTag("max", value); }
        }

        public int Index
        {
            get { return GetTagInt("index"); }
            set { SetTag("index", value); }
        }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count
        {
            get { return GetTagInt("count"); }
            set { SetTag("count", value); }
        }

        public string After
        {
            get { return GetTag("after"); }
            set { SetTag("after", value); }
        }

        public string Before
        {
            get { return GetTag("before"); }
            set { SetTag("before", value); }
        }

        /// <summary>
        /// Gets or sets a reference the last item.
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
        /// Gets or sets a reference the first item.
        /// </summary>
        /// <value>
        /// The first.
        /// </value>
        public First First
        {
            get { return Element<First>(); }
            set { Replace(value); }
        }
    }
}
