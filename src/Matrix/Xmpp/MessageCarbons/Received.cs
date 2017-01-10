using Matrix.Attributes;

namespace Matrix.Xmpp.MessageCarbons
{
    [XmppTag(Name = "received", Namespace = Namespaces.MessageCarbons)]
    public class Received : ForwardContainer
    {
        public Received() : base("received") { }
    }
}