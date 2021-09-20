using Matrix.Xml;
using Matrix.Xmpp.HttpUpload;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.HttpUpload
{

    public class HeaderTest
    {
        [Fact]
        public void ElementShouldBeOfTypeHeader()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.HttpUpload.header-cookie.xml")).ShouldBeOfType<Header>();
        }
           
        [Fact]
        public void TestCookieHeaderProperties()
        {
            var header = XmppXElement.LoadXml(Resource.Get("Xmpp.HttpUpload.header-cookie.xml")).Cast<Header>();
            Assert.Equal(HeaderNames.Cookie, header.HeaderName);
            Assert.Equal("foo=bar; user=romeo", header.Value);
        }

        [Fact]
        public void TestAuthHeaderProperties()
        {
            var header = XmppXElement.LoadXml(Resource.Get("Xmpp.HttpUpload.header-authorization.xml")).Cast<Header>();
            Assert.Equal(HeaderNames.Authorization, header.HeaderName);
            Assert.Equal("Basic Base64String==", header.Value);
        }

        [Fact]
        public void TestBuildCookieHeader()
        {
            var header = new Header(HeaderNames.Cookie, "foo=bar; user=romeo");
            header.ShouldBe(Resource.Get("Xmpp.HttpUpload.header-cookie.xml"));
        }

        [Fact]
        public void TestBuildAuthHeader()
        {
            var header = new Header(HeaderNames.Authorization, "Basic Base64String==");
            header.ShouldBe(Resource.Get("Xmpp.HttpUpload.header-authorization.xml"));
        }
    }
}
