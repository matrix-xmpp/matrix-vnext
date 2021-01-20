using Matrix.Attributes;

namespace Matrix.Xmpp.PubSub.Owner
{
    [XmppTag(Name = "subscription", Namespace = Namespaces.PubsubOwner)]
    public class Subscription : Base.Subscription
    {
        public Subscription() : base(Namespaces.PubsubOwner)
        {
        }
    }
}
