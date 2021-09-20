using System.Collections.Generic;
using Matrix.Xml;

namespace Matrix.Xmpp.Blocking
{
    public abstract class BlockBase : XmppXElement
    {
        protected BlockBase(string tagname)
            : base(Namespaces.Blocking, tagname)
        {
        }

        /// <summary>
        /// Adds an Item.
        /// </summary>
        /// <returns></returns>
        public Item AddItem()
        {
            var item = new Item();
            Add(item);

            return item;
        }

        /// <summary>
        /// Adds an Item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void AddItem(Item item)
        {
            Add(item);
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Item> GetItems()
        {
            return Elements<Item>();
        }
    }
}
