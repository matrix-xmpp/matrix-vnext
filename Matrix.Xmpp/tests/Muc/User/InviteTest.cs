using Microsoft.VisualStudio.TestTools.UnitTesting;

using Matrix.Xml;
using Matrix.Xmpp.Muc.User;
using Matrix.Xmpp.Tests;

namespace Test.Xmpp.Muc.User
{
    [TestClass]
    public class InviteTest
    {
        const string XML1 = @"<invite xmlns='http://jabber.org/protocol/muc#user' to='hecate@shakespeare.lit'>
                  <reason>The reason.</reason>
                </invite>";

        [TestMethod]
        public void Test1()
        {
            var xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.AreEqual(true, xmpp1 is Invite);

            var invite = xmpp1 as Invite;

            if (invite != null)
            {
                Assert.AreEqual(invite.To.Equals("hecate@shakespeare.lit"), true);
                Assert.AreEqual(invite.Reason, "The reason.");
            }
        }

        [TestMethod]
        public void Test2()
        {
            var invite = new Invite("hecate@shakespeare.lit", "The reason.");
            invite.ShouldBe(XML1);
        }
    }
}