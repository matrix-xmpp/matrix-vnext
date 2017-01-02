using System;
using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.MessageArchiving;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matrix.Xmpp.Tests.Archiving
{
    [TestClass]
    public class ArchivingTests
    {
        private const string XML1
           = @"<chat with='juliet@capulet.com/chamber'
          start='1469-07-21T02:56:15Z'
          subject='She speaks!'
            xmlns='urn:xmpp:archive'>
      <from utc='1469-07-21T00:32:29Z'><body>Art thou not Romeo, and a Montague?</body></from>
      <to secs='11'><body>Neither, fair saint, if either thee dislike.</body></to>
      <from secs='7'><body>How cam'st thou hither, tell me, and wherefore?</body></from>
    </chat>";


        private const string XML2 = @"<pref xmlns='urn:xmpp:archive'>
    <auto save='true'/>
    <default expire='31536000' otr='concede' save='body'/>
    <item jid='romeo@montague.net' otr='require' save='false'/>
    <item expire='630720000' jid='benvolio@montague.net' otr='forbid' save='message'/>
    <session thread='ffd7076498744578d10edabfe7f4a866' save='body'/>
    <method type='auto' use='forbid'/>
    <method type='local' use='concede'/>
    <method type='manual' use='prefer'/>
  </pref>";

        
        private string XML3 = @"<list xmlns='urn:xmpp:archive'
        with='juliet@capulet.com'/>";

        private string XML4 = @"<iq type='get' id='foo' xmlns='jabber:client'><list xmlns='urn:xmpp:archive'
        with='juliet@capulet.com'/></iq>";

        private string XML5 = @"<iq type='get' id='page1' xmlns='jabber:client'>
<retrieve xmlns='urn:xmpp:archive' 
    with='juliet@capulet.com/chamber'
    start='1469-07-21T02:56:15.000Z'>
        <set xmlns='http://jabber.org/protocol/rsm'>
            <max>100</max>
        </set>
    </retrieve>
</iq>";

        [TestMethod]
        public void ChatTest1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.AreEqual(true, xmpp1 is Chat);

            var chat = xmpp1 as Chat;
            if (chat != null)
            {
                Assert.AreEqual(chat.Subject, "She speaks!");
                Assert.AreEqual(chat.With.ToString(), "juliet@capulet.com/chamber");
                Assert.AreEqual(chat.GetItems().Count(), 3);
                
                var to = chat.Element<To>();
                Assert.AreEqual(to.Body, "Neither, fair saint, if either thee dislike.");
                Assert.AreEqual(to.Seconds, 11);

                var from = chat.Element<From>();
                Assert.AreEqual(from.Body, "Art thou not Romeo, and a Montague?");
                Assert.AreEqual(from.TimeStamp.ToUniversalTime(), new DateTime(1469, 07, 21, 0, 32, 29,DateTimeKind.Utc));
            }
        }

        [TestMethod]
        public void PrefTest1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML2);
            Assert.AreEqual(true, xmpp1 is Preferences);

            var prefs = xmpp1 as Preferences;
            if (prefs != null)
            {
                Assert.AreEqual(prefs.Auto != null,true);
                Assert.AreEqual(prefs.Auto.Save, true);

                // <default expire='31536000' otr='concede' save='body'/>
                Assert.AreEqual(prefs.Default != null, true);
                Assert.AreEqual(prefs.Default.Expire, 31536000);
                Assert.AreEqual(prefs.Default.Otr == OtrType.Concede, true);
                Assert.AreEqual(prefs.Default.Save == SaveType.Body, true);

                // <session thread='ffd7076498744578d10edabfe7f4a866' save='body'/>
                Assert.AreEqual(prefs.Session != null, true);
                Assert.AreEqual(prefs.Session.Thread, "ffd7076498744578d10edabfe7f4a866");
                Assert.AreEqual(prefs.Default.Save == SaveType.Body, true);

                // first: <method type='auto' use='forbid'/> 
                Assert.AreEqual(prefs.GetMethods().Count(), 3);
                Assert.AreEqual(prefs.GetMethods().First().Type == MethodType.Auto, true);
                Assert.AreEqual(prefs.GetMethods().First().Use == UseType.Forbid, true);
            }
        }

        [TestMethod]
        public void BuildListQuery()
        {
            var list = new Matrix.Xmpp.MessageArchiving.List()
            {
                With = "juliet@capulet.com"
            };
            
            list.ShouldBe(XML3);
           
            var iq = new Matrix.Xmpp.Client.Iq
            {
                Type = Matrix.Xmpp.IqType.Get,
                Id = "foo",
                Query = list
            };

            iq.ShouldBe(XML4);
        }


        [TestMethod]
        public void BuildRetrieveQuery()
        {

//        private string XML5 = @"<iq type='get' id='page1'>
//<retrieve xmlns='urn:xmpp:archive' 
//    with='juliet@capulet.com/chamber'
//    start='1469-07-21T02:56:15.000Z'>
//        <set xmlns='http://jabber.org/protocol/rsm'>
//            <max>100</max>
//        </set>
//    </retrieve>
//</iq>";
            var iq = new Matrix.Xmpp.Client.Iq
            {
                Type = Matrix.Xmpp.IqType.Get,
                Id = "page1",
                Query = new Retrieve
                {
                    With = "juliet@capulet.com/chamber",
                    Start = new DateTime(1469, 07, 21, 2, 56, 15, DateTimeKind.Utc),
                    ResultSet = new Matrix.Xmpp.ResultSetManagement.Set()
                    {
                        Maximum = 100
                    }
                }
            };

            iq.ShouldBe(XML5);
        }
    }
}