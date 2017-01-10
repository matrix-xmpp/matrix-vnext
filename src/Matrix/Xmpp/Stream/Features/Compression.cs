using Matrix.Attributes;

namespace Matrix.Xmpp.Stream.Features
{
    [XmppTag(Name = "compression", Namespace = Namespaces.FeatureCompress)]
    public class Compression : Xmpp.Compression.Compression
    {
        public Compression() : base(Namespaces.FeatureCompress)
        {
        }
    }
}