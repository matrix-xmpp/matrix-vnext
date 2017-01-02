using Microsoft.VisualStudio.TestTools.UnitTesting;


using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Tests;
using Matrix.Xmpp.Muc.Admin;

namespace Test.Xmpp.Muc.Admin
{
    [TestClass]
    public class AdminIqTest
    {
        private const string XML1 = @"<iq xmlns='jabber:client' id='1'>
            <query xmlns='http://jabber.org/protocol/muc#admin'>
                <item nick='pistol' role='none'>
                    <reason>my reason!</reason>
                </item>
            </query>
        </iq>";

        [TestMethod]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.AreEqual(true, xmpp1 is Iq);

            var iq = xmpp1 as Iq;
            if (iq != null)
                Assert.AreEqual(true, iq.Query is AdminQuery);
        }

        [TestMethod]
        public void Test2()
        {
            var aIq = new AdminIq();
            aIq.AdminQuery.AddItem(new Item(Matrix.Xmpp.Muc.Role.None, "pistol", "my reason!"));
            aIq.Id = "1";
            aIq.ShouldBe(XML1);
        }
    }
}