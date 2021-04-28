namespace Matrix.Transport.WebSocket.Tests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Server;
    using Xunit;

    public class DisconnectTests
    {
        private readonly TestContext<Startup> context;
        
        public DisconnectTests()
        {
            context = new TestContext<Startup>();
        }

        [Fact]
        public async Task ShouldReceiceDisconnectStateWhenServerIsClosingTheStream()
        {
            var transport = context.CreateWebSocketTransport("stream_disconnect_server_is_closing");

            bool authenticated = false;
            bool binded = false;
            bool connected = false;
            bool disconnected = false;

            var xmppClient = new XmppClient(config => config.Transport = transport)
            {
                Jid = "alex@localhost",
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
                    case SessionState.Disconnected:
                        disconnected = true;
                        break;
                }
            });
            
            await xmppClient.ConnectAsync(CancellationToken.None);
            await xmppClient.DisconnectAsync();

            Assert.True(connected);
            Assert.True(authenticated);
            Assert.True(binded);
            Assert.True(disconnected);
        }
    }
}
