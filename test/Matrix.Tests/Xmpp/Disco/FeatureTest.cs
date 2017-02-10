using Matrix.Xml;
using Matrix.Xmpp.Disco;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Disco
{
    public class FeatureTest
    {
        [Fact]
        public void ElementShouldBeOfTypeFeature()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Disco.feature1.xml")).ShouldBeOfType<Feature>();
        }

        [Fact]
        public void TestFeatureVar()
        {
            var feat = XmppXElement.LoadXml(Resource.Get("Xmpp.Disco.feature1.xml")).Cast<Feature>();
            feat.Var.ShouldBe("http://jabber.org/protocol/disco#info");
        }

        [Fact]
        public void BuildFeature()
        {
            string expectedXml = Resource.Get("Xmpp.Disco.feature1.xml");
            Feature feat2 = new Feature("http://jabber.org/protocol/disco#info");
            feat2.ShouldBe(expectedXml);
        }
    }
}
