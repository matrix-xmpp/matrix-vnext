using Matrix.Attributes;

namespace Matrix.Xmpp.PubSub
{
    [XmppTag(Name = "affiliation", Namespace = Namespaces.Pubsub)]
    public class Affiliation : Base.Affiliation
    {
        public Affiliation() : base(Namespaces.Pubsub)
        {
        }

        #region << Properties >>
        /// <summary>
        /// Gets or sets the node.
        /// </summary>
        /// <value>The node.</value>
        public string Node
        {
            get { return GetAttribute("node"); }
            set { SetAttribute("node", value); }
        }
        #endregion
    }
}