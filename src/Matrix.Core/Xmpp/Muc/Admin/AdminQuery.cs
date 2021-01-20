using System.Collections.Generic;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Muc.Admin
{
    /// <summary>
    /// Admin
    /// </summary>
    [XmppTag(Name = Tag.Query, Namespace = Namespaces.MucAdmin)]
    public class AdminQuery : XmppXElement
    {
        /*
        <query xmlns='http://jabber.org/protocol/muc#admin'>
            <item nick='pistol' role='none'>
                <reason>Avaunt, you cullion!</reason>
            </item>
        </query>
        */

        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Admin"/> class.
        /// </summary>
        public AdminQuery()
            : base(Namespaces.MucAdmin, Tag.Query)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Admin"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public AdminQuery(Item item) : this()
        {
            AddItem(item);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Admin"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        public AdminQuery(Item[] items)
            : this()
        {
            AddItems(items);
        }

        #endregion

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void AddItem(Item item)
        {
            Add(item);
        }


        /// <summary>
        /// Adds the items.
        /// </summary>
        /// <param name="items">The items.</param>
        public void AddItems(Item[] items)
        {
            foreach (Item itm in items)
                Add(itm);
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
