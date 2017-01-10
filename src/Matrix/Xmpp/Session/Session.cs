using Matrix.Attributes;
using Matrix.Xmpp.Stream.Features;

namespace Matrix.Xmpp.Session
{
    [XmppTag(Name = Tag.Session, Namespace = Namespaces.Session)]
    public class Session :  StreamFeature
    {
        // <iq id="jcl_27" type="set"><session xmlns="urn:ietf:params:xml:ns:xmpp-session"/></iq>
        public Session() : base(Namespaces.Session, Tag.Session)
        {
        }
    }
}