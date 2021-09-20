namespace Matrix.Transport.WebSocket.Tests
{
    using Shouldly;
    using System;
    using System.Threading.Tasks;
    using Server;
    using Xunit;

    public class AuthTests
    {
        private readonly TestContext<Startup> context;

        public AuthTests()
        {
            context = new TestContext<Startup>();
        }

        [Fact]
        public async Task LoginShouldFailWithAuthenticationException()
        {
            bool authenticated = false;
            bool binded = false;
            bool connected = false;
            
            var transport = context.CreateWebSocketTransport("stream_not_authorized");

            var xmppClient = new XmppClient(config => config.Transport = transport)
            {
                XmppDomain = "localhost",
                Username = "alex",
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

            await xmppClient.ConnectAsync().ShouldThrowAsync<AuthenticationException>();
            await xmppClient.DisconnectAsync();

            Assert.True(connected);
            Assert.False(authenticated);
            Assert.False(binded);
        }
    }
}
