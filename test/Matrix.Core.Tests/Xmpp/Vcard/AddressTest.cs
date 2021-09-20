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
            Assert.True(address1.IsWork);
            Assert.False(address1.IsHome);
            Assert.Equal("Suite 600", address1.ExtraAddress);
            Assert.Equal("1899 Wynkoop Street", address1.Street);
            Assert.Equal("Denver", address1.Locality);
            Assert.Equal("CO", address1.Region);
            Assert.Equal("80202", address1.PostCode);
            Assert.Equal("USA", address1.Country);
        }
    }
}
