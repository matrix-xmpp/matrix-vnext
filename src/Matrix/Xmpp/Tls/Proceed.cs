using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Tls
{
    /// <summary>
    /// StartTls object
    /// </summary>
    [XmppTag(Name = Tag.Proceed, Namespace = Namespaces.Tls)]
    public class Proceed : XmppXElement
    {
        /*
         * <proceed xmlns='urn:ietf:params:xml:ns:xmpp-tls'/>
         */
        public Proceed()
            : base(Namespaces.Tls, Tag.Proceed)
        {
        }
    }
}