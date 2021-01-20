using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.HttpUpload
{
    [XmppTag(Name = "slot", Namespace = Namespaces.HttpUpload)]
    public class Slot : XmppXElement
    {
        public Slot(): base(Namespaces.HttpUpload, "slot" )
        {
        }

        /// <summary>
        /// Gets or sets the Get element.
        /// </summary>
        /// <value>The Get.</value>
        public Get Get
        {
            get { return Element<Get>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// Gets or sets the Put element.
        /// </summary>
        /// <value>The Put.</value>
        public Put Put
        {
            get { return Element<Put>(); }
            set { Replace(value); }
        }
    }
}
