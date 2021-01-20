using Matrix.Attributes;

namespace Matrix.Xmpp.MessageArchiveManagement
{
    [XmppTag(Name = "never", Namespace = Namespaces.MessageArchiveManagement)]
    public class Never : PolicyBase
    {
        public Never() : base("never")
        {
        }
    }
}