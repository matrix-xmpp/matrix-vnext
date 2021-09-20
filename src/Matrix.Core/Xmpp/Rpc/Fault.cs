using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Rpc
{
    [XmppTag(Name = "fault", Namespace = Namespaces.IqRpc)]
    internal class Fault : XmppXElement
    {
        public Fault()
            : base(Namespaces.IqRpc, "fault")
        {
        }
    }
}
