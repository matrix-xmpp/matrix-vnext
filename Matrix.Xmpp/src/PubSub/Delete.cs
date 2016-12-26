using Matrix.Xml;

namespace Matrix.Xmpp.PubSub
{
    public abstract class Delete : XmppXElement
    {
        protected Delete(string ns) : base(ns, "delete")
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