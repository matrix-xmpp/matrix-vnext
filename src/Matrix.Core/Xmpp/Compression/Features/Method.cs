using Matrix.Attributes;

namespace Matrix.Xmpp.Compression.Features
{
    [XmppTag(Name = "method", Namespace = Namespaces.FeatureCompress)]
    public class Method : Xmpp.Compression.Method
    {
        public Method() : base(Namespaces.FeatureCompress)
        {
        }

        public Method(Methods method) : this()
        {
            Value = method.GetName();
        }
    }
}
