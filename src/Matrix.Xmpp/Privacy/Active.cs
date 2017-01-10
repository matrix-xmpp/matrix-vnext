using Matrix.Core.Attributes;

namespace Matrix.Xmpp.Privacy
{
    [XmppTag(Name = "active", Namespace = Namespaces.IqPrivacy)]
    public class Active : Base.XmppXElementWithNameAttribute
    {
        public Active() : base(Namespaces.IqPrivacy, "active")
        {
        }
    }
}