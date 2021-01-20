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
            Assert.Equal("none",  item.Val);
            Assert.Equal(Matrix.Xmpp.Privacy.Action.Deny, item.Action);
            Assert.Equal(5, item.Order);
            Assert.Equal(Matrix.Xmpp.Privacy.Type.Subscription, item.Type);
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
