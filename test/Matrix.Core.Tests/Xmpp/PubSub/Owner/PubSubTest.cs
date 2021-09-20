using Matrix.Xml;
using Matrix.Xmpp.PubSub.Owner;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub.Owner
{
    public class PubSubTest
    {
        [Fact]
        public void TestBuildPubsubSubscriptions()
        {
            var ps = new Matrix.Xmpp.PubSub.Owner.PubSub { Type = PubSubOwnerType.Subscriptions, Subscriptions = { Node = "princely_musings" } };
            ps.ShouldBe(Resource.Get("Xmpp.PubSub.Owner.pubsub1.xml"));

            var ps2 = new Matrix.Xmpp.PubSub.Owner.PubSub
            {
                Type = PubSubOwnerType.Delete,
                Delete = { Node = "princely_musings" }
            };
            ps2.ShouldBe(Resource.Get("Xmpp.PubSub.Owner.pubsub2.xml"));


            var ps3 = new Matrix.Xmpp.PubSub.Owner.PubSub
            {
                Type = PubSubOwnerType.Purge,
                Purge = { Node = "princely_musings" }
            };

            ps3.ShouldBe(Resource.Get("Xmpp.PubSub.Owner.pubsub3.xml"));
        }

        [Fact]
        public void TestPubsubSubscriptions()
        {
            var ps = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.pubsub1.xml")).Cast<Matrix.Xmpp.PubSub.Owner.PubSub>(); 
            Assert.Equal(PubSubOwnerType.Subscriptions, ps.Type);
            Assert.NotEqual(PubSubOwnerType.Purge, ps.Type);

            ps = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.pubsub2.xml")).Cast<Matrix.Xmpp.PubSub.Owner.PubSub>();
            Assert.Equal(PubSubOwnerType.Delete, ps.Type);
            Assert.Equal("princely_musings", ps.Delete.Node);

            ps = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.pubsub3.xml")).Cast<Matrix.Xmpp.PubSub.Owner.PubSub>();
            Assert.Equal(PubSubOwnerType.Purge, ps.Type);
            Assert.Equal("princely_musings", ps.Purge.Node);
        }
    }
}
