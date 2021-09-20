using System.Collections.Generic;
using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.PubSub;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub
{
    public class SubscriptionTest
    {
        [Fact]
        public void ShoudBeOfTypeSubscriptions()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.subscription1.xml")).ShouldBeOfType<Subscriptions>();
        }

        [Fact]
        public void ShoudBeOfTypeOwnerSubscriptions()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.subscription2.xml")).ShouldBeOfType<Matrix.Xmpp.PubSub.Owner.Subscriptions>();
        }

        [Fact]
        public void TestSubscriptions()
        {
            var subs = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.subscription1.xml")).Cast<Subscriptions>();
         
            Assert.Equal("princely_musings", subs.Node);
            IEnumerable<Subscription> ss = subs.GetSubscriptions();

            Assert.Equal(2, ss.Count());
            Assert.True(ss.ToArray()[0].Jid.Equals("bernardo@denmark.lit"));
            Assert.Equal(SubscriptionState.Subscribed, ss.ToArray()[0].SubscriptionState);
            Assert.Equal("123", ss.ToArray()[0].Id);

            Assert.True(ss.ToArray()[1].Jid.Equals("bernardo2@denmark.lit"));
            Assert.Equal(SubscriptionState.Unconfigured, ss.ToArray()[1].SubscriptionState);
            Assert.Equal("456", ss.ToArray()[1].Id);
        }

        [Fact]
        public void TestOwnerSubscriptions()
        {
            var ss =
                XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.subscription2.xml"))
                    .ShouldBeOfType<Matrix.Xmpp.PubSub.Owner.Subscriptions>();

            Assert.Equal("princely_musings", ss.Node);
            IEnumerable<Matrix.Xmpp.PubSub.Owner.Subscription> subs = ss.GetSubscriptions();

            Assert.Equal(4, subs.Count());

            Assert.True(subs.ToArray()[0].Jid.Equals("hamlet@denmark.lit"));
            Assert.Equal(SubscriptionState.Subscribed, subs.ToArray()[0].SubscriptionState);
            Assert.Null(subs.ToArray()[0].Id);

            Assert.True(subs.ToArray()[1].Jid.Equals("polonius@denmark.lit"));
            Assert.Equal(SubscriptionState.Unconfigured, subs.ToArray()[1].SubscriptionState);
            Assert.Null(subs.ToArray()[1].Id);

            Assert.True(subs.ToArray()[2].Jid.Equals("bernardo@denmark.lit"));
            Assert.Equal(SubscriptionState.Subscribed, subs.ToArray()[2].SubscriptionState);
            Assert.Equal("123-abc", subs.ToArray()[2].Id);

            Assert.True(subs.ToArray()[3].Jid.Equals("bernardo@denmark.lit"));
            Assert.Equal(SubscriptionState.Subscribed, subs.ToArray()[3].SubscriptionState);
            Assert.Equal("004-yyy", subs.ToArray()[3].Id);
        }

        [Fact]
        public void TestBuildSubscriptions()
        {
            var ss = new Subscriptions {Node = "princely_musings"};

            ss.AddSubscription(new Subscription
                                   {
                                       Jid = "bernardo@denmark.lit",
                                       Id = "123",
                                       SubscriptionState = SubscriptionState.Subscribed
                                   });
            ss.AddSubscription(new Subscription
                                   {
                                       Jid = "bernardo2@denmark.lit",
                                       Id = "456",
                                       SubscriptionState = SubscriptionState.Unconfigured
                                   });

            ss.ShouldBe(Resource.Get("Xmpp.PubSub.subscription1.xml"));


            var ss2 = new Subscriptions {Node = "princely_musings"};

            var sub1 = ss2.AddSubscription();
            sub1.Jid = "bernardo@denmark.lit";
            sub1.Id = "123";
            sub1.SubscriptionState = SubscriptionState.Subscribed;

            var sub2 = ss2.AddSubscription();
            sub2.Jid = "bernardo2@denmark.lit";
            sub2.Id = "456";
            sub2.SubscriptionState = SubscriptionState.Unconfigured;

            ss2.ShouldBe(Resource.Get("Xmpp.PubSub.subscription1.xml"));

            ss2.RemoveAllSubscriptions();
            // try again, it shouln't raise an error when no nodes exist
            ss2.RemoveAllSubscriptions();

            ss2.ShouldBe("<subscriptions xmlns='http://jabber.org/protocol/pubsub' node='princely_musings'/>");
        }
    }
}
