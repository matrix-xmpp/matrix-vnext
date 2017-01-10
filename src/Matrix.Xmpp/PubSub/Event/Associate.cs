using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.PubSub.Event
{
    [XmppTag(Name = "associate", Namespace = Namespaces.PubsubEvent)]
    public class Associate : XmppXElement
    {
        public Associate()
            : base(Namespaces.PubsubEvent, "associate")
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