using Matrix.Xml;
using Matrix.Xmpp.Stream.Errors;
using Xunit;


namespace Matrix.Xmpp.Tests.Stream.Errors
{
    [Collection("Factory collection")]
    public class SeeOtherHostTest
    {
        private const string XML1 = @"<see-other-host xmlns='urn:ietf:params:xml:ns:xmpp-streams'>foo.com</see-other-host>";

        private const string XML2 = @"<see-other-host xmlns='urn:ietf:params:xml:ns:xmpp-streams'>foo.com:80</see-other-host>";

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is SeeOtherHost);

            var soh = xmpp1 as SeeOtherHost;
            Assert.Equal(soh.Hostname, "foo.com");
            Assert.Equal(soh.Port, 5222);
        }

        [Fact]
        public void Test2()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML2);
            Assert.Equal(true, xmpp1 is SeeOtherHost);

            var soh = xmpp1 as SeeOtherHost;
            Assert.Equal(soh.Hostname, "foo.com");
            Assert.Equal(soh.Port, 80);
        }

        [Fact]
        public void Test3()
        {
            var soh = new SeeOtherHost {Port = 80, Hostname = "foo.com" };
            soh.ShouldBe(XML2);
        }
    }
}