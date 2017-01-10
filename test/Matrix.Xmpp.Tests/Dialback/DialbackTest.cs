using Matrix.Xml;
using Matrix.Xmpp.Dialback;
using Xunit;

namespace Matrix.Xmpp.Tests.Dialback
{
    [Collection("Factory collection")]
    public class DialbackTest
    {
        private const string XML1 = @"<stream:stream
              xmlns:stream='http://etherx.jabber.org/streams'
              xmlns='jabber:server'
              xmlns:db='jabber:server:dialback'
              to='xmpp.example.com'
              from='example.org'
              id='D60000229F'>
            <db:verify
              to='example.org'
              from='xmpp.example.com'
              id='D60000229F'>37c69b1cf07a3f67c04a5ef5902fa5114f2c76fe4a2686482ba5b89323075643</db:verify>
            </stream:stream>";

        private const string XML2 = @"<verify
                xmlns='jabber:server:dialback'
                to='example.org'
                from='xmpp.example.com'
                type='valid'
                id='D60000229F'>37c69b1cf07a3f67c04a5ef5902fa5114f2c76fe4a2686482ba5b89323075643</verify>";

        private const string XML3 = @"<verify
                xmlns='jabber:server:dialback'
                to='example.org'
                from='xmpp.example.com'
                type='invalid'
                id='D60000229F'>37c69b1cf07a3f67c04a5ef5902fa5114f2c76fe4a2686482ba5b89323075643</verify>";

        private const string XML4 = @"<verify
                xmlns='jabber:server:dialback'
                to='example.org'
                from='xmpp.example.com'                
                id='D60000229F'>37c69b1cf07a3f67c04a5ef5902fa5114f2c76fe4a2686482ba5b89323075643</verify>";
        
        [Fact]
        public void TestDialBackKeyGeneration()
        {
            var key = Verify.GenerateDialbackKey(
                "s3cr3tf0rd14lb4ck",
                "xmpp.example.com",
                "example.org",
                "D60000229F");

            Assert.Equal(key, "37c69b1cf07a3f67c04a5ef5902fa5114f2c76fe4a2686482ba5b89323075643");
        }
        
        [Fact]
        public void TestLoadVerifyElement()
        {
            var xmpp1 = XmppXElement.LoadXml(XML1);

            Assert.Equal(xmpp1.FirstElement is Verify, true);
            var verify = xmpp1.FirstElement as Verify;
            Assert.Equal(verify.DialbackKey, "37c69b1cf07a3f67c04a5ef5902fa5114f2c76fe4a2686482ba5b89323075643");
            Assert.Equal(verify.Id, "D60000229F");
            Assert.Equal(verify.To.Equals("example.org"), true);
            Assert.Equal(verify.From.Equals("xmpp.example.com"), true);
        }

        [Fact]
        public void TypeTest()
        {
            var xmpp1 = XmppXElement.LoadXml(XML2);
            Assert.Equal(xmpp1 is Verify, true);
            var verify = xmpp1 as Verify;
            Assert.Equal(verify.Type == VerifyType.Valid, true);

            var xmpp2 = XmppXElement.LoadXml(XML3);
            Assert.Equal(xmpp2 is Verify, true);
            var verify2 = xmpp2 as Verify;
            Assert.Equal(verify2.Type == VerifyType.Invalid, true);

            var xmpp3 = XmppXElement.LoadXml(XML4);
            Assert.Equal(xmpp3 is Verify, true);
            var verify3 = xmpp3 as Verify;
            Assert.Equal(verify3.Type == VerifyType.None, true);
        }
        
        [Fact]
        public void TestBuildVerifyElement()
        {
            var stream = new Matrix.Xmpp.Server.Stream
            {
                To = "xmpp.example.com",
                From = "example.org",
                Id = "D60000229F"
            };
            stream.AddNameSpaceDeclaration("db", Namespaces.ServerDialback);

            var verify = new Verify
            {
                Id = "D60000229F",
                DialbackKey = "37c69b1cf07a3f67c04a5ef5902fa5114f2c76fe4a2686482ba5b89323075643",
                From = "xmpp.example.com",
                To = "example.org"
            };

            stream.Add(verify);
            stream.ShouldBe(XML1);
        }
      
        [Fact]
        public void TestBuildVerifyElement2()
        {
            var stream = new Matrix.Xmpp.Server.Stream
            {
                To = "xmpp.example.com",
                From = "example.org",
                Id = "D60000229F"
            };
            stream.AddDialbackNameSpaceDeclaration();

            var verify = new Verify
            {
                Id = "D60000229F",
                DialbackKey = "37c69b1cf07a3f67c04a5ef5902fa5114f2c76fe4a2686482ba5b89323075643",
                From = "xmpp.example.com",
                To = "example.org"
            };

            stream.Add(verify);
            stream.ShouldBe(XML1);
        }

        [Fact]
        public void TestBuildVerifyElement3()
        {
            var stream = new Matrix.Xmpp.Server.Stream(true)
            {
                To = "xmpp.example.com",
                From = "example.org",
                Id = "D60000229F"
            };

            var verify = new Verify
            {
                Id = "D60000229F",
                DialbackKey = "37c69b1cf07a3f67c04a5ef5902fa5114f2c76fe4a2686482ba5b89323075643",
                From = "xmpp.example.com",
                To = "example.org"
            };

            stream.Add(verify);
            stream.ShouldBe(XML1);
        }

        [Fact]
        public void TestBuildVerifyElement4()
        {
            var stream = new Matrix.Xmpp.Server.Stream(false)
            {
                To = "xmpp.example.com",
                From = "example.org",
                Id = "D60000229F"
            };
            stream.AddDialbackNameSpaceDeclaration();

            var verify = new Verify
            {
                Id = "D60000229F",
                DialbackKey = "37c69b1cf07a3f67c04a5ef5902fa5114f2c76fe4a2686482ba5b89323075643",
                From = "xmpp.example.com",
                To = "example.org"
            };

            stream.Add(verify);
            stream.ShouldBe(XML1);
        }
    }
}
