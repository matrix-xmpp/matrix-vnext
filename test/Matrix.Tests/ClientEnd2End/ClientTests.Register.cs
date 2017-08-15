using System;
using System.Net;
using System.Threading.Tasks;

using Shouldly;

using DotNetty.Codecs;

using Matrix.Network.Resolver;
using Matrix.Xmpp.Register;
using Matrix.Xmpp.XData;

using Xunit;

namespace Matrix.Tests.ClientEnd2End
{
    partial class ClientTests : NettyBaseServer
    {
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
                return await Task<Register>.Factory.StartNew(() =>
                {
                    register.RemoveAll<Data>();
                    register.Username = xmppClient.Username;
                    register.Password = xmppClient.Password;

                    return register;
                });
            }
        }

        [Fact]
        public async Task AccountRegistrationShouldFailWithRegisterExceptionInStep1()
        {
            var testPromise = new TaskCompletionSource<bool>();
            Func<Task> closeServerFunc = await StartServerAsync(ch =>
            {
                ch.Pipeline.AddLast(
                    new StringEncoder(),
                    new StringDecoder(),
                    new TestServerHandler("ClientEnd2End.stream_registeraccount_failed1.xml"));
            }, testPromise);

            try
            {
                var xmppClient = new XmppClient()
                {
                    Username = "alex",
                    Password = "secret",
                    XmppDomain = "localhost",
                    Compression = true,
                    HostnameResolver = new StaticNameResolver(IPAddress.Parse("127.0.0.1"), 5222)
                };

                xmppClient.RegistrationHandler = new RegisterAccountHandler(xmppClient);

                ShouldThrowExtensions.ShouldThrow<RegisterException>(
                    () => xmppClient.ConnectAsync().GetAwaiter().GetResult()
                );

                await xmppClient.CloseAsync();
            }
            finally
            {
                Task serverCloseTask = closeServerFunc();
                serverCloseTask.Wait(ShutdownTimeout);
            }
        }

        [Fact]
        public async Task AccountRegistrationShouldFailWithRegisterExceptionInStep2()
        {
            var testPromise = new TaskCompletionSource<bool>();
            Func<Task> closeServerFunc = await StartServerAsync(ch =>
            {
                ch.Pipeline.AddLast(
                    new StringEncoder(),
                    new StringDecoder(),
                    new TestServerHandler("ClientEnd2End.stream_registeraccount_failed2.xml"));
            }, testPromise);

            try
            {
                var xmppClient = new XmppClient()
                {
                    Username = "alex",
                    Password = "secret",
                    XmppDomain = "localhost",
                    Compression = true,
                    HostnameResolver = new StaticNameResolver(IPAddress.Parse("127.0.0.1"), 5222)
                };

                xmppClient.RegistrationHandler = new RegisterAccountHandler(xmppClient);

                ShouldThrowExtensions.ShouldThrow<RegisterException>(
                    () => xmppClient.ConnectAsync().GetAwaiter().GetResult()
                );

                await xmppClient.CloseAsync();
            }
            finally
            {
                Task serverCloseTask = closeServerFunc();
                serverCloseTask.Wait(ShutdownTimeout);
            }
        }

        [Fact]
        public async Task AccountRegistrationShouldSucceed()
        {
            var testPromise = new TaskCompletionSource<bool>();
            Func<Task> closeServerFunc = await StartServerAsync(ch =>
            {
                ch.Pipeline.AddLast(
                    new StringEncoder(),
                    new StringDecoder(),
                    new TestServerHandler("ClientEnd2End.stream_registeraccount_success.xml"));
            }, testPromise);

            try
            {
                var xmppClient = new XmppClient()
                {
                    Username = "alex",
                    Password = "secret",
                    XmppDomain = "localhost",
                    Compression = true,
                    HostnameResolver = new StaticNameResolver(IPAddress.Parse("127.0.0.1"), 5222)
                };

                xmppClient.RegistrationHandler = new RegisterAccountHandler(xmppClient);

                ShouldThrowExtensions.ShouldNotThrow(
                    () => xmppClient.ConnectAsync().GetAwaiter().GetResult()
                );

                await xmppClient.CloseAsync();
            }
            finally
            {
                Task serverCloseTask = closeServerFunc();
                serverCloseTask.Wait(ShutdownTimeout);
            }
        }
    }
}
