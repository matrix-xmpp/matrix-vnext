/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
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

using System;
using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.PubSub.Event;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub.Event
{
    public class EventTest
    {
        [Fact]
        public void ShoudBeOfTypeEvent()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.event1.xml")).ShouldBeOfType<Matrix.Xmpp.PubSub.Event.Event>();
        }

        [Fact]
        public void TestEvent()
        {
            var ev = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.event1.xml")).Cast<Matrix.Xmpp.PubSub.Event.Event>();
            
            Assert.Equal(ev.Type, PubSubEventType.Items);
            Assert.NotEqual(ev.Type, PubSubEventType.Delete);

            var items = ev.Items;
            Assert.Equal(items.GetItems().Count(), 2);
            Assert.NotEqual(items.GetItems().Count(), 5);

            Assert.Equal(items.GetItems().ToArray()[0].Id, "ab");
            Assert.Equal(items.GetItems().ToArray()[1].Id, "cd");


            ev = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.event2.xml")).Cast<Matrix.Xmpp.PubSub.Event.Event>();

            Assert.Equal(ev.Type, PubSubEventType.Items);
            items = ev.Items;
            Assert.Equal(items.GetRetracts().Count(), 1);
            Assert.NotEqual(items.GetRetracts().Count(), 2);
            Assert.Equal(items.GetRetracts().ToArray()[0].Id, "ae890ac52d0df67ed7cfdf51b644e901");
            Assert.NotEqual(items.GetRetracts().ToArray()[0].Id, "_ae890ac52d0df67ed7cfdf51b644e901");


            ev = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.event3.xml")).Cast<Matrix.Xmpp.PubSub.Event.Event>();
            Assert.Equal(ev.Type, PubSubEventType.Configuration);
            Assert.NotEqual(ev.Type, PubSubEventType.Delete);


            ev = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.event4.xml")).Cast<Matrix.Xmpp.PubSub.Event.Event>();
            Assert.NotEqual(ev.Type, PubSubEventType.Items);
            Assert.Equal(ev.Type, PubSubEventType.Delete);
            Assert.Equal(ev.Delete.Node, "princely_musings");

            ev = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.event5.xml")).Cast<Matrix.Xmpp.PubSub.Event.Event>();
            Assert.NotEqual(ev.Type, PubSubEventType.Items);
            Assert.Equal(ev.Type, PubSubEventType.Purge);
            Assert.Equal(ev.Purge.Node, "princely_musings");
        }

        [Fact]
        public void TestBuildEvent()
        {
            var ev2 = new Matrix.Xmpp.PubSub.Event.Event
            {
                Type = PubSubEventType.Items,
                Items = {Node = "princely_musings"}
            };
            ev2.Items.AddRetract(new Retract {Id = "ae890ac52d0df67ed7cfdf51b644e901"});
            ev2.ShouldBe(Resource.Get("Xmpp.PubSub.Event.event2.xml"));


            ev2 = new Matrix.Xmpp.PubSub.Event.Event { Type = PubSubEventType.Delete };

            Assert.Equal(ev2.Delete != null, true);

            ev2.Type = PubSubEventType.Items;
            ev2.Items.Node = "princely_musings";
            ev2.Items.AddRetract(new Retract { Id = "ae890ac52d0df67ed7cfdf51b644e901" });
            ev2.ShouldBe(Resource.Get("Xmpp.PubSub.Event.event2.xml"));


            var ev3 = new Matrix.Xmpp.PubSub.Event.Event
            {
                Type = PubSubEventType.Purge,
                Purge = { Node = "princely_musings" }
            };
            ev3.ShouldBe(Resource.Get("Xmpp.PubSub.Event.event5.xml"));
        }
        
        [Fact]
        public void Test3()
        {
            var ev2 = new Matrix.Xmpp.PubSub.Event.Event
                          {
                              Configuration = new Configuration {Node = "princely_musings"}
                          };
            ev2.ShouldBe(Resource.Get("Xmpp.PubSub.Event.event3.xml"));
        }

        [Fact]
        public void TestEvent6()
        {
            var ev = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.event6.xml")).Cast<Matrix.Xmpp.PubSub.Event.Event>();
            Assert.NotEqual(ev.Type, PubSubEventType.Items);
            Assert.Equal(ev.Type, PubSubEventType.Subscription);

            Assert.Equal(ev.Subscription.Node, "princely_musings");
            Assert.Equal(ev.Subscription.Jid.Equals("horatio@denmark.lit"), true);
            Assert.Equal(ev.Subscription.SubscriptionState, Matrix.Xmpp.PubSub.SubscriptionState.Subscribed);
        }

        [Fact]
        public void TestBuildEvent6()
        {
            var ev2 = new Matrix.Xmpp.PubSub.Event.Event
            {
                Type = PubSubEventType.Subscription,
                Subscription =
                    {
                        Node = "princely_musings",
                        Jid = "horatio@denmark.lit",
                        SubscriptionState = Matrix.Xmpp.PubSub.SubscriptionState.Subscribed
                    }
            };
            ev2.ShouldBe(Resource.Get("Xmpp.PubSub.Event.event6.xml"));
        }
        
        [Fact]
        public void TestEvent7()
        {
            DateTime dt = DateTime.MinValue;
            XmppXElement xmpp1 = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.event7.xml")).Cast<Matrix.Xmpp.PubSub.Event.Event>();
            
            var ev = xmpp1 as Matrix.Xmpp.PubSub.Event.Event;
            Assert.NotEqual(ev.Type, PubSubEventType.Items);
            Assert.Equal(ev.Type, PubSubEventType.Subscription);

            Assert.Equal(ev.Subscription.Node, "princely_musings");
            Assert.Equal(ev.Subscription.Jid.Equals("francisco@denmark.lit"), true);
            Assert.Equal(ev.Subscription.Id, "ba49252aaa4f5d320c24d3766f0bdcade78c78d3");
            Assert.Equal(ev.Subscription.SubscriptionState, Matrix.Xmpp.PubSub.SubscriptionState.Subscribed);

            dt = ev.Subscription.Expiry;
            

            var ev2 = new Matrix.Xmpp.PubSub.Event.Event
                          {
                              Type = PubSubEventType.Subscription,
                              Subscription =
                                  {
                                      Node = "princely_musings",
                                      Jid = "francisco@denmark.lit",
                                      Id = "ba49252aaa4f5d320c24d3766f0bdcade78c78d3",
                                      SubscriptionState = Matrix.Xmpp.PubSub.SubscriptionState.Subscribed,
                                      Expiry = dt
                                  }
                          };
            
            ev2.ShouldBe(Resource.Get("Xmpp.PubSub.Event.event7.xml"));


            ev2 = new Matrix.Xmpp.PubSub.Event.Event
            {
                Type = PubSubEventType.Subscription,
                Subscription =
                                  {
                                      Node = "princely_musings",
                                      Jid = "francisco@denmark.lit",
                                      Id = "ba49252aaa4f5d320c24d3766f0bdcade78c78d3",
                                      SubscriptionState = Matrix.Xmpp.PubSub.SubscriptionState.Subscribed,
                                      Expiry = dt
                                  }
            };
            ev2.ShouldBe(Resource.Get("Xmpp.PubSub.Event.event7.xml"));
        }
    }
}
