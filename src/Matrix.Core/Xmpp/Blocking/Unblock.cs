using Matrix.Attributes;

namespace Matrix.Xmpp.Blocking
{
    [XmppTag(Name = "unblock", Namespace = Namespaces.Blocking)]
    public class Unblock : BlockBase
    {
        public Unblock()
            : base("unblock")
        {
        }
    }
}
