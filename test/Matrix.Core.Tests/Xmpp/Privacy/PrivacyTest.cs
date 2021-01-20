using Matrix.Xml;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Privacy
{
    public class PrivacyTest
    {
        [Fact]
        public void ShoudBeOfTypePrivacy()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Privacy.privacy_query1.xml")).ShouldBeOfType<Matrix.Xmpp.Privacy.Privacy>();
        }

        [Fact]
        public void TestPrivacy()
        {
            var priv = XmppXElement.LoadXml(Resource.Get("Xmpp.Privacy.privacy_query1.xml")).Cast<Matrix.Xmpp.Privacy.Privacy>();
            Assert.Equal("public", priv.Default.Name);
            Assert.Equal("private", priv.Active.Name);
        }
    }
}
