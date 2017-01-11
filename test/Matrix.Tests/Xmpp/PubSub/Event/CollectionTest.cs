using Matrix.Xml;
using Matrix.Xmpp.PubSub.Event;
using NUnit.Framework;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub.Event
{
    
    public class CollectionTest
    {
        private const string XML1 = @"<collection node='some-collection' xmlns='http://jabber.org/protocol/pubsub#event'>
                                        <disassociate node='new-node-id'/>
                                </collection>";

        private const string XML2 = @"<collection node='some-collection' xmlns='http://jabber.org/protocol/pubsub#event'>
                                        <associate node='new-node-id'/>
                                </collection>";

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Collection);

            var col = xmpp1 as Collection;
            if (col != null)
            {
                Assert.Equal(col.Node, "some-collection");
                Assert.Equal(col.FirstElement is Disassociate, true);
            }

            XmppXElement xmpp2 = XmppXElement.LoadXml(XML2);
            Assert.Equal(true, xmpp2 is Collection);

            var col2 = xmpp2 as Collection;
            if (col2 != null)
            {
                Assert.Equal(col2.Node, "some-collection");
                Assert.Equal(col2.FirstElement is Associate, true);
            }
        }

        [Fact]
        public void Test2()
        {
            var col = new Collection {Node = "some-collection"};
            col.Add(new Disassociate {Node = "new-node-id"});

            col.ShouldBe(XML1);

            var col2 = new Collection
                           {
                               Node = "some-collection",
                               Disassociate = new Disassociate {Node = "new-node-id"}
                           };
            
            col2.ShouldBe(XML1);

            var col3 = new Collection
            {
                Node = "some-collection",
                Associate = new Associate { Node = "new-node-id" }
            };
            
            col3.ShouldBe(XML2);

            Assert.Equal(col3.Associate != null, true);
            col3.Disassociate = new Disassociate {Node = "123"};
            Assert.Equal(col3.Associate == null, true);
            Assert.Equal(col3.Disassociate != null, true);
        }
    }
}