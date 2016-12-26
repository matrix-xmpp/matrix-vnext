using Matrix.Core.Attributes;
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.PubSub
{
    [XmppTag(Name = "items", Namespace = Namespaces.Pubsub)]
    public class Items : XmppXElementWithItemCollection<Item>
    {
        #region << Constrictors >>
        public Items() : base(Namespaces.Pubsub, "items")
        {
        }
        #endregion

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
        /// Gets or sets the subscription id.
        /// </summary>
        /// <value>The subscription id.</value>
        public string Id
        {
            get { return GetAttribute("subid"); }
            set { SetAttribute("subid", value); }
        }
        
        /// <summary>
        /// A service MAY allow entities to request the most recent N items. 
        /// When max_items is used, implementations SHOULD return the N most recent 
        /// (as opposed to the N oldest) items.
        /// </summary>
        /// <value>The max items.</value>
        public int MaxItems
        {
            get { return GetAttributeInt("max_items"); }
            set { SetAttribute("max_items", value); }
        }
    }
}