namespace Matrix.Transport.WebSocket.Tests
{
    using Shouldly;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Server;
    using Xunit;
    
    public class BindTests
    {
        private readonly TestContext<Startup> context;
        
        public BindTests()
        {
            context = new TestContext<Startup>();
        }

        [Fact]
        public async Task LoginWithBindShouldSucceed()
        {
            var transport = context.CreateWebSocketTransport("stream1");

            bool authenticated = false;
            bool binded = false;
            bool connected = false;

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
                }
            });

            await xmppClient.ConnectAsync(CancellationToken.None);
            await xmppClient.DisconnectAsync();

            Assert.True(connected);
            Assert.True(authenticated);
            Assert.True(binded);
        }
        
        [Fact]
        public async Task LoginShouldFailWithBindException()
        {
            var transport = context.CreateWebSocketTransport("stream_resource_conflict");

            bool authenticated = false;
            bool binded = false;
            bool connected = false;

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
                }
            });

            await xmppClient.ConnectAsync().ShouldThrowAsync<BindException>();
            await xmppClient.DisconnectAsync();

            Assert.True(connected);
            Assert.True(authenticated);
            Assert.False(binded);
        }
    }
}
