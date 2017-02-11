using System.Collections.Generic;
using System.Linq;

using Matrix.Xml;
using Matrix.Xmpp.PubSub;

using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub
{
    public class PublishTest
    {
        [Fact]
        public void ShoudBeOfTypePublish()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.publish1.xml")).ShouldBeOfType<Publish>();
        }

        [Fact]
        public void TestPublish()
        {
            var publish = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.publish1.xml")).Cast<Publish>();
            Assert.Equal(publish.Node, "princely_musings");
            IEnumerable<Item> items = publish.GetItems();
            Assert.Equal(items.Count(), 3);
            Assert.Equal(items.ToArray()[0].Id, "ae890ac52d0df67ed7cfdf51b644e901");
            Assert.Equal(items.ToArray()[1].Id, "abc");
            Assert.Equal(items.ToArray()[2].Id, "def");
        }
    }
}