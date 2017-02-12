using System.Text;
using Matrix.Xml;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Sasl
{
    
    public class AuthTest
    {
        [Fact]
        public void ShouldBeOfTypeAuth()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.auth1.xml")).ShouldBeOfType<Matrix.Xmpp.Sasl.Auth>();
        }

        [Fact]
        public void TestAuth()
        {
            var resp = XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.auth1.xml")).Cast<Matrix.Xmpp.Sasl.Auth>();

            byte[] bval = resp.Bytes;
            string sval = Encoding.ASCII.GetString(bval);
            Assert.Equal("dummy value", sval);
        }

        [Fact]
        public void TestBuildAuth()
        {
            new Matrix.Xmpp.Sasl.Auth { Bytes = Encoding.ASCII.GetBytes("dummy value") }
                .ShouldBe(Resource.Get("Xmpp.Sasl.auth1.xml"));
        }
    }
}
