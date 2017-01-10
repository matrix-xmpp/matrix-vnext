using Matrix.Xml;
using Matrix.Xmpp.Compression;
using Xunit;

namespace Matrix.Tests.Xmpp.Compression
{
    
    public class CompressionTest
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

        private const string XML11 = @"<stream:features xmlns:stream='http://etherx.jabber.org/streams'>
  <compression xmlns='http://jabber.org/features/compress'>
    <method>zlib</method>
  </compression>
  <mechanisms xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>
    <mechanism>SCRAM-SHA-1</mechanism>
    <mechanism>ANONYMOUS</mechanism>
    <mechanism>DIGEST-MD5</mechanism>
    <mechanism>PLAIN</mechanism>
  </mechanisms>
  <c xmlns='http://jabber.org/protocol/caps' hash='sha-1' node='http://www.process-one.net/en/ejabberd/' ver='wwrSvLFOLzC92POh074kJuEqYxE=' />
  <register xmlns='http://jabber.org/features/iq-register' />
</stream:features>";

        private const string XML2 = @"<compression xmlns='http://jabber.org/features/compress'>
                    <method>zlib</method>
                  </compression>";

        private const string XML22 = @"<compression xmlns='http://jabber.org/features/compress'>
                    <method>Zlib</method>
                  </compression>";

        private const string XML3 = @"<compression xmlns='http://jabber.org/features/compress'>
                    <method>FooBar</method>
                  </compression>";

        private const string XML4 = @"<compress xmlns='http://jabber.org/protocol/compress'>
  <method>zlib</method>
</compress>";

        private const string XML41 = @"<compress xmlns='http://jabber.org/protocol/compress'>
  <method>Zlib</method>
</compress>";

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.Stream.StreamFeatures);

            var feats = xmpp1 as Matrix.Xmpp.Stream.StreamFeatures;

            Assert.Equal(feats.SupportsCompression, true);
            Assert.Equal(feats.SupportsAuth, true);
            Assert.Equal(feats.Compression.Supports(Methods.Zlib), true);
        }

        [Fact]
        public void Test11()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML11);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.Stream.StreamFeatures);

            var feats = xmpp1 as Matrix.Xmpp.Stream.StreamFeatures;

            Assert.Equal(feats.SupportsCompression, true);
            Assert.Equal(feats.Compression.Supports(Methods.Zlib), true);
        }

        [Fact]
        public void TestCompressionStreamFeature()
        {
            /*
             *   private const string XML2 = @"<compression xmlns='http://jabber.org/features/compress'>
                    <method>zlib</method>
                  </compression>";

        private const string XML22 = @"<compression xmlns='http://jabber.org/features/compress'>
                    <method>Zlib</method>
                  </compression>";
             */

            // read it
            var comp = new Matrix.Xmpp.Stream.Features.Compression();
            comp.AddMethod(Methods.Zlib);
            comp.ShouldBe(XML2);
            comp.ShouldNotBe(XML22);
           
            //build it
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML2);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.Stream.Features.Compression);
            var comp1 = xmpp1 as Matrix.Xmpp.Stream.Features.Compression;
            Assert.Equal(comp1.Supports(Methods.Zlib), true);
            Assert.Equal(comp1.Supports(Methods.Lzw), false);

            XmppXElement xmpp2 = XmppXElement.LoadXml(XML22);
            Assert.Equal(true, xmpp2 is Matrix.Xmpp.Stream.Features.Compression);
            var comp2 = xmpp2 as Matrix.Xmpp.Stream.Features.Compression;
            Assert.Equal(comp2.Supports(Methods.Zlib), false);
            Assert.Equal(comp2.Supports(Methods.Lzw), false);
        }

        [Fact]
        public void Test3()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML3);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.Compression.Compression);

            var comp = xmpp1 as Matrix.Xmpp.Compression.Compression;

            Assert.Equal(comp.Supports(Methods.Zlib), false);

            var method = comp.Element<Method>();
            Assert.Equal(method.CompressionMethod == Methods.Unknown, true);
        }


        [Fact]
        public void Test4()
        {
            var comp = new Matrix.Xmpp.Compression.Compress(Methods.Zlib);
            comp.ShouldBe(XML4);
            
             /*
             private const string XML4 = @"<compress xmlns='http://jabber.org/protocol/compress'>
            <method>zlib</method>
            </compress>";
              */
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML4);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.Compression.Compress);
            var comp1 = xmpp1 as Matrix.Xmpp.Compression.Compress;
            Assert.Equal(comp1.Method == Methods.Zlib, true);
            Assert.Equal(comp1.Method == Methods.Lzw, false);

            XmppXElement xmpp2 = XmppXElement.LoadXml(XML41);
            var comp2 = xmpp2 as Matrix.Xmpp.Compression.Compress;
            Assert.Equal(comp2.Method == Methods.Zlib, false);
            Assert.Equal(comp2.Method == Methods.Lzw, false);
            Assert.Equal(comp2.Method == Methods.Unknown, true);
        }
    }
}