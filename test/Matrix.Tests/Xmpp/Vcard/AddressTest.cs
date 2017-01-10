using Xunit;
using Matrix.Xml;
using Matrix.Xmpp.Vcard;

namespace Matrix.Tests.Xmpp.Vcard
{
    
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

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(xml1);
            Assert.Equal(true, xmpp1 is Address);

            Address address1 = xmpp1 as Address;
            Assert.Equal(address1.IsWork, true);
            Assert.Equal(address1.IsHome, false);
            Assert.Equal(address1.ExtraAddress, "Suite 600");
            Assert.Equal(address1.Street, "1899 Wynkoop Street");
            Assert.Equal(address1.Locality, "Denver");
            Assert.Equal(address1.Region, "CO");
            Assert.Equal(address1.PostCode, "80202");
            Assert.Equal(address1.Country, "USA");

        }
    }
}





