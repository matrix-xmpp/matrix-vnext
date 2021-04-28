namespace Matrix.Transport.WebSocket.Tests
{
    using Shouldly;
    using System;
    using System.Threading.Tasks;
    using Server;
    using Xunit;

    public class StreamErrorTests
    {
        private readonly TestContext<Startup> context;

        public StreamErrorTests()
        {
            context = new TestContext<Startup>();
        }

        [Fact]
        public async Task LoginShouldFailWithAuthenticationException()
        {
            bool authenticated = false;
            bool binded = false;
            bool connected = false;

            var transport = context.CreateWebSocketTransport("stream_error_host_unknown");

            var xmppClient = new XmppClient(config => config.Transport = transport)
            {
                Jid = "alex@somehost",
                Password = "secret"
            };

            xmppClient.StateChanged.Subscribe(sessionState =>
            {
                switch (sessionState)
                {
                    case SessionState.Connected:
                        connected = true;
                        break;
                    case SessionState.Authenticated:
                        authenticated = true;
                        break;
                    case SessionState.Binded:
                        binded = true;
                        break;
                }
            });

            await xmppClient.ConnectAsync().ShouldThrowAsync<StreamErrorException>();
            await xmppClient.DisconnectAsync();

            Assert.True(connected);
            Assert.False(authenticated);
            Assert.False(binded);
        }
    }
}