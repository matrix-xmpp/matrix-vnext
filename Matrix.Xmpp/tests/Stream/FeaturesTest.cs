using Matrix.Xml;
using Matrix.Xmpp.Compression;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Xmpp.Stream
{
    [TestClass]
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

        [TestMethod]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.AreEqual(true, xmpp1 is Matrix.Xmpp.Stream.StreamFeatures);

            var feats = xmpp1 as Matrix.Xmpp.Stream.StreamFeatures;
            
            Assert.AreEqual(feats.SupportsCompression, true);
            Assert.AreEqual(feats.SupportsAuth, true);
            Assert.AreEqual(feats.Compression.Supports(Matrix.Xmpp.Compression.Methods.Zlib), true);
        }

        [TestMethod]
        public void Test3()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML3);
            Assert.AreEqual(true, xmpp1 is Matrix.Xmpp.Stream.Features.Compression);

            var comp = xmpp1 as Matrix.Xmpp.Stream.Features.Compression;
            
            Assert.AreEqual(comp.Supports(Matrix.Xmpp.Compression.Methods.Zlib), false);

            var method = comp.Element<Method>();
            Assert.AreEqual(method.CompressionMethod == Methods.Unknown, true);
        }
    }
}