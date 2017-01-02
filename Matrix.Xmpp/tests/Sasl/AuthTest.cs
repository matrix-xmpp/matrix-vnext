using System.Text;
using Matrix.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Matrix.Xmpp.Tests.Sasl
{
    [TestClass]
    public class AuthTest
    {
        const string XML1 = "<auth xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>ZHVtbXkgdmFsdWU=</auth>";

        [TestMethod]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);


            var resp = xmpp1 as Matrix.Xmpp.Sasl.Auth;

            byte[] bval = resp.Bytes;
            string sval = Encoding.ASCII.GetString(bval);
            Assert.AreEqual("dummy value", sval);

            var auth2 = new Matrix.Xmpp.Sasl.Auth { Bytes = Encoding.ASCII.GetBytes("dummy value") };
            auth2.ShouldBe(XML1);
            
            var auth3 = new Matrix.Xmpp.Sasl.Auth { Bytes = null };
        }
    }
}
