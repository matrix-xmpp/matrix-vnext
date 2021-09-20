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
            Assert.Equal("princely_musings", publish.Node);
            IEnumerable<Item> items = publish.GetItems();
            Assert.Equal(3, items.Count());
            Assert.Equal("ae890ac52d0df67ed7cfdf51b644e901", items.ToArray()[0].Id);
            Assert.Equal("abc", items.ToArray()[1].Id);
            Assert.Equal("def", items.ToArray()[2].Id);
        }
    }
}
