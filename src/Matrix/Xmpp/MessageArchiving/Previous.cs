using Matrix.Attributes;

namespace Matrix.Xmpp.MessageArchiving
{
    [XmppTag(Name = "previous", Namespace = Namespaces.Archiving)]
    public class Previous : Link
    {
        public Previous() : base("previous")
        {
        }
    }
}