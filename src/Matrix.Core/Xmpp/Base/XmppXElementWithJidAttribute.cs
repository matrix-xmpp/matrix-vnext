using Matrix.Xml;

namespace Matrix.Xmpp.Base
{
    public abstract class XmppXElementWithJidAttribute : XmppXElement
    {
        internal XmppXElementWithJidAttribute(string ns, string tagname)
            : base(ns, tagname)
        {
        }

        /// <summary>
        /// Gets or sets the jid.
        /// </summary>
        /// <value>The jid.</value>
        public Jid Jid
        {
            get { return GetAttributeJid("jid");}
            set { SetAttribute("jid", value); }
        }
    }
}
