using Matrix.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Matrix.Xmpp.Tests.Stream
{
    [TestClass]
    public class ErrorTest
    {
        string XML1 = @"<stream:error xmlns:stream='http://etherx.jabber.org/streams'>
  <resource-constraint xmlns='urn:ietf:params:xml:ns:xmpp-streams' />
</stream:error>";

        string XML2 = @"<stream:error xmlns:stream='http://etherx.jabber.org/streams'>
  <invalid-xml xmlns='urn:ietf:params:xml:ns:xmpp-streams' />
</stream:error>";

        [TestMethod]
        public void Test1()
        {
            var xmpp1 = new Matrix.Xmpp.Stream.Error(Matrix.Xmpp.Stream.ErrorCondition.ResourceConstraint);
            xmpp1.ShouldBe(XML1);

            var xmpp2 = new Matrix.Xmpp.Stream.Error(Matrix.Xmpp.Stream.ErrorCondition.InvalidXml);
            xmpp2.ShouldBe(XML2);
        }

        [TestMethod]
        public void Test2()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.AreEqual(true, xmpp1 is Matrix.Xmpp.Stream.Error);
            var error = xmpp1 as Matrix.Xmpp.Stream.Error;
            
            Assert.AreEqual(error.Condition == Matrix.Xmpp.Stream.ErrorCondition.ResourceConstraint, true);
        }

        [TestMethod]
        public void Test3()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML2);
            Assert.AreEqual(true, xmpp1 is Matrix.Xmpp.Stream.Error);
            var error = xmpp1 as Matrix.Xmpp.Stream.Error;

            Assert.AreEqual(error.Condition == Matrix.Xmpp.Stream.ErrorCondition.InvalidXml, true);
        }
    }
}