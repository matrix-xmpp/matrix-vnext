using System.Collections.Generic;
using Matrix.Core.Attributes;
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.Disco
{
    /// <summary>
    /// 
    /// </summary>
    [XmppTag(Name = Tag.Query, Namespace = Namespaces.DiscoItems)]
    public class Items : XmppXElementWithResultSet
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Items"/> class.
        /// </summary>
        public Items()
            : base(Namespaces.DiscoItems, Tag.Query)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Items"/> class.
        /// </summary>
        /// <param name="node">The node.</param>
        public Items(string node) : this()
        {
            Node = node;
        }
        #endregion

        /// <summary>
        /// The node to discover (Optional)
        /// </summary>
        public string Node
        {
            get { return GetAttribute("node"); }
            set { SetAttribute("node", value); }
        }

        /// <summary>
        /// Adds an item.
        /// </summary>
        /// <returns></returns>
        public Item AddItem()
        {
            var item = new Item();
            Add(item);
            
            return item;
        }

        /// <summary>
        /// Adds an item.
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

        /// <summary>
        /// Sets the items.
        /// </summary>
        /// <param name="items">The items.</param>
        public void SetItems(IEnumerable<Item> items)
        {
            RemoveAllItems();
            foreach (Item item in items)            
                AddItem(item);            
        }

        /// <summary>
        /// Removes all items.
        /// </summary>
        public void RemoveAllItems()
        {            
            RemoveAll<Item>();           
        }
    }
}