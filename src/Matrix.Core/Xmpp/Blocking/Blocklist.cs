using Matrix.Attributes;

namespace Matrix.Xmpp.Blocking
{
    [XmppTag(Name = "blocklist", Namespace = Namespaces.Blocking)]
    public class Blocklist : BlockBase
    {
        public Blocklist() : base("blocklist")
        {
        }
    }
}
