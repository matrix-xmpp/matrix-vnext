using Matrix.Attributes;

namespace Matrix.Xmpp.StreamManagement
{
    [XmppTag(Name = "enabled", Namespace = Namespaces.FeatureStreamManagement)]
    public class Enabled : Enable
    {
        public Enabled() : base("enabled")
        {
        }

        /// <summary>
        /// the stream identifier.
        /// </summary>
        public string Id
        {
            get { return GetAttribute("id"); }
            set { SetAttribute("id", value); }
        }
    }
}
