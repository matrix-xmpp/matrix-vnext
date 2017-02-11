using Matrix.Xml;
using Matrix.Xmpp.Privacy;
using Shouldly;
using Xunit;
using Item = Matrix.Xmpp.Privacy.Item;

namespace Matrix.Tests.Xmpp.Privacy
{
    
    public class ItemTest
    {
        [Fact]
        public void ShoudBeOfTypeItem()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Privacy.item1.xml")).ShouldBeOfType<Item>();
        }

        [Fact]
        public void TestItem()
        {
            var item = XmppXElement.LoadXml(Resource.Get("Xmpp.Privacy.item1.xml")).Cast<Item>();
            Assert.Equal(item.Val,  "none");
            Assert.Equal(item.Action, Matrix.Xmpp.Privacy.Action.Deny);
            Assert.Equal(item.Order, 5);
            Assert.Equal(item.Type, Matrix.Xmpp.Privacy.Type.Subscription);
        }

        [Fact]
        public void TestBuildItem()
        {
            var item = new Item { Val = "none", Type = Type.Subscription, Action = Action.Deny, Order = 5};
            item.ShouldBe(Resource.Get("Xmpp.Privacy.item1.xml"));

            item.Type = Type.None;
            item.ShouldBe(Resource.Get("Xmpp.Privacy.item2.xml"));

            item = new Item { Stanza = Stanza.Message | Stanza.IncomingPresence };
            item.ShouldBe(Resource.Get("Xmpp.Privacy.item3.xml"));

            item = new Item { Stanza = Stanza.All };
            item.ShouldBe(Resource.Get("Xmpp.Privacy.item4.xml"));
        }
    }
}
