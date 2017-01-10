using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Compression
{
    [XmppTag(Name = "compressed", Namespace = Namespaces.Compress)]
    public class Compresed : XmppXElement
    {
        public Compresed()
            : base(Namespaces.Compress, "compressed")
        {
        }
    }
}