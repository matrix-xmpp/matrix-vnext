using Matrix.Attributes;
using Matrix.Xmpp.Stream.Features;

namespace Matrix.Xmpp.Tls
{
    /// <summary>
    /// StartTls object
    /// </summary>
    [XmppTag(Name = Tag.StartTls, Namespace = Namespaces.Tls)]
    public class StartTls : StreamFeature
    {
        public StartTls()
            : base(Namespaces.Tls, Tag.StartTls)
        {            
        }
    }
}