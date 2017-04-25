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

using System.Collections.Generic;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.MessageArchiving
{
    [XmppTag(Name = "itemremove", Namespace = Namespaces.Archiving)]
    public class ItemRemove : XmppXElement
    {
        #region Schema
        /*
          <xs:element name='itemremove'>
            <xs:complexType>
              <xs:sequence>
                <xs:element ref='item' minOccurs='1' maxOccurs='unbounded'/>
              </xs:sequence>
            </xs:complexType>
          </xs:element>
        */
        #endregion
        public ItemRemove() : base(Namespaces.Archiving, "itemremove")
        {
        }

        #region methods
        public IEnumerable<Item> GetItems()
        {
            return Elements<Item>();
        }

        public Item AddItem()
        {
            var item = new Item();
            Add(item);

            return item;
        }

        public ItemRemove AddItem(Item item)
        {
            Add(item);
            return this;
        }
        #endregion
    }
}
