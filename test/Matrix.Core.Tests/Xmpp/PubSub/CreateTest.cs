using Matrix.Xml;
using Matrix.Xmpp.PubSub;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub
{
    public class CreateTest
    {
        [Fact]
        public void ShoudBeOfTypeConfigure()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.create1.xml")).ShouldBeOfType<Create>();
        }

        [Fact]
        public void TestCreate()
        {
            var c = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.create1.xml")).Cast<Create>(); ;
            Assert.Equal("generic/pgm-mp3-player", c.Node);
        }

        [Fact]
        public void TestBuildCreate()
        {
            var create = new Create { Node = "generic/pgm-mp3-player" };
            create.ShouldBe(Resource.Get("Xmpp.PubSub.Owner.create1.xml"));
        }
    }
}
