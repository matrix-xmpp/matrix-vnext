using Matrix.Attributes;
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.PubSub
{
    [XmppTag(Name = "configure", Namespace = Namespaces.Pubsub)]
    public class Configure : XmppXElementWithXData
    {
        #region << Constructors >>
        internal Configure(string ns, string tag)
            : base(ns, tag)
        {
        }

        internal Configure(string ns) : base(ns, "configure")
        {
        }

        public Configure() : base(Namespaces.Pubsub, "configure")
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
    }
}
