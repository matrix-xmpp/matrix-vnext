using Matrix.Attributes;
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.AdvancedMessageProcessing
{
    [XmppTag(Name = "unsupported-conditions", Namespace = Namespaces.AMP)]
    public class UnsupportedConditions : XmppXElementWithRules
    {
        public UnsupportedConditions() : base(Namespaces.AMP, "unsupported-conditions")
        {
        }
    }
}