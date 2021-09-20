using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Rpc
{
    [XmppTag(Name = "value", Namespace = Namespaces.IqRpc)]
    internal class Value : XmppXElement
    {
        public Value() : base(Namespaces.IqRpc, "value")
        {
        }
    }
}
