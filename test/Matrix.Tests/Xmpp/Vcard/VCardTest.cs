using Matrix.Xml;
using Matrix.Xmpp.Client;
using Xunit;

namespace Matrix.Tests.Xmpp.Vcard
{
    
    public class VCardTest
    {
        [Fact]
        public void TestVcardIq()
        {
            var iq = XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.vcard_iq1.xml")).Cast<Iq>();
            var vcard = iq.Element<Matrix.Xmpp.Vcard.Vcard>();
            Assert.Equal(vcard != null, true);
        }
    }
}
