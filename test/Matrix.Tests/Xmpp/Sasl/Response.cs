using System.Text;
using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using Xunit;

namespace Matrix.Tests.Xmpp.Sasl
{
    
    public class ResponseTest
    {
        const string XML1 = "<response xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>ZHVtbXkgdmFsdWU=</response>";

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            
            var resp = xmpp1 as Response;

            byte[] bval = resp.Bytes;
            string sval = Encoding.ASCII.GetString(bval);
            Assert.Equal("dummy value", sval);
        }
    }
}