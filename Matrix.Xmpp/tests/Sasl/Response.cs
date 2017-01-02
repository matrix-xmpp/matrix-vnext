using System.Text;
using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matrix.Xmpp.Tests.Sasl
{
    [TestClass]
    public class ResponseTest
    {
        const string XML1 = "<response xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>ZHVtbXkgdmFsdWU=</response>";

        [TestMethod]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            
            var resp = xmpp1 as Response;

            byte[] bval = resp.Bytes;
            string sval = Encoding.ASCII.GetString(bval);
            Assert.AreEqual("dummy value", sval);
        }
    }
}