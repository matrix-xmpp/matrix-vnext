using Matrix.Attributes;

namespace Matrix.Xmpp.MessageArchiving
{
    [XmppTag(Name = "chat", Namespace = Namespaces.Archiving)]
    public class Chat : ArchiveBase
    {
        public Chat() : base("chat")
        {
        }
    }
}
