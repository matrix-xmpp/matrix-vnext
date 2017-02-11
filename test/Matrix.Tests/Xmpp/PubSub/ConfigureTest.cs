using Matrix.Xml;
using Matrix.Xmpp.PubSub;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub
{
    
    public class ConfigureTest
    {
        [Fact]
        public void ShoudBeOfTypeConfigure()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.configure3.xml")).ShouldBeOfType<Configure>();
        }

        [Fact]
        public void TestConfigure()
        {
            var conf = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.configure3.xml")).Cast<Configure>();
            Assert.Equal(conf.Node, "princely_musings");
        }

        [Fact]
        public void TestBuildConfigure()
        {
            var conf = new Configure { Node = "princely_musings" };
            conf.ShouldBe(Resource.Get("Xmpp.PubSub.Owner.configure3.xml"));
        }
    }
}
