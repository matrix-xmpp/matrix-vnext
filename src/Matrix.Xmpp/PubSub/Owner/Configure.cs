using Matrix.Core.Attributes;

namespace Matrix.Xmpp.PubSub.Owner
{
    [XmppTag(Name = "configure", Namespace = Namespaces.PubsubOwner)]
    public class Configure : Xmpp.PubSub.Configure
    {
        public Configure() : base(Namespaces.PubsubOwner)
        {
        }
    }
}