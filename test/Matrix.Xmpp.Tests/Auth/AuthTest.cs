using Matrix.Xml;

using Xunit;

namespace Matrix.Xmpp.Tests.Auth
{
    [Collection("Factory collection")]
    public class AuthTest
    {
        const string XML = @"<query xmlns='jabber:iq:auth'><username>gnauck</username><digest/></query>";
        
        [Fact]
        public void Test1()
        {
            var el = XmppXElement.LoadXml(XML);
            Assert.True(el is Matrix.Xmpp.Auth.Auth);

            var auth = el as Matrix.Xmpp.Auth.Auth;
            Assert.Equal(auth.Username, "gnauck");
            Assert.True(auth.HasTag("digest"));
            Assert.True(auth.Digest != null);
            //auth.Digest = null;
            //Assert.True(auth.Digest == null);

        }
    }
}
