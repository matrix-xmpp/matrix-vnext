namespace Matrix.Transport.WebSocket.Tests.Xml
{
    using Matrix.Transport.WebSocket.Xml;
    using Matrix.Xml;
    using Matrix.Xmpp;
    using Shouldly;
    using System.Linq;
    using System.Reflection;
    using Xunit;

    public class MetaTest
    {
        private const string XML_HOST_META = @"<?xml version='1.0' encoding='utf-8'?>
<XRD xmlns='http://docs.oasis-open.org/ns/xri/xrd-1.0'>
  <Link rel=""urn:xmpp:alt-connections:xbosh""
        href=""https://web.example.com:5280/bosh"" />
  <Link rel=""urn:xmpp:alt-connections:websocket""
        href=""wss://web.example.com:443/ws"" />
</XRD>";

        public MetaTest()
        {
            Factory.RegisterElementsFromAssembly(typeof(HostMeta).GetTypeInfo().Assembly);
        }

        private const string XML_LINK = @"<Link xmlns=""http://docs.oasis-open.org/ns/xri/xrd-1.0"" rel=""urn:xmpp:alt-connections:websocket"" href=""wss://web.example.com:443/ws"" />";

        [Fact]
        public void XmlShouldBeOfTypeMeta()
        {
            XmppXElement.LoadXml(XML_HOST_META)
                .ShouldBeOfType<HostMeta>();
        }

        [Fact]
        public void XmlShouldBeOfTypeLink()
        {
            XmppXElement.LoadXml(XML_LINK)
                .ShouldBeOfType<Link>();
        }

        [Fact]
        public void XmlShoulContainLinks()
        {
            var hostMeta = XmppXElement.LoadXml(XML_HOST_META).Cast<HostMeta>();
            hostMeta.Links.ToList().Count.ShouldBe(2);
            hostMeta
                .Links
                .FirstOrDefault(l => l.Rel == Namespaces.AlternativeConnectionsWebSocket)
                .Href.ShouldBe("wss://web.example.com:443/ws");
        }
    }
}
