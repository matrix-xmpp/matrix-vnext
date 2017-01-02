using Microsoft.VisualStudio.TestTools.UnitTesting;
using Matrix.Xml;
using Matrix.Xmpp.Vcard;

namespace Matrix.Xmpp.Tests.Vcard
{
    [TestClass]
    public class AddressTest
    {
        string xml1 = @"<ADR xmlns='vcard-temp'>
			<WORK/>
			<EXTADD>Suite 600</EXTADD>
			<STREET>1899 Wynkoop Street</STREET>
			<LOCALITY>Denver</LOCALITY>
			<REGION>CO</REGION>
			<PCODE>80202</PCODE>
			<CTRY>USA</CTRY>
		</ADR>";

        [TestMethod]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(xml1);
            Assert.AreEqual(true, xmpp1 is Address);

            Address address1 = xmpp1 as Address;
            Assert.AreEqual(address1.IsWork, true);
            Assert.AreEqual(address1.IsHome, false);
            Assert.AreEqual(address1.ExtraAddress, "Suite 600");
            Assert.AreEqual(address1.Street, "1899 Wynkoop Street");
            Assert.AreEqual(address1.Locality, "Denver");
            Assert.AreEqual(address1.Region, "CO");
            Assert.AreEqual(address1.PostCode, "80202");
            Assert.AreEqual(address1.Country, "USA");

        }
    }
}





