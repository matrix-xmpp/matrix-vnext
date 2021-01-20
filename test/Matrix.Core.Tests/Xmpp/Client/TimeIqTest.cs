using Matrix.Xmpp.Client;
using Xunit;

namespace Matrix.Tests.Xmpp.Client
{
    public class TimeIqTest
    {
        [Fact]
        public void BuildTimeIq()
        {
            string expectedXml = Resource.Get("Xmpp.Client.timeiq.xml");
            new TimeIq
            {
                Type = Matrix.Xmpp.IqType.Get,
                To = "juliet@capulet.com/balcony",
                Id = "time_1"
            }
            .ShouldBe(expectedXml);
        }
    }
}
