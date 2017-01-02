using Microsoft.VisualStudio.TestTools.UnitTesting;
using Matrix.Xml;
using Matrix.Xmpp.Vcard;

namespace Matrix.Xmpp.Tests.Vcard
{
    [TestClass]
    public class EmailTest
    {
        string xml1 = @"<EMAIL xmlns='vcard-temp'><INTERNET/><USERID>info@ag-software.de</USERID></EMAIL>";
        string xml2 = @"<EMAIL xmlns='vcard-temp'><PREF/><INTERNET/><USERID>stpeter@jabber.org</USERID></EMAIL>";

        [TestMethod]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(xml1);
            Assert.AreEqual(true, xmpp1 is Email);

            Email email1 = xmpp1 as Email;
            Assert.AreEqual(email1.Address, "info@ag-software.de");
            Assert.AreEqual(email1.IsPreferred, false);

            XmppXElement xmpp2 = XmppXElement.LoadXml(xml2);
            Assert.AreEqual(true, xmpp2 is Email);

            Email email2 = xmpp2 as Email;
            Assert.AreEqual(email2.Address, "stpeter@jabber.org");
            Assert.AreEqual(email2.IsPreferred, true);


            Email email3 = new Email("info@ag-software.de");
            email1.ShouldBe(xml1);

            Email email4 = new Email("stpeter@jabber.org", true);
            email4.ShouldBe(xml2);
        }
    }
}





