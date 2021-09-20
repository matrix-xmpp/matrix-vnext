using Matrix.Xml;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub.Owner
{
    public class ConfigureTest
    {
        [Fact]
        public void ShoudBeOfTypeDisassociate()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.configure1.xml")).ShouldBeOfType<Matrix.Xmpp.PubSub.Owner.Configure>();
        }

        [Fact]
        public void BuildConfigure()
        {
            new Matrix.Xmpp.PubSub.Owner.Configure().ShouldBe(Resource.Get("Xmpp.PubSub.Owner.configure1.xml"));
        }

        [Fact]
        public void TestConfigureContainsXData()
        {
            var conf = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.configure2.xml")).Cast<Matrix.Xmpp.PubSub.Owner.Configure>();
            Assert.True(conf.XData != null);
        }
    }
}
