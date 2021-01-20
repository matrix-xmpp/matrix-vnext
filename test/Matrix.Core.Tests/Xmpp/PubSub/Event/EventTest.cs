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
            
            Assert.Equal(PubSubEventType.Items, ev.Type);
            Assert.NotEqual(PubSubEventType.Delete, ev.Type);

            var items = ev.Items;
            Assert.Equal(2, items.GetItems().Count());
            Assert.NotEqual(5, items.GetItems().Count());

            Assert.Equal("ab", items.GetItems().ToArray()[0].Id);
            Assert.Equal("cd", items.GetItems().ToArray()[1].Id);


            ev = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.event2.xml")).Cast<Matrix.Xmpp.PubSub.Event.Event>();

            Assert.Equal(PubSubEventType.Items, ev.Type);
            items = ev.Items;
            Assert.Single(items.GetRetracts());
            Assert.NotEqual(2, items.GetRetracts().Count());
            Assert.Equal("ae890ac52d0df67ed7cfdf51b644e901", items.GetRetracts().ToArray()[0].Id);
            Assert.NotEqual("_ae890ac52d0df67ed7cfdf51b644e901", items.GetRetracts().ToArray()[0].Id);


            ev = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.event3.xml")).Cast<Matrix.Xmpp.PubSub.Event.Event>();
            Assert.Equal(PubSubEventType.Configuration, ev.Type);
            Assert.NotEqual(PubSubEventType.Delete, ev.Type);


            ev = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.event4.xml")).Cast<Matrix.Xmpp.PubSub.Event.Event>();
            Assert.NotEqual(PubSubEventType.Items, ev.Type);
            Assert.Equal(PubSubEventType.Delete, ev.Type);
            Assert.Equal("princely_musings", ev.Delete.Node);

            ev = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.event5.xml")).Cast<Matrix.Xmpp.PubSub.Event.Event>();
            Assert.NotEqual(PubSubEventType.Items, ev.Type);
            Assert.Equal(PubSubEventType.Purge, ev.Type);
            Assert.Equal("princely_musings", ev.Purge.Node);
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

            Assert.True(ev2.Delete != null);

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
                              Configuration = new Matrix.Xmpp.PubSub.Event.Configuration {Node = "princely_musings"}
                          };
            ev2.ShouldBe(Resource.Get("Xmpp.PubSub.Event.event3.xml"));
        }

        [Fact]
        public void TestEvent6()
        {
            var ev = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.event6.xml")).Cast<Matrix.Xmpp.PubSub.Event.Event>();
            Assert.NotEqual(PubSubEventType.Items, ev.Type);
            Assert.Equal(PubSubEventType.Subscription, ev.Type);

            Assert.Equal("princely_musings", ev.Subscription.Node);
            Assert.True(ev.Subscription.Jid.Equals("horatio@denmark.lit"));
            Assert.Equal(Matrix.Xmpp.PubSub.SubscriptionState.Subscribed, ev.Subscription.SubscriptionState);
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
            DateTime dt;
            XmppXElement xmpp1 = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.event7.xml")).Cast<Matrix.Xmpp.PubSub.Event.Event>();
            
            var ev = xmpp1 as Matrix.Xmpp.PubSub.Event.Event;
            Assert.NotEqual(PubSubEventType.Items, ev.Type);
            Assert.Equal(PubSubEventType.Subscription, ev.Type);

            Assert.Equal("princely_musings", ev.Subscription.Node);
            Assert.True(ev.Subscription.Jid.Equals("francisco@denmark.lit"));
            Assert.Equal("ba49252aaa4f5d320c24d3766f0bdcade78c78d3", ev.Subscription.Id);
            Assert.Equal(Matrix.Xmpp.PubSub.SubscriptionState.Subscribed, ev.Subscription.SubscriptionState);

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
