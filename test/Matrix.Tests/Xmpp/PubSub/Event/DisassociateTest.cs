using Matrix.Xml;
using Matrix.Xmpp.PubSub.Event;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub.Event
{
    
    public class DisassociateTest
    {
        [Fact]
        public void ShoudBeOfTypeDisassociate()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.disassociate1.xml")).ShouldBeOfType<Disassociate>();
        }

        [Fact]
        public void TestDisassociate()
        {
            var dis = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.disassociate1.xml")).Cast<Disassociate>();
            Assert.Equal(dis.Node, "new-node-id");
        }

        [Fact]
        public void TestBuildDisassociate()
        {
            new Disassociate {Node = "new-node-id"}.ShouldBe(Resource.Get("Xmpp.PubSub.Event.disassociate1.xml"));
        }
    }
}