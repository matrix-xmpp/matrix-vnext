using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using Xunit;

namespace Matrix.Tests.Xmpp.Sasl
{
    
    public class FailureTest
    {
        private string XML1 = @"<failure xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>
  <invalid-authzid/>
</failure>";

        private string XML2 = @"<failure xmlns='urn:ietf:params:xml:ns:xmpp-sasl'/>";

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);

            var fail = xmpp1 as Failure;

            Assert.Equal(fail.Condition == FailureCondition.InvalidAuthzId, true);
        }

        [Fact]
        public void Test2()
        {
            XmppXElement xmpp2 = XmppXElement.LoadXml(XML2);

            var fail = xmpp2 as Failure;

            Assert.Equal(fail.Condition == FailureCondition.UnknownCondition, true);
        }

        [Fact]
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
