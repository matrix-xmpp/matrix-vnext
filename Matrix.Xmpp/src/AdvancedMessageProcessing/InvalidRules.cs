using Matrix.Core.Attributes;
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.AdvancedMessageProcessing
{
    [XmppTag(Name = "invalid-rules", Namespace = Namespaces.AMP)]
    public class InvalidRules : XmppXElementWithRules
    {
        public InvalidRules() : base(Namespaces.AMP, "invalid-rules")
        {
        }
    }
}