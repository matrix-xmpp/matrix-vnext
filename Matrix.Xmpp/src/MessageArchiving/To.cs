using Matrix.Core.Attributes;

namespace Matrix.Xmpp.MessageArchiving
{
    [XmppTag(Name = "to", Namespace = Namespaces.Archiving)]
    public class To : MessageItem
    {
        public To() : base("to")
        {
        }
    }
}