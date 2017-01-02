using Matrix.Xml;
using Matrix.Xmpp.Stream.Features;
using Matrix.Xmpp.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;


namespace Test.Xmpp.Stream.Features
{
    [TestClass]
    public class FeatureTest
    {
        private const string XML1 = @"<ver xmlns='urn:xmpp:features:rosterver'>
            <optional/>
          </ver>";

        private const string XML2 = @"<ver xmlns='urn:xmpp:features:rosterver'>
            <required/>
          </ver>";
        
        [TestMethod]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.AreEqual(true, xmpp1 is RosterVersioning);

            var rv = xmpp1 as RosterVersioning;
            Assert.AreEqual(rv.Optional, true);
            Assert.AreEqual(rv.Required, false);
        }

        [TestMethod]
        public void Test2()
        {
            var rv = new RosterVersioning {Required = true};
            rv.ShouldBe(XML2);
        }
    }
}