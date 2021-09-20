namespace Matrix.Transport.WebSocket.Tests
{
    using Matrix.Transport.WebSocket;
    using Shouldly;
    using Xunit;

    public class HostsTests
    {
        [Fact]
        public void Given_JsonMeta_Should_Return_Hosts_Uri()
        {
            string json = "{\"links\":[{\"rel\":\"urn:xmpp:alt-connections:xbosh\",\"href\":\"https://palaver.im/bosh\"},{\"rel\":\"urn:xmpp:alt-connections:websocket\",\"href\":\"wss://palaver.im/ws\"}]}";
            var resolver = new WebSocketUriResolver();
            resolver.GetUriFromJsonMetadata(json)
                .AbsoluteUri
                .ShouldBe("wss://palaver.im/ws");
        }

        [Fact]
        public void Given_JsonMeta_When_Rel_Missing_Should_Return_Null()
        {
            string json = "{\"links\":[{\"rel\":\"urn:xmpp:alt-connections:xbosh\", \"href\":\"https://palaver.im/bosh\"}]}";
            var resolver = new WebSocketUriResolver();
            resolver.GetUriFromJsonMetadata(json)
                .ShouldBeNull();
        }

        [Fact]
        public void Given_XmlMeta_Should_Return_Hosts_Uri()
        {
            const string XML_HOST_META = @"<?xml version='1.0' encoding='utf-8'?>
<XRD xmlns='http://docs.oasis-open.org/ns/xri/xrd-1.0'>
  <Link rel=""urn:xmpp:alt-connections:xbosh""
        href=""https://web.example.com:5280/bosh"" />
  <Link rel=""urn:xmpp:alt-connections:websocket""
        href=""wss://web.example.com/ws"" />
</XRD>";

            var resolver = new WebSocketUriResolver();
            resolver.GetUriFromXmlMetadata(XML_HOST_META)
                .AbsoluteUri
                .ShouldBe("wss://web.example.com/ws");
        }

        [Fact]
        public async void Given_XmppDomain_Should_Return_Host()
        {
            var resolver = new WebSocketUriResolver();
            var uri = await resolver.ResolveUriAsync("palaver.im");

            //var uri = await resolver.ResolveAsync("conversations.im");

            uri.AbsoluteUri
                .ShouldBe("wss://palaver.im/ws");
        }

    }
}
