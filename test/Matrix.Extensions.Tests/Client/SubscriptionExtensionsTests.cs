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

namespace Matrix.Extensions.Tests.Client
{
    using System.Threading.Tasks;
    using Xunit;
    using Matrix.Extensions.Client.Subscription;
    using Moq;
    using Matrix.Xml;
    using Matrix.Xmpp.Client;
    using Shouldly;

    public class SubscriptionExtensionsTests
    {
        [Fact]
        public async Task ApproveSubscriptionRequestTest()
        {
            var mock = new Mock<IStanzaSender>();

            XmppXElement result = null;
            mock.Setup(s => s.SendAsync(It.IsAny<XmppXElement>()))
                .Callback<XmppXElement>((el) => result = el)
                .Returns(Task.CompletedTask);

            string expectedResult = "<presence xmlns='jabber:client' to='user@server.com' type='subscribed'/>";

            await mock.Object.ApproveSubscriptionRequestAsync(new Jid("user@server.com"));

            result.ShouldBe(expectedResult);
        }

        [Fact]
        public async Task DenySubscriptionRequestTest()
        {
            var mock = new Mock<IStanzaSender>();

            XmppXElement result = null;
            mock.Setup(s => s.SendAsync(It.IsAny<XmppXElement>()))
                .Callback<XmppXElement>((el) => result = el)
                .Returns(Task.CompletedTask);

            string expectedResult = "<presence xmlns='jabber:client' to='user@server.com' type='unsubscribed'/>";

            await mock.Object.DenySubscriptionRequestAsync(new Jid("user@server.com"));

            result.ShouldBe(expectedResult);
        }

        [Fact]
        public async Task CancelSubscriptionRequestTest()
        {
            var mock = new Mock<IStanzaSender>();

            XmppXElement result = null;
            mock.Setup(s => s.SendAsync(It.IsAny<XmppXElement>()))
                .Callback<XmppXElement>((el) => result = el)
                .Returns(Task.CompletedTask);

            string expectedResult = "<presence xmlns='jabber:client' to='user@server.com' type='unsubscribed'/>";

            await mock.Object.CancelSubscriptionAsync(new Jid("user@server.com"));

            result.ShouldBe(expectedResult);
        }

        [Fact]
        public async Task SubscribeTest()
        {
            var mock = new Mock<IStanzaSender>();

            XmppXElement result = null;
            mock.Setup(s => s.SendAsync(It.IsAny<XmppXElement>()))
                .Callback<XmppXElement>((el) => result = el)
                .Returns(Task.CompletedTask);

            string expectedResult = "<presence xmlns='jabber:client' to='user@server.com' type='subscribe'/>";

            await mock.Object.SubscribeAsync(new Jid("user@server.com"));

            result.ShouldBe(expectedResult);
        }
    }
}
