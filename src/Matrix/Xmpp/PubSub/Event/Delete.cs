using Matrix.Attributes;

namespace Matrix.Xmpp.PubSub.Event
{
    [XmppTag(Name = "delete", Namespace = Namespaces.PubsubEvent)]
    public class Delete : Xmpp.PubSub.Delete
    {
        public Delete() : base(Namespaces.PubsubEvent)
        {
        }
    }
}