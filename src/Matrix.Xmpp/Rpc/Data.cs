using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Rpc
{
    [XmppTag(Name = "data", Namespace = Namespaces.IqRpc)]
    internal class Data : XmppXElement
    {
        public Data()
            : base(Namespaces.IqRpc, "data")
        {
        }
    }
}