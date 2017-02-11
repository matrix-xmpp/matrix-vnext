using Xunit;
using Matrix.Xml;
using Matrix.Xmpp.Vcard;
using Shouldly;

namespace Matrix.Tests.Xmpp.Vcard
{
    public class AddressTest
    {
        [Fact]
        public void XmlShouldBeTypeOfEmail()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.address1.xml")).ShouldBeOfType<Address>();
        }

        [Fact]
        public void TestAddress()
        {
            Address address1 = XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.address1.xml")).Cast<Address>();
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
