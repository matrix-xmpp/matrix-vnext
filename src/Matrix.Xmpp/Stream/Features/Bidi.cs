using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Stream.Features
{
    [XmppTag(Name = "bidi", Namespace = Namespaces.FeatureBidi)]
    public class Bidi : XmppXElement
    {
        public Bidi() : base(Namespaces.FeatureBidi, "bidi")
        {
        }
    }
}