using Matrix.Xml;

namespace Matrix.Xmpp.Base
{
    public abstract class XmppXElementWithIdAttribute : XmppXElement
    {
        internal XmppXElementWithIdAttribute(string ns, string tagname)
            : base(ns, tagname)
        {
        }

        public string Id
        {
            get { return GetAttribute("id"); }
            set { SetAttribute("id", value); }
        }
    }
}