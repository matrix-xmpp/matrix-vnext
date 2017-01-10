using Matrix.Core.Attributes;

namespace Matrix.Xmpp.MessageCarbons
{
     [XmppTag(Name = "disable", Namespace = Namespaces.MessageCarbons)]
    public class Disable : CarbonBase
    {
        public Disable() : base("disable") { }
    }
}