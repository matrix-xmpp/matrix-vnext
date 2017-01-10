using Matrix.Xml;

namespace Matrix.Xmpp.Base
{
    public abstract class XmppXElementWithNameAttribute : XmppXElement
    {
        internal XmppXElementWithNameAttribute(string ns, string tagname)
            : base(ns, tagname)
        {
        }

        public new string Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }
    }
}