using Matrix.Xml;
using Matrix.Xmpp.Client;
using Xunit;
using Shouldly;

namespace Matrix.Tests.Xmpp.Chatstates
{
    public class ChatstatesTest
    {
        [Fact]
        public void TestMessageStates()
        {
            var expectedXml = XmppXElement.LoadXml(Resource.Get("Xmpp.Chatstates.message2.xml")).Cast<Message>();
            var msg = XmppXElement.LoadXml(Resource.Get("Xmpp.Chatstates.message1.xml")).Cast<Message>();

            msg.Chatstate.ShouldBe(Matrix.Xmpp.Chatstates.Chatstate.Active);
            msg.Chatstate.ShouldNotBe(Matrix.Xmpp.Chatstates.Chatstate.Composing);
            msg.Chatstate.ShouldNotBe(Matrix.Xmpp.Chatstates.Chatstate.Gone);

            msg.Chatstate = Matrix.Xmpp.Chatstates.Chatstate.None;
            msg.ShouldBe(expectedXml);
        }

        [Fact]
        public void TestMessageStates2()
        {
            var expectedXml = XmppXElement.LoadXml(Resource.Get("Xmpp.Chatstates.message1.xml")).Cast<Message>();
            var msg = XmppXElement.LoadXml(Resource.Get("Xmpp.Chatstates.message2.xml")).Cast<Message>();
            msg.Chatstate.ShouldBe(Matrix.Xmpp.Chatstates.Chatstate.None);
            msg.Chatstate = Matrix.Xmpp.Chatstates.Chatstate.Active;
            msg.ShouldBe(expectedXml);
        }

        [Fact]
        public void BuildChatStateMessage()
        {
            var expectedXml = XmppXElement.LoadXml(Resource.Get("Xmpp.Chatstates.message3.xml")).Cast<Message>();
            new Message
                {
                    Type = Matrix.Xmpp.MessageType.Chat,
                    Chatstate = Matrix.Xmpp.Chatstates.Chatstate.Gone
                }
                .ShouldBe(expectedXml);
        }
    }
}