using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.MessageArchiveManagement
{
    [XmppTag(Name = "prefs", Namespace = Namespaces.MessageArchiveManagement)]
    public class Preferences : XmppXElement
    {
        public Preferences() : base(Namespaces.MessageArchiveManagement, "prefs")
        {
        }

        /// <summary>
        /// Gets or sets the default behaviour
        /// </summary>
        public DefaultPreference Default
        {
            get { return GetAttributeEnumUsingNameAttrib<DefaultPreference>("default"); }
            set { SetAttribute("default", value.GetName()); }
        }

        /// <summary>
        /// Gets or Sets the <see cref="Always"/> Element
        /// </summary>
        public Always Always
        {
            get { return Element<Always>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// /// Gets or Sets the <see cref="Never"/> Element
        /// </summary>
        public Never Never
        {
            get { return Element<Never>(); }
            set { Replace(value); }
        }
    }
}
