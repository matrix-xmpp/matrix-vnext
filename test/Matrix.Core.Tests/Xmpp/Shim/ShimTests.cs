using Matrix.Xml;
using Matrix.Xmpp.Shim;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Shim
{
    public class ShimTests
    {
        [Fact]
        public void TestShouldbeOfTypeHeaders()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Shim.headers1.xml")).ShouldBeOfType<Headers>();
        }

        [Fact]
        public void TestHeaders()
        {
            var headers = XmppXElement.LoadXml(Resource.Get("Xmpp.Shim.headers1.xml")).Cast<Headers>();

            Assert.True(headers.HasHeaders);
            Assert.True(headers.HasHeader("Created"));
            Assert.False(headers.HasHeader("created"));
            Assert.True(headers.HasHeader(HeaderNames.Created));
            Assert.True(headers[HeaderNames.Created].Value == "2004-09-21T03:01:52Z");
        }

        [Fact]
        public void TestHeaders2()
        {
            var headers = new Headers();
            headers.AddHeader(HeaderNames.Created, "2004-09-21T03:01:52Z");
            headers.ShouldBe(Resource.Get("Xmpp.Shim.headers1.xml"));

            var headers2 = new Headers();
            headers2[HeaderNames.Created].Value = "2004-09-21T03:01:52Z";
            headers2.ShouldBe(Resource.Get("Xmpp.Shim.headers1.xml"));
        }

        [Fact]
        public void TestBuildHeaders()
        {
            var headers = new Headers();
            headers.AddHeader(HeaderNames.Created, "2004-09-21T03:01:52Z");

            headers.ShouldBe(Resource.Get("Xmpp.Shim.headers1.xml"));
        }

        [Fact]
        public void TestBuildHeaders2()
        {
            var headers = new Headers();
            headers.SetHeader(HeaderNames.Created);
            headers.ShouldBe(Resource.Get("Xmpp.Shim.headers2.xml"));
        }
    }
}
