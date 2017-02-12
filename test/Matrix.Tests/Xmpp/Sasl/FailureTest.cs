using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Sasl
{
    public class FailureTest
    {
        [Fact]
        public void ShouldBeOfTypeChallenge()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.failure1.xml")).ShouldBeOfType<Failure>();
            XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.failure2.xml")).ShouldBeOfType<Failure>();
        }

        [Fact]
        public void TestFailure1()
        {
            var fail = XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.failure1.xml")).Cast<Failure>();
            Assert.Equal(fail.Condition == FailureCondition.InvalidAuthzId, true);
        }

        [Fact]
        public void TestFailure2()
        {
            var fail = XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.failure2.xml")).Cast<Failure>();
            Assert.Equal(fail.Condition == FailureCondition.UnknownCondition, true);
        }

        [Fact]
        public void TestBuildFailure()
        {
            new Failure
            {
                Condition = FailureCondition.InvalidAuthzId
            }.ShouldBe(Resource.Get("Xmpp.Sasl.failure1.xml"));
        }
    }
}
