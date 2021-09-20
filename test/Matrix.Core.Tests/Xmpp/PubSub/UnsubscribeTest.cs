using Matrix.Xml;
using Matrix.Xmpp.PubSub;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub
{
    public class UnSubscribeTest
    {
        [Fact]
        public void ShoudBeOfTypeUnsubscribe()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.unsubscribe1.xml")).ShouldBeOfType<Unsubscribe>();
        }

        [Fact]
        public void TestUnsubscribe()
        {
            var unsub = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.unsubscribe1.xml")).Cast<Unsubscribe>();
            Assert.Equal("princely_musings", unsub.Node);
            Assert.Equal("francisco@denmark.lit", unsub.Jid.ToString());
            Assert.Equal("abcd", unsub.SubId);
        }

        [Fact]
        public void TestBuildUnsubscribe()
        {
            var unsub = new Unsubscribe { Node = "princely_musings", Jid = "francisco@denmark.lit", SubId = "abcd" };
            unsub.ShouldBe(Resource.Get("Xmpp.PubSub.unsubscribe1.xml"));
        }
    }
}
