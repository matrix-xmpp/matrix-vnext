using Matrix.Core.Attributes;

namespace Matrix.Xmpp.Muc.Owner
{
    [XmppTag(Name = "destroy", Namespace = Namespaces.MucOwner)]
    public class Destroy : User.Destroy
    {
        public Destroy() : base(Namespaces.MucOwner)
        {}
    }
}