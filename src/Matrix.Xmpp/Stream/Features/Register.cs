using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Stream.Features
{
    [XmppTag(Name = "register", Namespace = Namespaces.FeatureIqRegister)]
    public class Register : XmppXElement
    {
        public Register() : base(Namespaces.FeatureIqRegister, "register")
        {
        }
    }
}