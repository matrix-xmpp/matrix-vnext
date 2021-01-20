using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.SecurityLabels
{
    [XmppTag(Name = "esssecuritylabel", Namespace = Namespaces.SecurityLabelEss)]
    public class EssSecurityLabel : XmppXElement
    {
        public EssSecurityLabel()
            : base(Namespaces.SecurityLabelEss, "esssecuritylabel")
        {
        }
    }
}
