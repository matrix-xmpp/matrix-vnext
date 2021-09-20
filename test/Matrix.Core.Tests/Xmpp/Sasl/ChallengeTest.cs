using System.Text;
using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Sasl
{
    public class ChallengeTest
    {
        [Fact]
        public void ShouldBeOfTypeChallenge()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.challenge1.xml")).ShouldBeOfType<Challenge>();
        }

        [Fact]
        public void TestChallenge()
        {
            var resp = XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.challenge1.xml")).Cast<Challenge>();

            byte[] bval = resp.Bytes;
            string sval = Encoding.ASCII.GetString(bval);
            Assert.Equal("dummy value", sval);
        }
    }
}
