namespace Matrix.Transport.WebSocket.Tests
{
    using System;
    using System.Threading.Tasks;
    using Server;
    using Xunit;

    public class StreamManagementTests
    {
        private readonly TestContext<Startup> context;

        public StreamManagementTests()
        {
            context = new TestContext<Startup>();
        }

        [Fact]
        public async Task ResumeStreamShouldSucceed()
        {
            bool authenticated = false;
            bool binded = false;
            bool connected = false;
            bool resuming = false;
            bool resumed = false;

            var transport = context.CreateWebSocketTransport("stream_management_resume_success");

            var xmppClient = new XmppClient(
                config => config.Transport = transport,
                (h, c) =>
                {
                    h.Add(
                        new StreamManagementHandler(c)
                        {
                            StreamId = "b39f4f76-da2a-4785-8764-1fda0e6a5435",
                            CanResume = true,
                            IsEnabled = true,
                        });
                })
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
                    case SessionState.Resuming:
                        resuming = true;
                        break;
                    case SessionState.Resumed:
                        resumed = true;
                        break;
                }
            });

            await xmppClient.ConnectAsync();
            await xmppClient.DisconnectAsync();

            Assert.True(connected);
            Assert.True(authenticated);
            Assert.False(binded);
            Assert.True(resuming);
            Assert.True(resumed);
        }

        [Fact]
        public async Task OnResumeFailureShouldContinueWithResourceBinding()
        {
            bool connected = false;
            bool authenticated = false;
            bool resumed = false;
            bool binded = false;

            var transport = context.CreateWebSocketTransport("stream_management_resume_failure");

            StreamManagementHandler sm = null;
            var xmppClient = new XmppClient(
                config => config.Transport = transport,
                (h, c) =>
                {
                    sm = new StreamManagementHandler(c)
                    {
                        StreamId = "b39f4f76-da2a-4785-8764-1fda0e6a5435",
                        IsEnabled = true,
                        CanResume = true
                    };
                    sm.IncomingStanzaCounter.Value = 99;

                    h.Add(sm);
                })
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
                    case SessionState.Resumed:
                        resumed = true;
                        break;
                }
            });

            await xmppClient.ConnectAsync();
            await xmppClient.DisconnectAsync();
            
            Assert.True(connected);
            Assert.True(authenticated);
            Assert.True(binded);
            Assert.False(resumed);
            Assert.True(sm.IsEnabled);

            Assert.False(sm.CanResume);
            Assert.Null(sm.StreamId);
        }
    }
}
