using Matrix.Attributes;

namespace Matrix.Xmpp.PubSub.Event
{
    [XmppTag(Name = "purge", Namespace = Namespaces.PubsubEvent)]
    public class Purge : Xmpp.PubSub.Purge
    {
        public Purge() : base(Namespaces.PubsubEvent)
        {
        }
    }
}
