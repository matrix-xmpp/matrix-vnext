using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Rpc
{
    [XmppTag(Name = "member", Namespace = Namespaces.IqRpc)]
    internal class Member : XmppXElement
    {
        public Member()
            : base(Namespaces.IqRpc, "member")
        {
        }
    }
}
