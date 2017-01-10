using Matrix.Attributes;

namespace Matrix.Xmpp.MessageCarbons
{
    [XmppTag(Name = "private", Namespace = Namespaces.MessageCarbons)]
    public class Private : CarbonBase
    {
        public Private() : base("private") { }
    }
}