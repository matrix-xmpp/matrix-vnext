using Matrix.Core.Attributes;

namespace Matrix.Xmpp.StreamManagement
{
    [XmppTag(Name = "resumed", Namespace = Namespaces.FeatureStreamManagement)]
    public class Resumed : Resume
    {
        public Resumed() : base("resumed")
        {
        }
    }
}