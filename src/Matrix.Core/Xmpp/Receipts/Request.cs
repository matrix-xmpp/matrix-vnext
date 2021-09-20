using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Receipts
{
    [XmppTag(Name = "request", Namespace = Namespaces.MessageReceipts)]
    public class Request : XmppXElement
    {
        public Request()
            : base(Namespaces.MessageReceipts, "request")
        {
        }
    }
}
