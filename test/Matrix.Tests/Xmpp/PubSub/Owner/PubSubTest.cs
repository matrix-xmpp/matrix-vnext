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
            Assert.Equal(ps.Type, PubSubOwnerType.Subscriptions);
            Assert.NotEqual(ps.Type, PubSubOwnerType.Purge);

            ps = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.pubsub2.xml")).Cast<Matrix.Xmpp.PubSub.Owner.PubSub>();
            Assert.Equal(ps.Type, PubSubOwnerType.Delete);
            Assert.Equal(ps.Delete.Node, "princely_musings");

            ps = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.pubsub3.xml")).Cast<Matrix.Xmpp.PubSub.Owner.PubSub>();
            Assert.Equal(ps.Type, PubSubOwnerType.Purge);
            Assert.Equal(ps.Purge.Node, "princely_musings");
        }
    }
}
