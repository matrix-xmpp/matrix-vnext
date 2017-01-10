using Xunit;
using Matrix.Xml;
using Matrix.Xmpp.Vcard;
using Shouldly;

namespace Matrix.Tests.Xmpp.Vcard
{
    
    public class EmailTest
    {
        string xml1 = @"<EMAIL xmlns='vcard-temp'><INTERNET/><USERID>info@ag-software.de</USERID></EMAIL>";
        string xml2 = @"<EMAIL xmlns='vcard-temp'><PREF/><INTERNET/><USERID>stpeter@jabber.org</USERID></EMAIL>";

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(xml1);
            xmpp1.ShouldBeOfType<Email>();
            
            Email email1 = xmpp1 as Email;
            email1.Address.ShouldBe("info@ag-software.de");
            email1.IsPreferred.ShouldBeFalse();

            XmppXElement xmpp2 = XmppXElement.LoadXml(xml2);
            xmpp2.ShouldBeOfType<Email>();

            Email email2 = xmpp2 as Email;
            email2.Address.ShouldBe("stpeter@jabber.org");
            email2.IsPreferred.ShouldBeTrue();
           
            email1.ShouldBe(xml1);

            Email email4 = new Email("stpeter@jabber.org", true);
            email4.ShouldBe(xml2);
        }
    }
}
