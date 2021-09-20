using Matrix.Xml;
using Matrix.Xmpp.PubSub.Event;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub.Event
{
    public class AssociateTest
    {
        [Fact]
        public void ShoudBeOfTypeAssociate()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.associate1.xml")).ShouldBeOfType<Associate>();
        }

        [Fact]
        public void TestAssociate()
        {
            var ass = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.associate1.xml")).Cast<Associate>();
            Assert.Equal("new-node-id", ass.Node);
        }

        [Fact]
        public void TestBuildAssociate()
        {
            new Associate { Node = "new-node-id" }.
            ShouldBe(Resource.Get("Xmpp.PubSub.Event.associate1.xml"));
        }
    }
}
