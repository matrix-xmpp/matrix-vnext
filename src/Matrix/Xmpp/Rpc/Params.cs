using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Rpc
{
    [XmppTag(Name = "params", Namespace = Namespaces.IqRpc)]
    internal class Params : XmppXElement
    {
        public Params() : base(Namespaces.IqRpc, "params")
        {
        }
    }
}