using Matrix.Xmpp.Jingle.Transports;
using Xunit;

namespace Matrix.Tests.Xmpp.Jingle.Transports
{
    public class TransportTest
    {
        [Fact]
        public void TestBuildTransport()
        {
            var tp = new TransportIbb { BlockSize = 4096, Sid = "ch3d9s71"};
            tp.ShouldBe(Resource.Get("Xmpp.Jingle.Transports.transport1.xml"));
        }
    }
}
