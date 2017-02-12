using Matrix.Xml;
using Matrix.Xmpp.Compression;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Stream
{
    public class FeaturesTest
    {
        [Fact]
        public void TestShouldbeOfTypeStreamFeatures()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Stream.stream_features1.xml")).ShouldBeOfType<Matrix.Xmpp.Stream.StreamFeatures>();
        }

        [Fact]
        public void TestStreamFeatures()
        {
            var feats = XmppXElement.LoadXml(Resource.Get("Xmpp.Stream.stream_features1.xml")).Cast<Matrix.Xmpp.Stream.StreamFeatures>();

            Assert.Equal(feats.SupportsCompression, true);
            Assert.Equal(feats.SupportsAuth, true);
            Assert.Equal(feats.Compression.Supports(Methods.Zlib), true);
        }

        [Fact]
        public void TestCompression()
        {
            var comp = XmppXElement.LoadXml(Resource.Get("Xmpp.Stream.compression2.xml")).Cast<Matrix.Xmpp.Stream.Features.Compression>();

            Assert.Equal(comp.Supports(Methods.Zlib), false);

            var method = comp.Element<Method>();
            Assert.Equal(method.CompressionMethod == Methods.Unknown, true);
        }
    }
}