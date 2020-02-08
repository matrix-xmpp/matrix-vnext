/*
 * Copyright (c) 2003-2020 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
 * See the COPYING file for more information.
 *
 * This file is part of the MatriX project.
 *
 * NOTICE: All information contained herein is, and remains the property
 * of AG-Software and its suppliers, if any.
 * The intellectual and technical concepts contained herein are proprietary
 * to AG-Software and its suppliers and may be covered by German and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 *
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from AG-Software.
 *
 * Contact information for AG-Software is available at http://www.ag-software.de
 */

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

        [Fact]
        public void TestOwnerSubscriptions()
        {
            var ss =
                XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.subscription2.xml"))
                    .ShouldBeOfType<Matrix.Xmpp.PubSub.Owner.Subscriptions>();

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
