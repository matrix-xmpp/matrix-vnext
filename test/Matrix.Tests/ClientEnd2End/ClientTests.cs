/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
 * See the COPYING file for more information.
 *
 * This file is part of the MatriX project.
 *
 * NOTICE: All information contained herein is, and remains the property
 * of AG-Software and its suppliers, if any.
 * The intellectual and technical concepts contained herein are proprietary
 * to AG-Software and its suppliers and may be covered by German and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 *
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from AG-Software.
 *
 * Contact information for AG-Software is available at http://www.ag-software.de
 */

using System.Net;
using System.Threading.Tasks;
using Xunit;
using Matrix.Network.Resolver;
using Shouldly;
using System;
using DotNetty.Codecs;
using System.Threading;

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
                    Username    = "alex",
                    Password    = "secret",
                    XmppDomain  = "localhost",
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
        
        [Fact]
        public async Task LoginShouldFailWithStreamErrorException()
        {
            var testPromise = new TaskCompletionSource<bool>();
            Func<Task> closeServerFunc = await StartServerAsync(ch =>
            {
                ch.Pipeline.AddLast(
                    new StringEncoder(),
                    new StringDecoder(),
                    new TestServerHandler("ClientEnd2End.stream_error_host_unknown.xml"));
            }, testPromise);

            try
            {
                var xmppClient = new XmppClient()
                {
                    Username = "alex",
                    Password = "secret",
                    XmppDomain = "unknown",
                    HostnameResolver = new StaticNameResolver(IPAddress.Parse("127.0.0.1"), 5222)
                };

                ShouldThrowExtensions.ShouldThrow<StreamErrorException>(
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
        public async Task LoginShouldFailWithOperationCanceledException()
        {
            var testPromise = new TaskCompletionSource<bool>();
            Func<Task> closeServerFunc = await StartServerAsync(ch =>
            {
                ch.Pipeline.AddLast(
                    new StringEncoder(),
                    new StringDecoder(),
                    new TestServerHandler("ClientEnd2End.stream_no_server_reply_to_bind.xml"));
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

                CancellationTokenSource cts = new CancellationTokenSource();
                cts.Token.ThrowIfCancellationRequested();

                await Task.Factory.StartNew(async() => {
                    await Task.Delay(5000);
                    cts.Cancel();
                });

                ShouldThrowExtensions.ShouldThrow<OperationCanceledException>(
                     () => xmppClient.ConnectAsync(cts.Token).GetAwaiter().GetResult());
            }
            finally
            {
                Task serverCloseTask = closeServerFunc();
                serverCloseTask.Wait(ShutdownTimeout);
            }
        }


        [Fact]
        public async Task LoginShouldFailWithCompressionException()
        {
            var testPromise = new TaskCompletionSource<bool>();
            Func<Task> closeServerFunc = await StartServerAsync(ch =>
            {
                ch.Pipeline.AddLast(
                    new StringEncoder(),
                    new StringDecoder(),
                    new TestServerHandler("ClientEnd2End.stream_compression_failed.xml"));
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

                ShouldThrowExtensions.ShouldThrow<CompressionException>(
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
