using Matrix.Xml;
using Matrix.Xmpp.PubSub.Owner;
using NUnit.Framework;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub.Owner
{
    
    public class PubSubTest
    {
        private const string XML1
            = @"<pubsub xmlns='http://jabber.org/protocol/pubsub#owner'>
                    <subscriptions node='princely_musings'/>
                </pubsub>";

        private const string XML2
            = @"<pubsub xmlns='http://jabber.org/protocol/pubsub#owner'>
                <delete node='princely_musings'/>
              </pubsub>";

        private const string XML3
           = @"<pubsub xmlns='http://jabber.org/protocol/pubsub#owner'>
                <purge node='princely_musings'/>
              </pubsub>";

        [Fact]
        public void Test1()
        {
            var ps = new Matrix.Xmpp.PubSub.Owner.PubSub { Type = PubSubOwnerType.Subscriptions, Subscriptions = { Node = "princely_musings" } };

            ps.ShouldBe(XML1);

            Assert.Equal(ps.Type, PubSubOwnerType.Subscriptions);
            Assert.NotEqual(ps.Type, PubSubOwnerType.Purge);
        }

        [Fact]
        public void Test2()
        {
            var xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.PubSub.Owner.PubSub);

            var ps = xmpp1 as Matrix.Xmpp.PubSub.Owner.PubSub;
            if (ps != null)
            {
                Assert.Equal(ps.Type, PubSubOwnerType.Subscriptions);
            }
        }

        [Fact]
        public void Test3()
        {
            var xmpp1 = XmppXElement.LoadXml(XML2);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.PubSub.Owner.PubSub);

            var ps = xmpp1 as Matrix.Xmpp.PubSub.Owner.PubSub;
            if (ps != null)
            {
                Assert.Equal(ps.Type, PubSubOwnerType.Delete);
                Assert.Equal(ps.Delete.Node, "princely_musings");
            }

            var ps2 = new Matrix.Xmpp.PubSub.Owner.PubSub
            {
                Type = PubSubOwnerType.Delete,
                Delete = { Node = "princely_musings" }
            };

            ps2.ShouldBe(XML2);
        }

        [Fact]
        public void Test4()
        {
            var xmpp1 = XmppXElement.LoadXml(XML3);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.PubSub.Owner.PubSub);

            var ps = xmpp1 as Matrix.Xmpp.PubSub.Owner.PubSub;
            if (ps != null)
            {
                Assert.Equal(ps.Type, PubSubOwnerType.Purge);
                Assert.Equal(ps.Purge.Node, "princely_musings");
            }

            var ps2 = new Matrix.Xmpp.PubSub.Owner.PubSub
            {
                Type = PubSubOwnerType.Purge,
                Purge = { Node = "princely_musings" }
            };

            ps2.ShouldBe(XML3);
        }
    }
}