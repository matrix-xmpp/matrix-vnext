using System.Collections.Generic;
using System.Linq;

using Matrix.Xml;
using Matrix.Xmpp.PubSub;

using NUnit.Framework;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub
{
    
    public class PublishTest
    {
        const string XML1 = @"<publish node='princely_musings' xmlns='http://jabber.org/protocol/pubsub'>
                  <item id='ae890ac52d0df67ed7cfdf51b644e901'/>
                  <item id='abc'/>
                  <item id='def'/>
                </publish>";
        
        [Fact]
        public void Test1()
        {
            var xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Publish);

            var publish = xmpp1 as Publish;
            if (publish != null)
            {
                Assert.Equal(publish.Node, "princely_musings");
                IEnumerable<Item> items = publish.GetItems();

                Assert.Equal(items.Count(), 3);
                Assert.Equal(items.ToArray()[0].Id, "ae890ac52d0df67ed7cfdf51b644e901");
                Assert.Equal(items.ToArray()[1].Id, "abc");
                Assert.Equal(items.ToArray()[2].Id, "def");
            }
        }
    }
}