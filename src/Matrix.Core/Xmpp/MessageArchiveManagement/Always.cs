using Matrix.Attributes;

namespace Matrix.Xmpp.MessageArchiveManagement
{
    [XmppTag(Name = "always", Namespace = Namespaces.MessageArchiveManagement)]
    public class Always : PolicyBase
    {
        public Always() : base("always")
        {
        }
    }
}