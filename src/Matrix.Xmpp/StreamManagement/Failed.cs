using Matrix.Core.Attributes;

namespace Matrix.Xmpp.StreamManagement
{
    [XmppTag(Name = "failed", Namespace = Namespaces.FeatureStreamManagement)]
    public class Failed : Base.Error
    {
        public Failed() : base(Namespaces.FeatureStreamManagement, "failed")
        {
        }
    }
}