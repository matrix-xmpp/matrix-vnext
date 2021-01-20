using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.StreamManagement.Ack
{
    [XmppTag(Name = "r", Namespace = Namespaces.FeatureStreamManagement)]
    public class Request : XmppXElement
    {
        public Request() : base(Namespaces.FeatureStreamManagement, "r")
        {
        }
    }
}
