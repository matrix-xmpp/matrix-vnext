using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.ResultSetManagement
{
    [XmppTag(Name = "first", Namespace = Namespaces.Rsm)]
    public class First : XmppXElement
    {
        public First() : base(Namespaces.Rsm, "first")
        {}

        public int Index
        {
            get { return GetAttributeInt("index"); }
            set { SetAttribute("index", value); }
        }
    }
}
