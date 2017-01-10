using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.XHtmlIM;
using Xunit;


namespace Matrix.Xmpp.Tests.Client
{
    [Collection("Factory collection")]
    public class XHTML
    {
        private string XML1 = @"<message xmlns='jabber:client'>
            <body>Wow, I&apos;m green with envy!</body>
            <html xmlns='http://jabber.org/protocol/xhtml-im'>
            <body xmlns='http://www.w3.org/1999/xhtml'>
              <p style='font-size:large'>
                <em>Wow</em>, I&apos;m <span style='color:green'>green</span>
                with <strong>envy</strong>!
              </p>
            </body>
            </html>
            </message>";
        private string XML2 = @"<message xmlns='jabber:client'>            
            <html xmlns='http://jabber.org/protocol/xhtml-im'>
            <body xmlns='http://www.w3.org/1999/xhtml'><p>Hello World</p></body>
            </html>
            </message>";

        private string PTAG =
            @"<p style='font-size:large'>
                <em>Wow</em>, I&apos;m <span style='color:green'>green</span>
                with <strong>envy</strong>!
              </p>";

        [Fact]
        public void Test()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML2);

            Assert.Equal(true, xmpp1 is Message);

            var msg = xmpp1 as Message;
            var body = msg.Element<Html>().Element<Body>();
            Assert.Equal(body.InnerXHtml, "<p>Hello World</p>");

            var msg2 = new Message
                           {
                               XHtml = new Html
                                           {
                                               Body = new Body {InnerXHtml = "<p>Hello World</p>"}
                                           }
                           };

            msg2.ShouldBe(XML2);
        }
    }
}
