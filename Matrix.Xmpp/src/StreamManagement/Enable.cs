using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.StreamManagement
{
    [XmppTag(Name = "enable", Namespace = Namespaces.FeatureStreamManagement)]
    public class Enable : XmppXElement
    {
        internal Enable(string tagname)
            : base(Namespaces.FeatureStreamManagement, tagname)
        {
        }

        public Enable() : this("enable")
        {
        }

        public bool Resume
        {
            get { return GetAttributeBool("resume"); }
            set
            {
                if (value)
                    SetAttribute("resume", true);
                else
                    RemoveAttribute("resume");
            }
        }

        /// <summary>
        /// The initiating entity's preferred maximum resumption time in seconds.
        /// </summary>
        public int MaxResumptionTime
        {
            get { return GetAttributeInt("max"); }
            set { SetAttribute("max", value); }
        }

        /// <summary>
        /// The initiating entity's preferred number of stanzas between acks.
        /// </summary>
        public int StanzasBetweenAcks
        {
            get { return GetAttributeInt("stanzas"); }
            set { SetAttribute("stanzas", value); }
        }
    }
}