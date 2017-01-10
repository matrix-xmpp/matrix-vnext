using Matrix.Attributes;

namespace Matrix.Xmpp.Stream.Features
{
    [XmppTag(Name = "sm", Namespace = Namespaces.FeatureStreamManagement)]
    public class StreamManagement : StreamFeature
    {
        public StreamManagement()
            : base(Namespaces.FeatureStreamManagement, "sm")
        {
        }
    }
}