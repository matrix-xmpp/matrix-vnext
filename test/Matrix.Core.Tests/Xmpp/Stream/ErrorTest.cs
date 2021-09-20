using Matrix.Xml;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Stream
{
    public class ErrorTest
    {
        [Fact]
        public void TestBuildstreamError()
        {
            new Matrix.Xmpp.Stream.Error(Matrix.Xmpp.Stream.ErrorCondition.ResourceConstraint)
                .ShouldBe(Resource.Get("Xmpp.Stream.stream_error1.xml"));

            new Matrix.Xmpp.Stream.Error(Matrix.Xmpp.Stream.ErrorCondition.InvalidXml)
                .ShouldBe(Resource.Get("Xmpp.Stream.stream_error2.xml"));
        }

        [Fact]
        public void TestShouldbeOfTypeError()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Stream.stream_error1.xml")).ShouldBeOfType<Matrix.Xmpp.Stream.Error>();
        }

        [Fact]
        public void TestStreamError1()
        {
            var error = XmppXElement.LoadXml(Resource.Get("Xmpp.Stream.stream_error1.xml")).Cast<Matrix.Xmpp.Stream.Error>();
            Assert.True(error.Condition == Matrix.Xmpp.Stream.ErrorCondition.ResourceConstraint);
        }

        [Fact]
        public void TestStreamError2()
        {
            var error = XmppXElement.LoadXml(Resource.Get("Xmpp.Stream.stream_error2.xml")).Cast<Matrix.Xmpp.Stream.Error>();
            Assert.True(error.Condition == Matrix.Xmpp.Stream.ErrorCondition.InvalidXml);
        }
    }
}
