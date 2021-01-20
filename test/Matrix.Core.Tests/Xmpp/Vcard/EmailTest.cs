using Xunit;
using Matrix.Xml;
using Matrix.Xmpp.Vcard;
using Shouldly;

namespace Matrix.Tests.Xmpp.Vcard
{
    
    public class EmailTest
    {
        [Fact]
        public void XmlShouldBeTypeOfEmail()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.email1.xml")).ShouldBeOfType<Email>();
            XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.email2.xml")).ShouldBeOfType<Email>();
        }

        [Fact]
        public void TestEmail()
        {
            Email email1 = XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.email1.xml")).Cast<Email>();
            email1.Address.ShouldBe("info@ag-software.de");
            email1.IsPreferred.ShouldBeFalse();


            Email email2 = XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.email2.xml")).Cast<Email>();
            email2.Address.ShouldBe("stpeter@jabber.org");
            email2.IsPreferred.ShouldBeTrue();
           
            email1.ShouldBe(Resource.Get("Xmpp.Vcard.email1.xml"));

            Email email4 = new Email("stpeter@jabber.org", true);
            email4.ShouldBe(Resource.Get("Xmpp.Vcard.email2.xml"));
        }
    }
}
