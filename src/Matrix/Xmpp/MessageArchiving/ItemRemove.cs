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