using Matrix.Xml;
using Matrix.Xmpp.Stream.Features;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Stream.Features
{
    public class FeatureTest
    {
        [Fact]
        public void TestShouldbeOfTypeRosterVersioning()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Stream.Features.ver1.xml")).ShouldBeOfType<RosterVersioning>();
        }

        [Fact]
        public void TestRosterVersioning()
        {
            var rv = XmppXElement.LoadXml(Resource.Get("Xmpp.Stream.Features.ver1.xml")).Cast<RosterVersioning>();
            Assert.True(rv.Optional);
            Assert.False(rv.Required);
        }

        [Fact]
        public void TestBuildRosterVersioning()
        {
            new RosterVersioning {Required = true}
                .ShouldBe(Resource.Get("Xmpp.Stream.Features.ver2.xml"));
        }
    }
}
