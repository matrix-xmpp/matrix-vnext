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
using System.Threading.Tasks;

using Shouldly;

using DotNetty.Codecs;

using Matrix.Network.Resolver;
using Matrix.Xmpp.Register;
using Matrix.Xmpp.XData;

using Xunit;

namespace Matrix.Tests.ClientEnd2End
{
    public partial class ClientTests : NettyBaseServer
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
                return await Task<Register>.Run(() =>
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
                    HostnameResolver = new StaticNameResolver(IPAddress.Parse("127.0.0.1"), 5222)
                };

                xmppClient.RegistrationHandler = new RegisterAccountHandler(xmppClient);

                ShouldThrowExtensions.ShouldThrow<RegisterException>(
                    () => xmppClient.ConnectAsync().GetAwaiter().GetResult()
                );

                await xmppClient.DisconnectAsync();
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
                    HostnameResolver = new StaticNameResolver(IPAddress.Parse("127.0.0.1"), 5222)
                };

                xmppClient.RegistrationHandler = new RegisterAccountHandler(xmppClient);

                ShouldThrowExtensions.ShouldThrow<RegisterException>(
                    () => xmppClient.ConnectAsync().GetAwaiter().GetResult()
                );

                await xmppClient.DisconnectAsync();
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
                    HostnameResolver = new StaticNameResolver(IPAddress.Parse("127.0.0.1"), 5222)
                };

                xmppClient.RegistrationHandler = new RegisterAccountHandler(xmppClient);

                ShouldThrowExtensions.ShouldNotThrow(
                    () => xmppClient.ConnectAsync().GetAwaiter().GetResult()
                );

                await xmppClient.DisconnectAsync();
            }
            finally
            {
                Task serverCloseTask = closeServerFunc();
                serverCloseTask.Wait(ShutdownTimeout);
            }
        }
    }
}
