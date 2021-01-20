using Matrix.Attributes;

namespace Matrix.Xmpp.MessageArchiving
{
    [XmppTag(Name = "from", Namespace = Namespaces.Archiving)]
    public class From : MessageItem
    {
        public From() : base("from")
        {
        }
    }
}
