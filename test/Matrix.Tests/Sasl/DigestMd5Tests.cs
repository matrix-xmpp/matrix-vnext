using Matrix.Sasl.Digest;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Sasl
{
    public class DigestMd5Tests
    {
        [Fact]
        public void Should_Use_XmppDomain_As_Default_Realm()
        {
            // Arrange
            string serverStep1 = "nonce=\"3828753646\",qop=\"auth\",charset=utf-8,algorithm=md5-sess";
            var step1 = new Step1(serverStep1);
            var xmppClient = new XmppClient() { Username = "alex", XmppDomain = "server.com", Password = "secret"};
            var step2 = new Step2(step1, xmppClient);

            // Assert
            step2.GetMessage().ShouldContain("realm=\"server.com\"");            
        }
    }
}
