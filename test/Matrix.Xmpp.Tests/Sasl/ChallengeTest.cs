using System.Text;
using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using Xunit;

namespace Matrix.Xmpp.Tests.Sasl
{
    [Collection("Factory collection")]
    public class ChallengeTest
    {
        const string XML1 = "<challenge xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>ZHVtbXkgdmFsdWU=</challenge>";
        
        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            
            var resp = xmpp1 as Challenge;

            byte[] bval = resp.Bytes;
            string sval = Encoding.ASCII.GetString(bval);
            Assert.Equal("dummy value", sval);
        }
    }
}