using Matrix.Xml;
using Matrix.Xmpp.Disco;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Disco
{
    public class ItemTest
    {
        [Fact]
        public void ElementSouldBeOfTypeItem()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Disco.item1.xml")).ShouldBeOfType<Item>();
        }

        [Fact]
        public void TestItemProperties()
        {
            var item = XmppXElement.LoadXml(Resource.Get("Xmpp.Disco.item1.xml")).Cast<Item>();
            item.Node.ShouldBe("node1");
            item.Jid.Bare.ShouldBe("user1@server.com");
            item.Name.ShouldBe("name1");
        }
    }
}
