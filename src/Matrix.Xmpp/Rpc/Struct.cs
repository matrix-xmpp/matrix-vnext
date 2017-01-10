using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Rpc
{
    [XmppTag(Name = "struct", Namespace = Namespaces.IqRpc)]
    internal class Struct : XmppXElement
    {
        public Struct()
            : base(Namespaces.IqRpc, "struct")
        {
        }
    }
}