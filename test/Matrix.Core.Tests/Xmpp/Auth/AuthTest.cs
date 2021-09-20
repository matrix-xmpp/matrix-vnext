using Matrix.Xml;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Auth
{    
    public class AuthTest
    {
        [Fact]
        public void XmlShoudbeOfTypeAuth()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Auth.query.xml"))
                .ShouldBeOfType<Matrix.Xmpp.Auth.Auth>();
        }

        [Fact]
        public void TestUsername()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Auth.query.xml"))
                .Cast<Matrix.Xmpp.Auth.Auth>()
                .Username.ShouldBe("gnauck");
        }
        
        [Fact]
        public void TestHastDigestTag()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Auth.query.xml"))
                .Cast<Matrix.Xmpp.Auth.Auth>()
                .HasTag("digest");

            XmppXElement.LoadXml(Resource.Get("Xmpp.Auth.query.xml"))
                .Cast<Matrix.Xmpp.Auth.Auth>()
                .Digest.ShouldNotBeNull();
        }
    }
}
