using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.PubSub.Event
{
    [XmppTag(Name = "disassociate", Namespace = Namespaces.PubsubEvent)]
    public class Disassociate : XmppXElement
    {
        public Disassociate()
            : base(Namespaces.PubsubEvent, "disassociate")
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