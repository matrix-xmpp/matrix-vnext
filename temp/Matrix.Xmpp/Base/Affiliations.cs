using Matrix.Xml;

namespace Matrix.Xmpp.Base
{
    public class Affiliations : XmppXElement
    {
        #region << Constructors >>
        internal Affiliations(string ns)
            : base(ns, "affiliations")
        {
        }
        public Affiliations() : base(Namespaces.Pubsub, "affiliations")
        {
        }
        #endregion

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