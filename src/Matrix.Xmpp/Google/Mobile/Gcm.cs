using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Google.Mobile
{
    [XmppTag(Name = "gcm", Namespace = Namespaces.GoogleMobileData)]
    public class Gcm : XmppXElement
    {
        public Gcm() : base(Namespaces.GoogleMobileData, "gcm")
        {
        }
    }
}