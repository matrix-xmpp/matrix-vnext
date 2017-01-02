using Matrix.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matrix.Xmpp.Tests.Base
{
    [TestClass]
    public class XmppXElementWithJidAttributeTest
    {
        [TestMethod]
        public void TestJid()
        {
            var xml = "<x xmlns='http://jabber.org/protocol/muc#user'><item affiliation='none' role='none' /></x>";
            var xml2 = "<x xmlns='http://jabber.org/protocol/muc#user'><item affiliation='none' role='none' jid='server'/></x>";
            
            var mucUser = XmppXElement.LoadXml(xml) as Matrix.Xmpp.Muc.User.X;

            var jid = mucUser.Item.Jid;
            Assert.AreEqual(jid, null);


            var mucUser2 = XmppXElement.LoadXml(xml2) as Matrix.Xmpp.Muc.User.X;
            var jid2 = mucUser2.Item.Jid;
            Assert.AreEqual(jid2 != null, true);
            
        }
    }
}