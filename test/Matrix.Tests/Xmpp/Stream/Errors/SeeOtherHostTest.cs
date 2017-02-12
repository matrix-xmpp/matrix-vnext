using Matrix.Xml;
using Matrix.Xmpp.Stream.Errors;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Stream.Errors
{
    public class SeeOtherHostTest
    {
        [Fact]
        public void TestShouldbeOfTypeHeaders()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Stream.Errors.see_other_host1.xml")).ShouldBeOfType<SeeOtherHost>();
        }

        [Fact]
        public void TestSeeOtherHost1()
        {
            var soh = XmppXElement.LoadXml(Resource.Get("Xmpp.Stream.Errors.see_other_host1.xml")).Cast<SeeOtherHost>();
            Assert.Equal(soh.Hostname, "foo.com");
            Assert.Equal(soh.Port, 5222);
        }

        [Fact]
        public void TestSeeOtherHost2()
        {
            var soh = XmppXElement.LoadXml(Resource.Get("Xmpp.Stream.Errors.see_other_host2.xml")).Cast<SeeOtherHost>(); ;
            Assert.Equal(soh.Hostname, "foo.com");
            Assert.Equal(soh.Port, 80);
        }

        [Fact]
        public void TestbuildSeeOtherHost()
        {
            new SeeOtherHost {Port = 80, Hostname = "foo.com" }
                .ShouldBe(Resource.Get("Xmpp.Stream.Errors.see_other_host2.xml"));
        }
    }
}