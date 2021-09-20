using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Rpc
{
    [XmppTag(Name = "param", Namespace = Namespaces.IqRpc)]
    internal class Param : XmppXElement
    {
        public Param()
            : base(Namespaces.IqRpc, "param")
        {
        }
    }
}
