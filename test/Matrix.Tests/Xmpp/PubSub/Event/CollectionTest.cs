using Matrix.Xml;
using Matrix.Xmpp.PubSub.Event;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub.Event
{
    
    public class CollectionTest
    {
        [Fact]
        public void ShoudBeOfTypeCollection()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.collection1.xml")).ShouldBeOfType<Collection>();
        }

        [Fact]
        public void TestColelciton()
        {
            var col = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.collection1.xml")).Cast<Collection>();
            Assert.Equal(col.Node, "some-collection");
            Assert.Equal(col.FirstElement is Disassociate, true);
        
            var col2 = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.collection2.xml")).Cast<Collection>();
            Assert.Equal(col2.Node, "some-collection");
            Assert.Equal(col2.FirstElement is Associate, true);
        }

        [Fact]
        public void TestBuildCollection()
        {
            var col = new Collection {Node = "some-collection"};
            col.Add(new Disassociate {Node = "new-node-id"});

            col.ShouldBe(Resource.Get("Xmpp.PubSub.Event.collection1.xml"));

            var col2 = new Collection
            {
                Node = "some-collection",
                Disassociate = new Disassociate {Node = "new-node-id"}
            };
            col2.ShouldBe(Resource.Get("Xmpp.PubSub.Event.collection1.xml"));

            var col3 = new Collection
            {
                Node = "some-collection",
                Associate = new Associate {Node = "new-node-id"}
            };

            col3.ShouldBe(Resource.Get("Xmpp.PubSub.Event.collection2.xml"));

            Assert.Equal(col3.Associate != null, true);
            col3.Disassociate = new Disassociate {Node = "123"};
            Assert.Equal(col3.Associate == null, true);
            Assert.Equal(col3.Disassociate != null, true);
        }
    }
}