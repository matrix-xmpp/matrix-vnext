using Matrix.Core.Attributes;

namespace Matrix.Xmpp.MessageCarbons
{
    [XmppTag(Name = "enable", Namespace = Namespaces.MessageCarbons)]
    public class Enable : CarbonBase
    {
        public Enable() : base("enable") { }
    }
}