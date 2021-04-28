namespace Matrix.Transport.WebSocket.Tests
{
    using Shouldly;
    using System;
    using System.Threading.Tasks;
    using Server;
    using Xmpp.Register;
    using Xmpp.XData;
    using Xunit;

    public class RegisterAccountHandler : IRegister
    {
        private XmppClient xmppClient;
        public RegisterAccountHandler(XmppClient xmppClient)
        {
            this.xmppClient = xmppClient;
        }

        public bool RegisterNewAccount => true;

        public async Task<Register> RegisterAsync(Register register)
        {
            return await Task<Register>.Run(() =>
            {
                register.RemoveAll<Data>();
                register.Username = xmppClient.Jid.Local;
                register.Password = xmppClient.Password;

                return register;
            });
        }
    }

    public class RegisterTests
    {
        private readonly TestContext<Startup> context;

        public RegisterTests()
        {
            context = new TestContext<Startup>();
        }

        [Fact]
        public async Task AccountRegistrationShouldFailWithRegisterExceptionInStep1()
        {
            bool authenticated = false;
            bool binded = false;
            bool connected = false;
            bool registered = false;

            var transport = context.CreateWebSocketTransport("stream_registeraccount_failed1");

            var xmppClient = new XmppClient(config => config.Transport = transport)
            {
                Jid = "alex@localhost",
                Password = "secret"
            };

            xmppClient.RegistrationHandler = new RegisterAccountHandler(xmppClient);

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
                    case SessionState.Registered:
                        registered = true;
                        break;
                }
            });

            await xmppClient.ConnectAsync().ShouldThrowAsync<RegisterException>();
            await xmppClient.DisconnectAsync();

            Assert.True(connected);
            Assert.False(authenticated);
            Assert.False(binded);
            Assert.False(registered);
        }

        [Fact]
        public async Task AccountRegistrationShouldFailWithRegisterExceptionInStep2()
        {
            bool authenticated = false;
            bool binded = false;
            bool connected = false;
            bool registered = false;

            var transport = context.CreateWebSocketTransport("stream_registeraccount_failed2");

            var xmppClient = new XmppClient(config => config.Transport = transport)
            {
                Jid = "alex@localhost",
                Password = "secret"
            };

            xmppClient.RegistrationHandler = new RegisterAccountHandler(xmppClient);

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
                    case SessionState.Registered:
                        registered = true;
                        break;
                }
            });

            await xmppClient.ConnectAsync().ShouldThrowAsync<RegisterException>();
            await xmppClient.DisconnectAsync();

            Assert.True(connected);
            Assert.False(authenticated);
            Assert.False(binded);
            Assert.False(registered);
        }

        [Fact]
        public async Task AccountRegistrationShouldSucceed()
        {
            bool authenticated = false;
            bool binded = false;
            bool connected = false;
            bool registered = false;

            var transport = context.CreateWebSocketTransport("stream_registeraccount_success");

            var xmppClient = new XmppClient(config => config.Transport = transport)
            {
                Jid = "alex@localhost",
                Password = "secret"
            };


            xmppClient.RegistrationHandler = new RegisterAccountHandler(xmppClient);

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
                    case SessionState.Registered:
                        registered = true;
                        break;
                }
            });

            await ShouldThrowExtensions.ShouldNotThrow(
                async () => await xmppClient.ConnectAsync()
            );

            await xmppClient.DisconnectAsync();

            Assert.True(connected);
            Assert.True(authenticated);
            Assert.True(binded);
            Assert.True(registered);
        }
    }
}
