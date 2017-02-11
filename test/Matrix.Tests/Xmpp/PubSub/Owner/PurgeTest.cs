using Matrix.Xml;
using Matrix.Xmpp.PubSub.Owner;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub.Owner
{
    public class PurgeTest
    {
        [Fact]
        public void ShoudBeOfTypeCollection()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.purge1.xml")).ShouldBeOfType<Purge>();
        }

        [Fact]
        public void TestPurge()
        {
            var purge = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.purge1.xml")).Cast<Purge>();
            Assert.Equal(purge.Node, "princely_musings");
        }

        [Fact]
        public void BuildPurge()
        {
            new Purge { Node = "princely_musings" }.ShouldBe(Resource.Get("Xmpp.PubSub.Owner.purge1.xml"));
        }
    }
}
