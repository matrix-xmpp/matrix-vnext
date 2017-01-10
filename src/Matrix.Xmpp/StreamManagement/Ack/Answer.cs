using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.StreamManagement.Ack
{
    [XmppTag(Name = "a", Namespace = Namespaces.FeatureStreamManagement)]
    public class Answer : XmppXElement
    {
        public Answer() : base(Namespaces.FeatureStreamManagement, "a")
        {
        }

        /// <summary>
        /// Identifies the last handled stanza (i.e., the last stanza that the receiver will acknowledge as having received).
        /// </summary>
        public int LastHandledStanza
        {
            get { return GetAttributeInt("h"); }
            set { SetAttribute("h", value); }
        }
    }
}