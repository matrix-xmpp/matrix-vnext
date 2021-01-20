namespace Matrix.Transport.WebSocket.Tests.Server
{
    using System;
    using Transport;
    using WebSocket;
    using Xunit.Abstractions;

    public class TestContext<TStartup> where TStartup : class
    {
        private readonly TestServerApplicationFactory<TStartup> factory;

        public TestContext()
        {
            factory = new TestServerApplicationFactory<TStartup>();
        }

        public TestContext(ITestOutputHelper output)
        {
            factory = new TestServerApplicationFactory<TStartup>();
        }

        public Uri GetWebSocketUri(string path)
        {
            var httpClient = factory.CreateClient(); // This is needed since _factory.Server would otherwise be null
            
            return new UriBuilder(factory.Server.BaseAddress)
            {
                Scheme = "ws",
                Path = path
            }.Uri;
        }

        public WebSocketTransport CreateWebSocketTransport(string testStream)
        {
            return new WebSocketTransport(
                async (uri, token) =>
                {
                    var webSocketClient = factory.Server.CreateWebSocketClient();
                    var ws = await webSocketClient.ConnectAsync(uri, token).ConfigureAwait(false);
                    return ws;
                })
            {
                Resolver = new StaticNameResolver(GetWebSocketUri(testStream))
            };
        }
    }
}