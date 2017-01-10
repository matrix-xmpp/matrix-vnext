using Matrix.Attributes;
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.Receipts
{
    [XmppTag(Name = "received", Namespace = Namespaces.MessageReceipts)]
    public class Received : XmppXElementWithIdAttribute
    {
        public Received()
            : base(Namespaces.MessageReceipts, "received")
        {
        }
    }
}