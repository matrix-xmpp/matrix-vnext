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

namespace Matrix.Extensions.Tests.Client
{
    using System.Threading.Tasks;
    using Xunit;
    using Matrix.Extensions.Client.Message;
    using Moq;
    using Matrix.Xml;
    using Matrix.Xmpp.Client;
    using Shouldly;

    public class MessageExtensionsTests
    {
        [Fact]
        public async Task Send_Chat_Message_Test()
        {
            var mock = new Mock<IStanzaSender>();

            XmppXElement result = null;
            mock.Setup(s => s.SendAsync(It.IsAny<XmppXElement>()))
                .Callback<XmppXElement>((el) => result = el)
                .Returns(Task.CompletedTask);            

            string expectedResult = "<message xmlns='jabber:client' to='user@server.com' type='chat'><body>hello world</body></message>";

            await mock.Object.SendChatMessageAsync(new Jid("user@server.com"), "hello world", false);

            result.ShouldBe(expectedResult);
        }

        [Fact]
        public async Task Send_Chat_Message_With_Auto_Id_Test()
        {
            var mock = new Mock<IStanzaSender>();

            XmppXElement result = null;
            mock.Setup(s => s.SendAsync(It.IsAny<XmppXElement>()))
                .Callback<XmppXElement>((el) => result = el)
                .Returns(Task.CompletedTask);

            await mock.Object.SendChatMessageAsync(new Jid("user@server.com"), "hello world", true);

            result.Cast<Message>().Id.ShouldNotBeNull();
            result.Cast<Message>().Id.Length.ShouldBeGreaterThan(1);
        }

        [Fact]
        public async Task Send_Group_Chat_Message_Test()
        {
            var mock = new Mock<IStanzaSender>();

            XmppXElement result = null;
            mock.Setup(s => s.SendAsync(It.IsAny<XmppXElement>()))
                .Callback<XmppXElement>((el) => result = el)
                .Returns(Task.CompletedTask);

            string expectedResult = "<message xmlns='jabber:client' to='user@server.com' type='groupchat'><body>hello world</body></message>";

            await mock.Object.SendGroupChatMessageAsync(new Jid("user@server.com"), "hello world", false);

            result.ShouldBe(expectedResult);
        }

        [Fact]
        public async Task Send_Group_Chat_Message_With_Auto_Id_Test()
        {
            var mock = new Mock<IStanzaSender>();

            XmppXElement result = null;
            mock.Setup(s => s.SendAsync(It.IsAny<XmppXElement>()))
                .Callback<XmppXElement>((el) => result = el)
                .Returns(Task.CompletedTask);

            await mock.Object.SendGroupChatMessageAsync(new Jid("user@server.com"), "hello world", true);

            result.Cast<Message>().Id.ShouldNotBeNull();
            result.Cast<Message>().Id.Length.ShouldBeGreaterThan(1);
        }
    }
}
