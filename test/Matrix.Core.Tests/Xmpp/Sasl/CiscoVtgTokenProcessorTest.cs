namespace Matrix.Tests.Xmpp.Sasl
{
    using Matrix.Xml;
    using Xunit;
    using Moq;
    using Matrix.Xmpp.Sasl;
    using System.Threading.Tasks;
    using System.Threading;
    using Matrix.Sasl.CiscoVtgToken;

    public class CiscoVtgTokenProcessorTest
    {
        [Fact]
        public async Task Test_Authentication_Message()
        {
            var expectedXml = "<auth mechanism='CISCO-VTG-TOKEN' xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>dXNlcmlkPXVzZXJAc2VydmVyLmNvbQB0b2tlbj1mb28=</auth>";

            var webexTokeSasl = new CiscoVtgTokenProcessor("foo");

            var mockXmppClient = new Mock<IXmppClient>();
            mockXmppClient.SetupGet(client => client.Username).Returns("user");
            mockXmppClient.SetupGet(client => client.XmppDomain).Returns("server.com");

            mockXmppClient
                .Setup(s => s.SendAsync<Success, Failure>(It.IsAny<XmppXElement>(), It.IsAny<CancellationToken>()))
                .Callback<XmppXElement, CancellationToken>((el, ct) =>
                {
                    el.ShouldBe(expectedXml);
                })
                .Returns(Task.FromResult<XmppXElement>(new Success()));

            var res = await webexTokeSasl.AuthenticateClientAsync(mockXmppClient.Object, CancellationToken.None);
        }
    }
}
