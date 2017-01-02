using Matrix.Xml;
using Matrix.Xmpp.Muc.User;
using Matrix.Xmpp.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Xmpp.Muc.User
{
    [TestClass]
    public class ActorTest
    {
        const string XML1 = @"<actor xmlns='http://jabber.org/protocol/muc#user' jid='bard@shakespeare.lit'/>";
        
        [TestMethod]
        public void Test1()
        {
            var xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.AreEqual(true, xmpp1 is Actor);

            var a = xmpp1 as Actor;
            if (a != null) Assert.AreEqual(a.Jid.Equals("bard@shakespeare.lit"), true);
        }

        [TestMethod]
        public void Test2()
        {
            var act = new Actor("bard@shakespeare.lit");
            act.ShouldBe(XML1);
        }       
    }
}