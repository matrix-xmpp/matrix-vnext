using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Rpc
{
    [XmppTag(Name = "array", Namespace = Namespaces.IqRpc)]
    internal class Array : XmppXElement
    {
        public Array()
            : base(Namespaces.IqRpc, "array")
        {
        }
    }
}
