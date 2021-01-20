using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.HttpUpload
{
    [XmppTag(Name = "header", Namespace = Namespaces.HttpUpload)]
    public class Header : XmppXElement
    {
        public Header() : base(Namespaces.HttpUpload, "header")
        {
        }

        public Header(HeaderNames headerName, string val) : base(Namespaces.HttpUpload, "header")
        {
            this.HeaderName = headerName;
            this.Value = val;
        }

        /// <summary>
        ///   Gets or sets the name of the header.
        /// </summary>
        /// <value>
        ///   The header name.
        /// </value>
        public HeaderNames HeaderName
        {
            get { return GetAttributeEnumUsingNameAttrib<HeaderNames>("name"); }
            set { SetAttribute("name", value.GetName()); }
        }
    }
}
