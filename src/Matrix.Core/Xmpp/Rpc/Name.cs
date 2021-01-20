using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Rpc
{
    [XmppTag(Name = "name", Namespace = Namespaces.IqRpc)]
    internal class Name : XmppXElement
    {
        public Name()
            : base(Namespaces.IqRpc, "name")
        {
        }
    }
}
