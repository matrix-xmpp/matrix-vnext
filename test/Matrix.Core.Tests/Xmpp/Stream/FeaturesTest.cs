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

            Assert.True(feats.SupportsCompression);
            Assert.True(feats.SupportsAuth);
            Assert.True(feats.Compression.Supports(Methods.Zlib));
        }

        [Fact]
        public void TestCompression()
        {
            var comp = XmppXElement.LoadXml(Resource.Get("Xmpp.Stream.compression2.xml")).Cast<Matrix.Xmpp.Stream.Features.Compression>();

            Assert.False(comp.Supports(Methods.Zlib));

            var method = comp.Element<Method>();
            Assert.True(method.CompressionMethod == Methods.Unknown);
        }
    }
}
