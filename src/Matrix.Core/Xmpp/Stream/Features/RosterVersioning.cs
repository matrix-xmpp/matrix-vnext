using Matrix.Attributes;

namespace Matrix.Xmpp.Stream.Features
{
    [XmppTag(Name = "ver", Namespace = Namespaces.FeatureRosterVersioning)]
    public class RosterVersioning : StreamFeature
    {
        public RosterVersioning() : base(Namespaces.FeatureRosterVersioning, "ver")
        {
        }
    }
}
