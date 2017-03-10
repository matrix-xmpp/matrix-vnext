using System.Net;
using System.Threading.Tasks;
using Xunit;
using Matrix.Network.Resolver;
using Shouldly;
using System.Security.Authentication;
using System;
using DotNetty.Codecs;

namespace Matrix.Tests.ClientEnd2End
{
    public class ClientTests : NettyBaseServer       
    {
        [Fact]
        public async Task LoginShouldFailWithAuthenticationException()
        {
            var testPromise = new TaskCompletionSource<bool>();
            Func<Task> closeServerFunc = await StartServerAsync(ch =>
            {
                ch.Pipeline.AddLast(
                    new StringEncoder(),
                    new StringDecoder(),
                    new TestServerHandler("ClientEnd2End.stream_not_authorized.xml"));
            }, testPromise);
            
            try
            {
                var xmppClient = new XmppClient()
                {
                    Username = "alex",
                    Password = "secret",
                    XmppDomain = "localhost",
                    HostnameResolver = new StaticNameResolver(IPAddress.Parse("127.0.0.1"), 5222)
                };

                ShouldThrowExtensions.ShouldThrow<AuthenticationException>(
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
        public async Task LoginShouldFailWithBindException()
        {
            var testPromise = new TaskCompletionSource<bool>();
            Func<Task> closeServerFunc = await StartServerAsync(ch =>
            {
                ch.Pipeline.AddLast(
                    new StringEncoder(),
                    new StringDecoder(),
                    new TestServerHandler("ClientEnd2End.stream_resource_conflict.xml"));
            }, testPromise);
            
            try
            {
                var xmppClient = new XmppClient()
                {
                    Username = "alex",
                    Password = "secret",
                    XmppDomain = "localhost",
                    HostnameResolver = new StaticNameResolver(IPAddress.Parse("127.0.0.1"), 5222)
                };

                ShouldThrowExtensions.ShouldThrow<BindException>(
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
