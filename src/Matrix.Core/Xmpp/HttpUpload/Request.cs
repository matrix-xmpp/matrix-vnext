using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.HttpUpload
{
    [XmppTag(Name = "request", Namespace = Namespaces.HttpUpload)]
    public class Request : XmppXElement
    {
        public Request() : base(Namespaces.HttpUpload, "request")
        {
        }

        /// <summary>
        ///   Gets or sets the filename.
        /// </summary>
        /// <value>
        ///   The filename.
        /// </value>
        public string Filename
        {
            get { return GetAttribute("filename"); }
            set { SetAttribute("filename", value); }
        }

        /// <summary>
        ///   Gets or sets the size.
        /// </summary>
        /// <value>
        ///   The size.
        /// </value>
        public int Size
        {
            get { return GetAttributeInt("size"); }
            set { SetAttribute("size", value); }
        }

        /// <summary>
        ///   Gets or sets the content type.
        /// </summary>
        /// <value>
        ///   The content type.
        /// </value>
        public string ContentType
        {
            get { return GetAttribute("content-type"); }
            set { SetAttribute("content-type", value); }
        }     
    }
}
