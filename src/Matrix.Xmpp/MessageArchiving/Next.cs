using Matrix.Core.Attributes;

namespace Matrix.Xmpp.MessageArchiving
{
    [XmppTag(Name = "next", Namespace = Namespaces.Archiving)]
    public class Next : Link
    {
        public Next() : base("next")
        {
        }
    }
}