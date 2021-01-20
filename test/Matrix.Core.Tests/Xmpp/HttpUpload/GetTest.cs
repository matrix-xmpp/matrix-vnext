using Matrix.Xml;
using Matrix.Xmpp.HttpUpload;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.HttpUpload
{

    public class GetTest
    {
        [Fact]
        public void ElementShouldBeOfTypeGet()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.HttpUpload.get.xml")).ShouldBeOfType<Get>();
        }
           
        [Fact]
        public void TestGetProperties()
        {
            var get = XmppXElement.LoadXml(Resource.Get("Xmpp.HttpUpload.get.xml")).Cast<Get>();
            Assert.Equal("https://download.montague.tld/4a771ac1-f0b2-4a4a-9700-f2a26fa2bb67/tr%C3%A8s%20cool.jpg", get.Url);
        }

        [Fact]
        public void TestBuildGet()
        {
            var get = new Get { Url = "https://download.montague.tld/4a771ac1-f0b2-4a4a-9700-f2a26fa2bb67/tr%C3%A8s%20cool.jpg" };
            get.ShouldBe(Resource.Get("Xmpp.HttpUpload.get.xml"));
        }
    }
}
