using Matrix.Xml;

namespace Matrix.Xmpp.PubSub
{
    public abstract class Purge : XmppXElement
    {
        protected Purge() : this(Namespaces.Pubsub)
        {
        }
        
        protected Purge(string ns)
            : base(ns, "purge")
        {
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
    }
}