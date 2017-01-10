using System.Collections.Generic;
using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.PubSub
{
    [XmppTag(Name = "publish", Namespace = Namespaces.Pubsub)]
    public class Publish : XmppXElement
    {
        internal Publish(string tag) : base(Namespaces.Pubsub, tag)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Publish"/> class.
        /// </summary>
        public Publish() : base(Namespaces.Pubsub, "publish")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Publish"/> class and adds the given item.
        /// </summary>
        /// <param name="item">The item.</param>
        public Publish(Item item): this()
        {
            AddItem(item);
        }

        /// <summary>
        /// Gets or sets the node.
        /// </summary>
        /// <value>The node.</value>
        public string Node
        {
            get { return GetAttribute("node"); }
            set { SetAttribute("node", value); }
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