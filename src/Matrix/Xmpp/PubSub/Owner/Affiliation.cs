using Matrix.Attributes;

namespace Matrix.Xmpp.PubSub.Owner
{
    [XmppTag(Name = "affiliation", Namespace = Namespaces.PubsubOwner)]
    public class Affiliation : Base.Affiliation
    {
        public Affiliation()
            : base(Namespaces.PubsubOwner)
        {
        }

        #region << Properties >>
        /// <summary>
        /// Gets or sets the node.
        /// </summary>
        /// <value>The node.</value>
        public Jid Jid
        {
            get { return GetAttribute("jid"); }
            set { SetAttribute("jid", value); }
        }
        #endregion
    }
}