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
