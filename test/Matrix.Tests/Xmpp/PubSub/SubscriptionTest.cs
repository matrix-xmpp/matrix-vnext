using System.Collections.Generic;
using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.PubSub;
using NUnit.Framework;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub
{
    
    public class SubscriptionTest
    {
        private const string XML1
       = @"<subscriptions xmlns='http://jabber.org/protocol/pubsub' node='princely_musings'>
      <subscription jid='bernardo@denmark.lit' subscription='subscribed' subid='123'/>
      <subscription jid='bernardo2@denmark.lit' subscription='unconfigured' subid='456'/>
    </subscriptions>";

        private const string XML2
            = @"<subscriptions node='princely_musings' xmlns='http://jabber.org/protocol/pubsub#owner'>
      <subscription jid='hamlet@denmark.lit' subscription='subscribed'/>
      <subscription jid='polonius@denmark.lit' subscription='unconfigured'/>
      <subscription jid='bernardo@denmark.lit' subscription='subscribed' subid='123-abc'/>
      <subscription jid='bernardo@denmark.lit' subscription='subscribed' subid='004-yyy'/>
    </subscriptions>";

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Subscriptions);

            var subs = xmpp1 as Subscriptions;
            if (subs != null)
            {
                Assert.Equal(subs.Node, "princely_musings");
                IEnumerable<Subscription> ss = subs.GetSubscriptions();
                

                Assert.Equal(ss.Count(), 2);
                Assert.Equal(ss.ToArray()[0].Jid.Equals("bernardo@denmark.lit"), true);
                Assert.Equal(ss.ToArray()[0].SubscriptionState, SubscriptionState.Subscribed);
                Assert.Equal(ss.ToArray()[0].Id, "123");

                Assert.Equal(ss.ToArray()[1].Jid.Equals("bernardo2@denmark.lit"), true);
                Assert.Equal(ss.ToArray()[1].SubscriptionState, SubscriptionState.Unconfigured);
                Assert.Equal(ss.ToArray()[1].Id, "456");
            }
        }

        [Fact]
        public void Test2()
        {
            /*
             *  @"<subscriptions node='princely_musings' xmlns='http://jabber.org/protocol/pubsub#owner'>
      <subscription jid='hamlet@denmark.lit' subscription='subscribed'/>
      <subscription jid='polonius@denmark.lit' subscription='unconfigured'/>
      <subscription jid='bernardo@denmark.lit' subscription='subscribed' subid='123-abc'/>
      <subscription jid='bernardo@denmark.lit' subscription='subscribed' subid='004-yyy'/>
    </subscriptions>
";
             */
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML2);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.PubSub.Owner.Subscriptions);

            var ss = xmpp1 as Matrix.Xmpp.PubSub.Owner.Subscriptions;
            if (ss != null)
            {
                Assert.Equal(ss.Node, "princely_musings");
                IEnumerable<Matrix.Xmpp.PubSub.Owner.Subscription> subs = ss.GetSubscriptions();
            
                Assert.Equal(subs.Count(), 4);

                Assert.Equal(subs.ToArray()[0].Jid.Equals("hamlet@denmark.lit"), true);
                Assert.Equal(subs.ToArray()[0].SubscriptionState, SubscriptionState.Subscribed);
                Assert.Equal(subs.ToArray()[0].Id, null);

                Assert.Equal(subs.ToArray()[1].Jid.Equals("polonius@denmark.lit"), true);
                Assert.Equal(subs.ToArray()[1].SubscriptionState, SubscriptionState.Unconfigured);
                Assert.Equal(subs.ToArray()[1].Id, null);

                Assert.Equal(subs.ToArray()[2].Jid.Equals("bernardo@denmark.lit"), true);
                Assert.Equal(subs.ToArray()[2].SubscriptionState, SubscriptionState.Subscribed);
                Assert.Equal(subs.ToArray()[2].Id, "123-abc");

                Assert.Equal(subs.ToArray()[3].Jid.Equals("bernardo@denmark.lit"), true);
                Assert.Equal(subs.ToArray()[3].SubscriptionState, SubscriptionState.Subscribed);
                Assert.Equal(subs.ToArray()[3].Id, "004-yyy");
            
            }
        }

        [Fact]
        public void Test3()
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

            ss.ShouldBe(XML1);

            var ss2 = new Subscriptions {Node = "princely_musings"};

            var sub1 = ss2.AddSubscription();
            sub1.Jid = "bernardo@denmark.lit";
            sub1.Id = "123";
            sub1.SubscriptionState = SubscriptionState.Subscribed;

            var sub2 = ss2.AddSubscription();
            sub2.Jid = "bernardo2@denmark.lit";
            sub2.Id = "456";
            sub2.SubscriptionState = SubscriptionState.Unconfigured;

            ss2.ShouldBe(XML1);

            ss2.RemoveAllSubscriptions();
            // try again, it shouln't raise an error when no nodes exist
            ss2.RemoveAllSubscriptions();

            ss2.ShouldBe("<subscriptions xmlns='http://jabber.org/protocol/pubsub' node='princely_musings'/>");
        }

        [Fact]
        public void Test4()
        {
            //var affs = new Matrix.Xmpp.PubSub.Owner.Affiliations { Node = "princely_musings" };

            //affs.AddAffiliation(new Matrix.Xmpp.PubSub.Owner.Affiliation { Jid = "hamlet@denmark.lit", AffiliationType = AffiliationType.owner });
            //affs.AddAffiliation(new Matrix.Xmpp.PubSub.Owner.Affiliation { Jid = "polonius@denmark.lit", AffiliationType = AffiliationType.outcast });

            //XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML2, affs));
        }
    }
}
