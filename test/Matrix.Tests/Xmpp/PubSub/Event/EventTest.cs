using System;
using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.PubSub.Event;
using NUnit.Framework;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub.Event
{
    
    public class EventTest
    {
        private const string XML1 = @"<event xmlns='http://jabber.org/protocol/pubsub#event'>
    <items node='princely_musings'>
      <item id='ab'>
        [ ... ENTRY ... ]
      </item>
    <item id='cd'>
        [ ... ENTRY ... ]
      </item>
    </items>
</event>";

        private const string XML2 =
            @"<event xmlns='http://jabber.org/protocol/pubsub#event'>
    <items node='princely_musings'>
      <retract id='ae890ac52d0df67ed7cfdf51b644e901'/>
    </items>
  </event>";

        private const string XML3 =
            @"<event xmlns='http://jabber.org/protocol/pubsub#event'>
    <configuration node='princely_musings'/>
  </event>";

       

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.PubSub.Event.Event);

            var ev = xmpp1 as Matrix.Xmpp.PubSub.Event.Event;
            if (ev != null)
            {
                Assert.Equal(ev.Type, PubSubEventType.Items);
                Assert.NotEqual(ev.Type, PubSubEventType.Delete);

                var items = ev.Items;
                Assert.Equal(items.GetItems().Count(), 2);
                Assert.NotEqual(items.GetItems().Count(), 5);

                Assert.Equal(items.GetItems().ToArray()[0].Id, "ab");
                Assert.Equal(items.GetItems().ToArray()[1].Id, "cd");
            }

        }

        [Fact]
        public void Test2()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML2);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.PubSub.Event.Event);

            var ev = xmpp1 as Matrix.Xmpp.PubSub.Event.Event;
            if (ev != null)
            {
                Assert.Equal(ev.Type, PubSubEventType.Items);
                var items = ev.Items;
                Assert.Equal(items.GetRetracts().Count(), 1);
                Assert.NotEqual(items.GetRetracts().Count(), 2);
                Assert.Equal(items.GetRetracts().ToArray()[0].Id, "ae890ac52d0df67ed7cfdf51b644e901");
                Assert.NotEqual(items.GetRetracts().ToArray()[0].Id, "_ae890ac52d0df67ed7cfdf51b644e901");
            }

            var ev2 = new Matrix.Xmpp.PubSub.Event.Event
                          {
                              Type = PubSubEventType.Items,
                              Items = {Node = "princely_musings"}
                          };
            ev2.Items.AddRetract(new Retract { Id = "ae890ac52d0df67ed7cfdf51b644e901" });
            ev2.ShouldBe(XML2);
        }

        [Fact]
        public void Test21()
        {
            var ev2 = new Matrix.Xmpp.PubSub.Event.Event { Type = PubSubEventType.Delete };

            Assert.Equal(ev2.Delete != null, true);

            ev2.Type = PubSubEventType.Items;
            ev2.Items.Node = "princely_musings";
            ev2.Items.AddRetract(new Retract { Id = "ae890ac52d0df67ed7cfdf51b644e901" });
            
            ev2.ShouldBe(XML2);
        }

        [Fact]
        public void Test3()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML3);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.PubSub.Event.Event);

            var ev = xmpp1 as Matrix.Xmpp.PubSub.Event.Event;
            if (ev != null)
            {
                Assert.Equal(ev.Type, PubSubEventType.Configuration);
                Assert.NotEqual(ev.Type, PubSubEventType.Delete);
            }

            var ev2 = new Matrix.Xmpp.PubSub.Event.Event
                          {
                              Configuration = new Configuration {Node = "princely_musings"}
                          };
            ev2.ShouldBe(XML3);
        }

        private const string XML4 = @"<event xmlns='http://jabber.org/protocol/pubsub#event'>
            <delete node='princely_musings'/>
        </event>";

        [Fact]
        public void Test4()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML4);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.PubSub.Event.Event);

            var ev = xmpp1 as Matrix.Xmpp.PubSub.Event.Event;
            if (ev != null)
            {
                Assert.NotEqual(ev.Type, PubSubEventType.Items);
                Assert.Equal(ev.Type, PubSubEventType.Delete);

                Assert.Equal(ev.Delete.Node, "princely_musings");
            }

        }
        
        private const string XML5 = @"<event xmlns='http://jabber.org/protocol/pubsub#event'>
            <purge node='princely_musings'/>
        </event>";

        [Fact]
        public void Test5()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML5);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.PubSub.Event.Event);

            var ev = xmpp1 as Matrix.Xmpp.PubSub.Event.Event;
            if (ev != null)
            {
                Assert.NotEqual(ev.Type, PubSubEventType.Items);
                Assert.Equal(ev.Type, PubSubEventType.Purge);

                Assert.Equal(ev.Purge.Node, "princely_musings");
            }

            var ev2 = new Matrix.Xmpp.PubSub.Event.Event
                          {
                              Type = PubSubEventType.Purge,
                              Purge = {Node = "princely_musings"}
                          };

            ev2.ShouldBe(XML5);
        }

        private const string XML6 = @"<event xmlns='http://jabber.org/protocol/pubsub#event'>
                <subscription node='princely_musings' jid='horatio@denmark.lit' subscription='subscribed'/>
              </event>";
        [Fact]
        public void Test6()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML6);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.PubSub.Event.Event);

            var ev = xmpp1 as Matrix.Xmpp.PubSub.Event.Event;
            if (ev != null)
            {
                Assert.NotEqual(ev.Type, PubSubEventType.Items);
                Assert.Equal(ev.Type, PubSubEventType.Subscription);

                Assert.Equal(ev.Subscription.Node, "princely_musings");
                Assert.Equal(ev.Subscription.Jid.Equals("horatio@denmark.lit"), true);
                Assert.Equal(ev.Subscription.SubscriptionState, Matrix.Xmpp.PubSub.SubscriptionState.Subscribed);
            }

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
            
            ev2.ShouldBe(XML6);
        }

        private const string XML7 = @"<event xmlns='http://jabber.org/protocol/pubsub#event'>
                <subscription
                    expiry='2006-02-28T23:59:00.000Z'
                    jid='francisco@denmark.lit'
                    node='princely_musings'
                    subid='ba49252aaa4f5d320c24d3766f0bdcade78c78d3'
                    subscription='subscribed'/>
              </event>";

        [Fact]
        public void Test7()
        {
            DateTime dt = DateTime.MinValue;
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML7);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.PubSub.Event.Event);

            var ev = xmpp1 as Matrix.Xmpp.PubSub.Event.Event;
            if (ev != null)
            {
                Assert.NotEqual(ev.Type, PubSubEventType.Items);
                Assert.Equal(ev.Type, PubSubEventType.Subscription);

                Assert.Equal(ev.Subscription.Node, "princely_musings");
                Assert.Equal(ev.Subscription.Jid.Equals("francisco@denmark.lit"), true);
                Assert.Equal(ev.Subscription.Id, "ba49252aaa4f5d320c24d3766f0bdcade78c78d3");
                Assert.Equal(ev.Subscription.SubscriptionState, Matrix.Xmpp.PubSub.SubscriptionState.Subscribed);

                dt = ev.Subscription.Expiry;
                //Assert.AreEqual(dt.Year, 2006);
                //Assert.AreEqual(dt.Month, 3);
                //Assert.AreEqual(dt.Day, 1);
                //Assert.AreEqual(dt.Minute, 59);
                //Assert.AreEqual(dt.Hour, 0);
                //Assert.AreEqual(dt.Millisecond, 0);
            }

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
            
            //ev2.Subscription.SetAttribute("expiry", "2006-02-28T23:59Z");
            
            ev2.ShouldBe(XML7);
        }
    }
}