using Matrix.Xml;
using Matrix.Xmpp.Shim;
using Xunit;

namespace Matrix.Tests.Xmpp.Shim
{
    
    public class ShimTests
    {
        private const string xml1 = @"<headers xmlns='http://jabber.org/protocol/shim'>
      <header name='Created'>2004-09-21T03:01:52Z</header>
    </headers>";

        private const string xml2 = @"<headers xmlns='http://jabber.org/protocol/shim'>
      <header name='Created'/>
    </headers>";
        
        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(xml1);
            Assert.Equal(xmpp1 is Headers, true);
            var headers = xmpp1 as Headers;

            Assert.Equal(headers.HasHeaders, true);

            Assert.Equal(headers.HasHeader("Created"), true);
            Assert.Equal(headers.HasHeader("created"), false);
            Assert.Equal(headers.HasHeader(HeaderNames.Created), true);
            Assert.Equal(headers[HeaderNames.Created].Value == "2004-09-21T03:01:52Z", true);
        }

        [Fact]
        public void Test2()
        {
            var headers = new Headers();
            headers.AddHeader(HeaderNames.Created, "2004-09-21T03:01:52Z");

            headers.ShouldBe(xml1);
        }

        [Fact]
        public void Test3()
        {
            var headers = new Headers();
            headers.AddHeader(HeaderNames.Created, "2004-09-21T03:01:52Z");
            headers.ShouldBe(xml1);

            var headers2 = new Headers();
            headers2[HeaderNames.Created].Value = "2004-09-21T03:01:52Z";
            headers2.ShouldBe(xml1);
        }

        [Fact]
        public void Test4()
        {
            var headers = new Headers();
            headers.SetHeader(HeaderNames.Created);
            headers.ShouldBe(xml2);
        }
    }
}
