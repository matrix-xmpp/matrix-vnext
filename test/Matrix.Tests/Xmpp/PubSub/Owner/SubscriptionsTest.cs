using System.Collections.Generic;
using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.PubSub;
using NUnit.Framework;
using Xunit;
using Subscription=Matrix.Xmpp.PubSub.Owner.Subscription;
using Subscriptions=Matrix.Xmpp.PubSub.Owner.Subscriptions;

namespace Matrix.Tests.Xmpp.PubSub.Owner
{
    
    public class SubscriptionsTest
    {
        private const string XML1 = @"<subscriptions node='princely_musings' xmlns='http://jabber.org/protocol/pubsub#owner'>
                                          <subscription jid='polonius@denmark.lit' subscription='none'/>
                                          <subscription jid='bard@shakespeare.lit' subscription='subscribed'/>
                                        </subscriptions>";
        [Fact]
        public void Test1()
        {
            var xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Subscriptions);

            var subs = xmpp1 as Subscriptions;
            if (subs != null)
            {
                Assert.Equal(subs.Node, "princely_musings");
                IEnumerable<Subscription> ss = subs.GetSubscriptions();


                Assert.Equal(ss.Count(), 2);
                Assert.Equal(ss.ToArray()[0].Jid.Equals("polonius@denmark.lit"), true);
                Assert.Equal(ss.ToArray()[0].SubscriptionState, SubscriptionState.None);

                Assert.Equal(ss.ToArray()[1].Jid.Equals("bard@shakespeare.lit"), true);
                Assert.Equal(ss.ToArray()[1].SubscriptionState, SubscriptionState.Subscribed);
                
            }
        }

        [Fact]
        public void Test2()
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

            ss.ShouldBe(XML1);

            var ss2 = new Subscriptions {Node = "princely_musings"};

            var sub1 = ss2.AddSubscription();
            sub1.Jid = "polonius@denmark.lit";
            sub1.SubscriptionState = SubscriptionState.None;

            var sub2 = ss2.AddSubscription();
            sub2.Jid = "bard@shakespeare.lit";
            sub2.SubscriptionState = SubscriptionState.Subscribed;

            ss2.ShouldBe(XML1);
        }
    }
}