using Xunit;
using Matrix.Xml;
using Matrix.Xmpp.Vcard;
using Shouldly;

namespace Matrix.Tests.Xmpp.Vcard
{
    public class TelephoneTest
    {
        [Fact]
        public void XmlShouldBeTypeOfTelephone()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.telephone1.xml")).ShouldBeOfType<Telephone>();
            XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.telephone2.xml")).ShouldBeOfType<Telephone>();
            XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.telephone3.xml")).ShouldBeOfType<Telephone>();
            XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.telephone4.xml")).ShouldBeOfType<Telephone>();
            XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.telephone5.xml")).ShouldBeOfType<Telephone>();
            XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.telephone6.xml")).ShouldBeOfType<Telephone>();
        }

        [Fact]
        public void TestTelephone()
        {
            Telephone tel1 = XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.telephone1.xml")).Cast<Telephone>();
            Assert.Equal("303-308-3282", tel1.Number);
            Assert.True(tel1.IsVoice);
            Assert.True(tel1.IsWork);
            Assert.False(tel1.IsHome);
            
            Telephone tel2 = XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.telephone2.xml")).Cast<Telephone>();
            Assert.Equal("12345", tel2.Number);
            Assert.True(tel2.IsWork);
            Assert.True(tel2.IsFax);

            Telephone tel3 = XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.telephone3.xml")).Cast<Telephone>();
            Assert.Equal("67890", tel3.Number);
            Assert.True(tel2.IsWork);
            Assert.False(tel2.IsMessagingService);
        }

        [Fact]
        public void TestBuildTelephone()
        {           
            Telephone tel1 = new Telephone("12345", true);     
            tel1.ShouldBe(Resource.Get("Xmpp.Vcard.telephone4.xml"));

            Telephone tel2 = new Telephone("12345", true) {IsHome = true};
            tel2.ShouldBe(Resource.Get("Xmpp.Vcard.telephone5.xml"));

            Telephone tel3 = new Telephone("12345", true) {IsWork = true};
            tel3.ShouldBe(Resource.Get("Xmpp.Vcard.telephone6.xml"));
        }
    }
}
