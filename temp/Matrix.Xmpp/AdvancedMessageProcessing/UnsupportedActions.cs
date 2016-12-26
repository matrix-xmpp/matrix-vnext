using Matrix.Attributes;
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.AdvancedMessageProcessing
{
    [XmppTag(Name = "unsupported-actions", Namespace = Namespaces.AMP)]
    public class UnsupportedActions : XmppXElementWithRules
    {
        public UnsupportedActions() : base(Namespaces.AMP, "unsupported-actions")
        {
        }
    }
}