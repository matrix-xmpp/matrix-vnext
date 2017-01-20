using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.XHtmlIM;
using Xunit;
using Shouldly;

namespace Matrix.Tests.Xmpp.Client
{
    public class XHTML
    {
        [Fact]
        public void TestXhtmlMessage()
        {
            var msg = XmppXElement.LoadXml(Resource.Get("Xmpp.Client.message2.xml"));
            var body = msg.Element<Html>().Element<Body>();
            body.InnerXHtml.Trim().ShouldBe("<p>Hello World</p>");
        }

        [Fact]
        public void BuildXhtmlMessage()
        {
            string expectedXml = Resource.Get("Xmpp.Client.message2.xml");
            new Message
                {
                    XHtml = new Html
                    {
                        Body = new Body {InnerXHtml = "<p>Hello World</p>"}
                    }
                }
                .ShouldBe(expectedXml);
        }
    }
}
