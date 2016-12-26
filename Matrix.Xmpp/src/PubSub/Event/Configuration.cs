using Matrix.Core.Attributes;

namespace Matrix.Xmpp.PubSub.Event
{
    [XmppTag(Name = "configuration", Namespace = Namespaces.PubsubEvent)]
    public class Configuration : Configure
    {
        public Configuration() : base(Namespaces.PubsubEvent, "configuration")
        {
        }
    }
}