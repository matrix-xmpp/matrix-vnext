using Matrix.Core.Attributes;

namespace Matrix.Xmpp.SecurityLabels
{
    [XmppTag(Name = "equivalentlabel", Namespace = Namespaces.SecurityLabel)]
    public class EquivalentLabel : Label
    {
        public EquivalentLabel()
            : base("equivalentlabel")
        {
        }
    }
}