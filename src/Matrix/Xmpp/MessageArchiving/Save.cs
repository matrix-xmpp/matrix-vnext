using Matrix.Attributes;

namespace Matrix.Xmpp.MessageArchiving
{
    [XmppTag(Name = "save", Namespace = Namespaces.Archiving)]
    public class Save : ArchiveBase
    {
        public Save() : base("save")
        {
        }
    }
}