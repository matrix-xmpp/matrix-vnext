using Matrix.Sasl.Digest;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Sasl
{
    using Moq;

    public class DigestMd5Tests
    {
        [Fact]
        public void Should_Use_XmppDomain_As_Default_Realm()
        {
            // Arrange
            string serverStep1 = "nonce=\"3828753646\",qop=\"auth\",charset=utf-8,algorithm=md5-sess";
            var step1 = new Step1(serverStep1);
            
            var mockXmppClient = new Mock<IXmppClient>();
            mockXmppClient.SetupGet(client => client.Username).Returns("alex");
            mockXmppClient.SetupGet(client => client.Password).Returns("secret");
            mockXmppClient.SetupGet(client => client.XmppDomain).Returns("server.com");

            var step2 = new Step2(step1, mockXmppClient.Object);

            // Assert
            step2.GetMessage().ShouldContain("realm=\"server.com\"");            
        }
    }
}
