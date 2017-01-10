using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Ping
{
    /// <summary>
    /// XEP-0199: XMPP Ping
    /// </summary>
    [XmppTag(Name = "ping", Namespace = Namespaces.Ping)]
    public class Ping : XmppXElement
    {
        public Ping() : base(Namespaces.Ping, "ping")
        {
        }
    }
}