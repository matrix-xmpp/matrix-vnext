using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Attention
{
    /// <summary>
    /// XEP-0224 Attention
    /// </summary>
    [XmppTag(Name = "attention", Namespace = Namespaces.Attention)]
    public class Attention : XmppXElement
    {
        public Attention()
            : base(Namespaces.Attention, "attention")
        {
        }
    }
}