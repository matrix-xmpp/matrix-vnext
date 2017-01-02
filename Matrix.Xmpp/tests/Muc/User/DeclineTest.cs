using Microsoft.VisualStudio.TestTools.UnitTesting;
using Matrix.Core;
using Matrix.Xml;
using Matrix.Xmpp.Muc.User;

namespace Matrix.Xmpp.Tests.Muc.User
{
    [TestClass]
    public class DeclineTest
    {
        const string XML1 = @"<decline xmlns='http://jabber.org/protocol/muc#user' to='crone1@shakespeare.lit'>
                  <reason>Sorry, I'm too busy right now.</reason>
                </decline>";

        [TestMethod]
        public void Test1()
        {
            var xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.AreEqual(true, xmpp1 is Decline);

            var d = xmpp1 as Decline;
            if (d != null)
            {
                Assert.AreEqual(d.Reason, "Sorry, I'm too busy right now.");
                Assert.AreEqual(d.To.Equals("crone1@shakespeare.lit"), true);
            }
        }

        [TestMethod]
        public void Test2()
        {
            var dec = new Decline(new Jid("crone1@shakespeare.lit"), "Sorry, I'm too busy right now.");
            dec.ShouldBe(XML1);
        }
    }
}