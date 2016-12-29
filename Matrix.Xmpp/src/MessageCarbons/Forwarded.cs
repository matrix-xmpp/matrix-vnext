using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.MessageCarbons
{
    [XmppTag(Name = "forwarded", Namespace = Namespaces.Forward)]
    public class Forwarded : XmppXElement
    {
        public Forwarded() : base(Namespaces.Forward, "forwarded") { }
    }
}