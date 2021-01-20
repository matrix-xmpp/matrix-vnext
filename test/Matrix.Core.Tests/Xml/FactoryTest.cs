using Matrix.Xml;
using Matrix.Xmpp.Client;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xml
{
    public class FactoryTest
    {
        [Fact]
        public void ShouldReturnXName()
        {
            Factory.GetXName<Message>().ShouldBe("{jabber:client}message");
        }
    }
}
