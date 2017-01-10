using Matrix.Xml;
using Matrix.Xmpp.Compression;
using Xunit;

namespace Matrix.Tests.Xmpp.Stream
{
    
    public class FeaturesTest
    {
        private const string XML1 =
            @"<stream:features xmlns:stream='http://etherx.jabber.org/streams'>
                  <starttls xmlns='urn:ietf:params:xml:ns:xmpp-tls' />
                  <mechanisms xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>
                    <mechanism>CRAM-MD5</mechanism>
                    <mechanism>LOGIN</mechanism>
                    <mechanism>PLAIN</mechanism>
                    <mechanism>DIGEST-MD5</mechanism>
                    <mechanism>SCRAM-SHA-1</mechanism>
                  </mechanisms> 
                  <compression xmlns='http://jabber.org/features/compress'>
                    <method>zlib</method>
                  </compression>
                  <ver xmlns='urn:xmpp:features:rosterver'>
                    <optional />
                  </ver>
                  <auth xmlns='http://jabber.org/features/iq-auth' />
                </stream:features>";

        private const string XML2 = @"<compression xmlns='http://jabber.org/features/compress'>
                    <method>zlib</method>
                  </compression>";

        private const string XML3 = @"<compression xmlns='http://jabber.org/features/compress'>
                    <method>FooBar</method>
                  </compression>";

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.Stream.StreamFeatures);

            var feats = xmpp1 as Matrix.Xmpp.Stream.StreamFeatures;
            
            Assert.Equal(feats.SupportsCompression, true);
            Assert.Equal(feats.SupportsAuth, true);
            Assert.Equal(feats.Compression.Supports(Matrix.Xmpp.Compression.Methods.Zlib), true);
        }

        [Fact]
        public void Test3()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML3);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.Stream.Features.Compression);

            var comp = xmpp1 as Matrix.Xmpp.Stream.Features.Compression;
            
            Assert.Equal(comp.Supports(Matrix.Xmpp.Compression.Methods.Zlib), false);

            var method = comp.Element<Method>();
            Assert.Equal(method.CompressionMethod == Methods.Unknown, true);
        }
    }
}