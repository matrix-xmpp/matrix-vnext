using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matrix.Xmpp.Tests.Sasl
{
    [TestClass]
    public class FailureTest
    {
        private string XML1 = @"<failure xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>
  <invalid-authzid/>
</failure>";

        private string XML2 = @"<failure xmlns='urn:ietf:params:xml:ns:xmpp-sasl'/>";

        [TestMethod]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);

            var fail = xmpp1 as Failure;

            Assert.AreEqual(fail.Condition == FailureCondition.InvalidAuthzId, true);
        }

        [TestMethod]
        public void Test2()
        {
            XmppXElement xmpp2 = XmppXElement.LoadXml(XML2);

            var fail = xmpp2 as Failure;

            Assert.AreEqual(fail.Condition == FailureCondition.UnknownCondition, true);
        }

        [TestMethod]
        public void Test3()
        {
            var fail = new Failure()
            {
                Condition = FailureCondition.InvalidAuthzId
            };
            
            fail.ShouldBe(XML1);
        }
    }
}
