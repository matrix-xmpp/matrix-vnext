using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.PubSub
{
    [XmppTag(Name = Tag.Item, Namespace = Namespaces.Pubsub)]
    public class Item : XmppXElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item() : base(Namespaces.Pubsub, "item")
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class and adds the given payload.
        /// </summary>
        /// <param name="payload">The payload.</param>
        public Item(XmppXElement payload) : this()
        {
            Add(payload);
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public string Id
        {
            get { return GetAttribute("id"); }
            set { SetAttribute("id", value); }
        }
    }
}
