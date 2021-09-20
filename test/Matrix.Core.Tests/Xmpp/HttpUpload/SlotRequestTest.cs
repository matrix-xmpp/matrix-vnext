using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.HttpUpload;
using Shouldly;
using System.Linq;
using Xunit;

namespace Matrix.Tests.Xmpp.HttpUpload
{
    public class SlotRequestTest
    {
        [Fact]
        public void ElementShouldBeOfTypeRequest()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.HttpUpload.slot-request.xml")).ShouldBeOfType<Request>();
        }

        [Fact]
        public void TestRequestProperties()
        {
            var request = XmppXElement.LoadXml(Resource.Get("Xmpp.HttpUpload.slot-request.xml")).Cast<Request>();
            Assert.Equal("très cool.jpg", request.Filename);
            Assert.Equal(23456, request.Size);
            Assert.Equal("image/jpeg", request.ContentType);
        }

        [Fact]
        public void TestBuildRequest()
        {
            var request = new Request { Size = 23456, Filename = "très cool.jpg", ContentType = "image/jpeg" };
            request.ShouldBe(Resource.Get("Xmpp.HttpUpload.slot-request.xml"));
        }
        
        [Fact]
        public void TestRequestIq()
        {
            var iq = XmppXElement.LoadXml(Resource.Get("Xmpp.HttpUpload.slot-request-iq.xml")).Cast<Iq>();

            var slot = iq.Element<Slot>();
            Assert.NotNull(slot);

            var put = slot.Element<Put>();
            Assert.NotNull(put);
            Assert.Equal("https://upload.montague.tld/4a771ac1-f0b2-4a4a-9700-f2a26fa2bb67/tr%C3%A8s%20cool.jpg", put.Url);

            Assert.True(put.HasHeaders);
            Assert.True(put.HasHeader(HeaderNames.Cookie));
            Assert.True(put.HasHeader(HeaderNames.Authorization));
            Assert.False(put.HasHeader(HeaderNames.Expires));
            Assert.Equal(2, put.GetHeaders().Count());
            Assert.Equal("Basic Base64String==", put.GetHeader(HeaderNames.Authorization).Value);
            Assert.Equal("foo=bar; user=romeo", put.GetHeader(HeaderNames.Cookie).Value);

            var get = slot.Element<Get>();
            Assert.NotNull(get);
            Assert.Equal("https://download.montague.tld/4a771ac1-f0b2-4a4a-9700-f2a26fa2bb67/tr%C3%A8s%20cool.jpg", get.Url);
        }

        [Fact]
        public void TestBuildRequestIq()
        {
            var put = new Put
            {
                Url = "https://upload.montague.tld/4a771ac1-f0b2-4a4a-9700-f2a26fa2bb67/tr%C3%A8s%20cool.jpg"
            };
            put.AddHeader(HeaderNames.Authorization, "Basic Base64String==");
            put.AddHeader(HeaderNames.Cookie, "foo=bar; user=romeo");

            var get = new Get
            {
                Url = "https://download.montague.tld/4a771ac1-f0b2-4a4a-9700-f2a26fa2bb67/tr%C3%A8s%20cool.jpg"
            };

            var slot = new Slot
            {
                Put = put,
                Get = get,
            };

            var iq = new Iq { Type = Matrix.Xmpp.IqType.Result, Id = "step_03", To = "romeo@montague.tld/garden", From = "upload.montague.tld" };
            iq.Add(slot);

            iq.ShouldBe(Resource.Get("Xmpp.HttpUpload.slot-request-iq.xml"));
        }
    }
}
