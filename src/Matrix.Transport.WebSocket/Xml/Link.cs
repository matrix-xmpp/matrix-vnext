namespace Matrix.Transport.WebSocket.Xml
{
    using Attributes;
    using Matrix.Xml;

    /// <summary>
    ///
    /// </summary>
    [XmppTag(Name = "Link", Namespace = "http://docs.oasis-open.org/ns/xri/xrd-1.0")]
    public class Link : XmppXElement
    {
        public Link() : base("http://docs.oasis-open.org/ns/xri/xrd-1.0", "Link")
        {
        }

        public string Rel
        {
            get { return GetAttribute("rel"); }
            set { SetAttribute("rel", value); }
        }

        public string Href
        {
            get { return GetAttribute("href"); }
            set { SetAttribute("href", value); }
        }
    }
}
