/*
 * Copyright (c) 2003-2020 by AG-Software <info@ag-software.de>
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

using System;
using System.Net;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Shouldly;

using DotNetty.Codecs;

using Matrix.Network.Resolver;

using Xunit;
using Matrix.Network.Handlers;
using DotNetty.Transport.Channels;

namespace Matrix.Tests.ClientEnd2End
{
    public partial class ClientTests : NettyBaseServer
    {
        [Fact]
        public async Task ResumeShouldSucceed()
        {
            var testPromise = new TaskCompletionSource<bool>();
            Func<Task> closeServerFunc = await StartServerAsync(ch =>
            {
                ch.Pipeline.AddLast(
                    new StringEncoder(),
                    new StringDecoder(),
                    new TestServerHandler("ClientEnd2End.stream_management_resume_success.xml"));
            }, testPromise);

            try
            {
                bool resumedStateFired = false;

                StreamManagementHandler smHandler = null;

                var xmppClient = new XmppClient(
                    new Action<IChannelPipeline, ISession>((pipeline, session) =>
                    {
                        pipeline.AddBefore<XmppStanzaHandler>(smHandler);
                    })
                )
                {
                    Username = "alex",
                    Password = "secret",
                    XmppDomain = "localhost",
                    HostnameResolver = new StaticNameResolver(IPAddress.Parse("127.0.0.1"), 5222)
                };

                smHandler = new StreamManagementHandler(xmppClient)
                {
                    StreamId = "b39f4f76-da2a-4785-8764-1fda0e6a5435",
                    CanResume = true,
                    IsEnabled = true,           
                };

                smHandler.IncomingStanzaCounter.Value = 99;

                xmppClient
                    .XmppSessionState
                    .Subject
                    .DistinctUntilChanged()
                    .Subscribe(st =>
                    {
                        if (st == SessionState.Resumed)
                        {
                            resumedStateFired = true;
                        }
                    });


                ShouldThrowExtensions.ShouldNotThrow(
                   () => xmppClient.ConnectAsync().GetAwaiter().GetResult()
               );

                await xmppClient.DisconnectAsync();

                resumedStateFired.ShouldBeTrue();
            }
            finally
            {
                Task serverCloseTask = closeServerFunc();
                serverCloseTask.Wait(ShutdownTimeout);
            }
        }

        [Fact]
        public async Task OnResumeFailureShouldContinueWithResourceBinding()
        {
            var testPromise = new TaskCompletionSource<bool>();
            Func<Task> closeServerFunc = await StartServerAsync(ch =>
            {
                ch.Pipeline.AddLast(
                    new StringEncoder(),
                    new StringDecoder(),
                    new TestServerHandler("ClientEnd2End.stream_management_resume_failure.xml"));
            }, testPromise);

            try
            {
                bool resumedStateFired = false;
                bool bindedStateFired = false;

                StreamManagementHandler smHandler = null;

                var xmppClient = new XmppClient(
                    conf =>
                    {
                        conf.UseStreamManagement();
                    }
                )
                {
                    Username = "alex",
                    Password = "secret",
                    XmppDomain = "localhost",
                    Resource = "MatriX",
                    HostnameResolver = new StaticNameResolver(IPAddress.Parse("127.0.0.1"), 5222)
                };

                xmppClient
                    .XmppSessionState
                    .Subject
                    .DistinctUntilChanged()
                    .Subscribe(st =>
                    {
                        if (st == SessionState.Connected)
                        {
                            smHandler = xmppClient.Pipeline.Get<StreamManagementHandler>();
                            smHandler.StreamId = "b39f4f76-da2a-4785-8764-1fda0e6a5435";
                            smHandler.IsEnabled = true;
                            smHandler.CanResume = true;
                            smHandler.IncomingStanzaCounter.Value = 99;
                        }
                    });

                xmppClient
                    .XmppSessionState
                    .Subject
                    .DistinctUntilChanged()                 
                    .Subscribe(st =>
                    {
                        if (st == SessionState.Resumed)
                        {
                            resumedStateFired = true;
                        }
                    });

                xmppClient
                    .XmppSessionState
                    .Subject
                    .DistinctUntilChanged()
                    .Subscribe(st =>
                    {
                        if (st == SessionState.Binded)
                        {
                            bindedStateFired = true;
                        }
                    });


                ShouldThrowExtensions.ShouldNotThrow(
                   () => xmppClient.ConnectAsync().GetAwaiter().GetResult()
               );

                await xmppClient.DisconnectAsync();

                resumedStateFired.ShouldBeFalse();
                bindedStateFired.ShouldBeTrue();

                // because we did a manual disconnect resumption gets disabled in the handler
                smHandler.StreamId.ShouldBeNull();
                smHandler.CanResume.ShouldBeFalse();
            }
            finally
            {
                Task serverCloseTask = closeServerFunc();
                serverCloseTask.Wait(ShutdownTimeout);
            }
        }
    }
}
