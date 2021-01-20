using Matrix.Attributes;
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.FeatureNegotiation
{
    [XmppTag(Name = "feature", Namespace = Namespaces.FeatureNeg)]
    public class Feature : XmppXElementWithXData
    {
        public Feature() : base(Namespaces.FeatureNeg, "feature")
        {
        }
    }
}
