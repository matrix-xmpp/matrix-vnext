namespace Matrix.Transport.WebSocket.Tests
{
    using Matrix.Xml;
    using System;
    using System.Reactive.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Server;
    using Xmpp.Client;
    using Xmpp.Session;
    using Xunit;

    public class SessionTests
    {
        private readonly TestContext<Startup> context;
        
        public SessionTests()
        {
            context = new TestContext<Startup>();
        }

        [Fact]
        public async Task SessionShouldbeStartedAfterResourceBiding()
        {
            var transport = context.CreateWebSocketTransport("stream2");

            bool authenticated = false;
            bool binded = false;
            bool connected = false;
            bool disconnected = false;
            bool sessionIqReceived = false;
            string sessionIqId = String.Empty;

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
                    case SessionState.Disconnected:
                        disconnected = true;
                        break;
                    case SessionState.Authenticated:
                        authenticated = true;
                        break;
                    case SessionState.Binded:
                        binded = true;
                        break;
                }
            });

            xmppClient.Transport.BeforeXmlSent
                .Where(el =>
                    el.OfType<Iq>() 
                    && el.Cast<Iq>().Type == Xmpp.IqType.Set 
                    && el.Cast<Iq>().Query.OfType<Session>())
                .Subscribe(el =>
                {
                    sessionIqId = el.Cast<Iq>().Id;
                });


            xmppClient.XmppXElementReceived
                .Where(el =>
                    el.OfType<Iq>()
                    && el.Cast<Iq>().Type == Xmpp.IqType.Result
                    && el.Cast<Iq>().Id == sessionIqId)
                .Subscribe(_ =>
                {
                    sessionIqReceived = true;
                });

            await xmppClient.ConnectAsync(CancellationToken.None);
            await xmppClient.DisconnectAsync();

            Assert.True(connected);
            Assert.True(disconnected);
            Assert.True(authenticated);
            Assert.True(binded);
            Assert.True(sessionIqReceived);
        }

    }
}
