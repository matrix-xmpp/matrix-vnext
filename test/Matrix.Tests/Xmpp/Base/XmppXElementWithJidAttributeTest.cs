using Matrix.Xml;
using Xunit;

namespace Matrix.Tests.Xmpp.Base
{
    
    public class XmppXElementWithJidAttributeTest
    {
        [Fact]
        public void TestJid()
        {
            var xml = "<x xmlns='http://jabber.org/protocol/muc#user'><item affiliation='none' role='none' /></x>";
            var xml2 = "<x xmlns='http://jabber.org/protocol/muc#user'><item affiliation='none' role='none' jid='server'/></x>";
            
            var mucUser = XmppXElement.LoadXml(xml) as Matrix.Xmpp.Muc.User.X;

            var jid = mucUser.Item.Jid;
            Assert.Equal(jid, null);


            var mucUser2 = XmppXElement.LoadXml(xml2) as Matrix.Xmpp.Muc.User.X;
            var jid2 = mucUser2.Item.Jid;
            Assert.Equal(jid2 != null, true);
            
        }
    }
}