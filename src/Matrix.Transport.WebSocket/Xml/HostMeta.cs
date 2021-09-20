namespace Matrix.Transport.WebSocket.Xml
{
    using System.Collections.Generic;
    using Attributes;
    using Matrix.Xml;

    [XmppTag(Name = "XRD", Namespace = "http://docs.oasis-open.org/ns/xri/xrd-1.0")]
    public class HostMeta : XmppXElement
    {
        public HostMeta() : base("http://docs.oasis-open.org/ns/xri/xrd-1.0", "XRD")
        {
        }

        public IEnumerable<Link> Links => Elements<Link>();
    }
}
