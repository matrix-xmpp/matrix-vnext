using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Stream.Features
{
    [XmppTag(Name = "auth", Namespace = Namespaces.FeatureAuth)]
    public class Auth : XmppXElement
    {
        public Auth()
            : base(Namespaces.FeatureAuth, "auth")
        {
        }
    }
}
