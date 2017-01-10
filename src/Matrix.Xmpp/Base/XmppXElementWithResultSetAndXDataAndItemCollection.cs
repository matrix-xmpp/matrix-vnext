using System.Collections.Generic;
using Matrix.Xml;
using Matrix.Xmpp.XData;

namespace Matrix.Xmpp.Base
{
    public abstract class XmppXElementWithResultSetAndXDataAndItemCollection<T> 
        : XmppXElementWithResultSet where T : XmppXElement, new()
    {
        protected XmppXElementWithResultSetAndXDataAndItemCollection(string ns, string tagname)
            : base(ns, tagname)
        {
        }
        #region << Item properties >>
        /// <summary>
        /// Adds an item.
        /// </summary>
        /// <returns></returns>
        public T AddItem()
        {
            var item = new T();
            Add(item);

            return item;
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void AddItem(T item)
        {
            Add(item);
        }

        ///<summary>
        /// Adds multiple items.
        ///</summary>
        ///<param name="items"></param>
        public void AddItems(T[] items)
        {
            foreach (var item in items)
                Add(item);
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetItems()
        {
            return Elements<T>();
        }

        /// <summary>
        /// Sets the Items.
        /// </summary>
        /// <param name="items">The Items.</param>
        public void SetItems(IEnumerable<T> items)
        {
            RemoveAllItems();
            foreach (T item in items)
                AddItem(item);
        }

        /// <summary>
        /// Removes all Items.
        /// </summary>
        public void RemoveAllItems()
        {
            RemoveAll<T>();
        }
        #endregion

        /// <summary>
        /// Gets or sets the Xdata object.
        /// </summary>
        /// <value>The X data.</value>
        public Data XData
        {
            get { return Element<Data>(); }
            set { Replace(value); }
        }
    }
}