using Matrix.Xml;
using Matrix.Xmpp.Stream.Features;
using Xunit;


namespace Matrix.Xmpp.Tests.Stream.Features
{
    [Collection("Factory collection")]
    public class FeatureTest
    {
        private const string XML1 = @"<ver xmlns='urn:xmpp:features:rosterver'>
            <optional/>
          </ver>";

        private const string XML2 = @"<ver xmlns='urn:xmpp:features:rosterver'>
            <required/>
          </ver>";
        
        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is RosterVersioning);

            var rv = xmpp1 as RosterVersioning;
            Assert.Equal(rv.Optional, true);
            Assert.Equal(rv.Required, false);
        }

        [Fact]
        public void Test2()
        {
            var rv = new RosterVersioning {Required = true};
            rv.ShouldBe(XML2);
        }
    }
}