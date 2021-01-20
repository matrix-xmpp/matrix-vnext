using Matrix.Xml;
using Matrix.Xmpp.Compression;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Compression
{
    public class CompressionTest
    {
        [Fact]
        public void TestStreamFeatures()
        {
            var feats = XmppXElement.LoadXml(Resource.Get("Xmpp.Compression.streamfeatures1.xml")).Cast<Matrix.Xmpp.Stream.StreamFeatures>();

            feats.SupportsCompression.ShouldBe(true);
            feats.SupportsAuth.ShouldBe(true);
            feats.Compression.Supports(Methods.Zlib).ShouldBe(true);

            var feats2 = XmppXElement.LoadXml(Resource.Get("Xmpp.Compression.streamfeatures2.xml")).Cast<Matrix.Xmpp.Stream.StreamFeatures>();

            feats2.SupportsCompression.ShouldBe(true);
            feats2.Compression.Supports(Methods.Zlib).ShouldBe(true);
        }
        
        [Fact]
        public void BuildCompression()
        {
            var expectedXml1 = Resource.Get("Xmpp.Compression.compression1.xml");
            var comp = new Matrix.Xmpp.Stream.Features.Compression();
            comp.AddMethod(Methods.Zlib);
            comp.ShouldBe(expectedXml1);

            var comp1 = XmppXElement.LoadXml(Resource.Get("Xmpp.Compression.compression1.xml")).Cast<Matrix.Xmpp.Stream.Features.Compression>();
            comp1.Supports(Methods.Zlib).ShouldBe(true);
            comp1.Supports(Methods.Lzw).ShouldBe(false);

            var comp2 = XmppXElement.LoadXml(Resource.Get("Xmpp.Compression.compression2.xml")).Cast<Matrix.Xmpp.Stream.Features.Compression>();
            comp2.Supports(Methods.Zlib).ShouldBe(false);
            comp2.Supports(Methods.Lzw).ShouldBe(false);
        }

        [Fact]
        public void TestCompression()
        {
            var comp = XmppXElement.LoadXml(Resource.Get("Xmpp.Compression.compression3.xml")).Cast< Matrix.Xmpp.Compression.Compression>();
            comp.Supports(Methods.Zlib).ShouldBe(false);

            var method = comp.Element<Method>();
            method.CompressionMethod.ShouldBe(Methods.Unknown);
        }

        [Fact]
        public void BuildCompress()
        {
            var expectedXml1 = Resource.Get("Xmpp.Compression.compress1.xml");
            
            var comp = new Compress(Methods.Zlib);
            comp.ShouldBe(expectedXml1);
        }

        [Fact]
        public void TestCompress()
        {
            var comp1 = XmppXElement.LoadXml(Resource.Get("Xmpp.Compression.compress1.xml")).Cast<Compress>();
            comp1.Method.ShouldBe(Methods.Zlib);
            comp1.Method.ShouldNotBe(Methods.Lzw);

            var comp2 = XmppXElement.LoadXml(Resource.Get("Xmpp.Compression.compress2.xml")).Cast<Compress>();
            comp2.Method.ShouldNotBe(Methods.Zlib);
            comp2.Method.ShouldNotBe(Methods.Lzw);
            comp2.Method.ShouldBe(Methods.Unknown);
        }
    }
}
