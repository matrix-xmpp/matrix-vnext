using Matrix.Xml;
using Matrix.Xmpp.PubSub;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub
{
    public class SubscribeTest
    {
        [Fact]
        public void ShoudBeOfTypeSubscribe()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.subscribe1.xml")).ShouldBeOfType<Subscribe>();
        }

        [Fact]
        public void TestSubscribe()
        {
            var sub = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.subscribe1.xml")).Cast<Subscribe>();
            Assert.Equal(sub.Node, "princely_musings");
            Assert.Equal(sub.Jid.ToString(), "francisco@denmark.lit");
        }

        [Fact]
        public void TestBuildSubscribe()
        {
            var sub = new Subscribe { Node = "princely_musings", Jid = "francisco@denmark.lit" };
            sub.ShouldBe(Resource.Get("Xmpp.PubSub.subscribe1.xml"));
        }
    }
}
