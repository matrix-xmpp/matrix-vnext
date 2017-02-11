using System.Collections.Generic;
using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.PubSub;
using Shouldly;
using Xunit;
using Subscription=Matrix.Xmpp.PubSub.Owner.Subscription;
using Subscriptions=Matrix.Xmpp.PubSub.Owner.Subscriptions;

namespace Matrix.Tests.Xmpp.PubSub.Owner
{
    
    public class SubscriptionsTest
    {
        [Fact]
        public void ShoudBeOfTypeSubscriptions()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.subscriptions1.xml")).ShouldBeOfType<Subscriptions>();
        }

        [Fact]
        public void TestSubscriptions()
        {
            var subs = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.subscriptions1.xml")).Cast<Subscriptions>();
          
            Assert.Equal(subs.Node, "princely_musings");
            IEnumerable<Subscription> ss = subs.GetSubscriptions();


            Assert.Equal(ss.Count(), 2);
            Assert.Equal(ss.ToArray()[0].Jid.Equals("polonius@denmark.lit"), true);
            Assert.Equal(ss.ToArray()[0].SubscriptionState, SubscriptionState.None);

            Assert.Equal(ss.ToArray()[1].Jid.Equals("bard@shakespeare.lit"), true);
            Assert.Equal(ss.ToArray()[1].SubscriptionState, SubscriptionState.Subscribed);
        }

        [Fact]
        public void TestBuildSubscriptions()
        {
            var ss = new Subscriptions {Node = "princely_musings"};

            ss.AddSubscription(new Subscription
                                   {
                                       Jid = "polonius@denmark.lit",
                                       SubscriptionState = SubscriptionState.None
                                   });
            ss.AddSubscription(new Subscription
                                   {
                                       Jid = "bard@shakespeare.lit",
                                       SubscriptionState = SubscriptionState.Subscribed
                                   });

            ss.ShouldBe(Resource.Get("Xmpp.PubSub.Owner.subscriptions1.xml"));

            var ss2 = new Subscriptions {Node = "princely_musings"};

            var sub1 = ss2.AddSubscription();
            sub1.Jid = "polonius@denmark.lit";
            sub1.SubscriptionState = SubscriptionState.None;

            var sub2 = ss2.AddSubscription();
            sub2.Jid = "bard@shakespeare.lit";
            sub2.SubscriptionState = SubscriptionState.Subscribed;

            ss2.ShouldBe(Resource.Get("Xmpp.PubSub.Owner.subscriptions1.xml"));
        }
    }
}