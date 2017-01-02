using Matrix.Xmpp.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;
using System.Xml.Linq;
using Matrix.Xml;
using Matrix.Xmpp.Muc;
using Matrix.Xmpp.Tests;
using X=Matrix.Xmpp.Muc.X;

namespace Test.Xmpp.Muc
{
    [TestClass]
    public class XTest
    {
        const string XML1 = @"<x xmlns='http://jabber.org/protocol/muc'><password>secret</password></x>";
        const string XML2 = @"<x xmlns='http://jabber.org/protocol/muc'><history maxstanzas='20'/></x>";
        const string XML3 = @"<message xmlns='jabber:client' from='coven@chat.shakespeare.lit' id='nzd143v8' to='hecate@shakespeare.lit'>
   <x xmlns='http://jabber.org/protocol/muc#user'>
      <invite from='crone1@shakespeare.lit/desktop'>
         <reason>FOO</reason>
      </invite>
      <password>cauldronburn</password>
   </x>
</message>";

        [TestMethod]
        public void Test1()
        {
            var xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.AreEqual(true, xmpp1 is X);

            var x = xmpp1 as X;
            if (x != null)
                Assert.AreEqual(x.Password, "secret");
        }

        [TestMethod]
        public void Test2()
        {
            var xmpp2 = XmppXElement.LoadXml(XML2);
            Assert.AreEqual(true, xmpp2 is X);

            var x = xmpp2 as X;
            if (x != null)
                Assert.AreEqual(x.History.MaxStanzas, 20);
        }

        [TestMethod]
        public void Test3()
        {
            var x = new X(new History(20));
            x.ShouldBe(XML2);
        }

        [TestMethod]
        public void Test4()
        {
            var x = new X("secret");
            x.ShouldBe(XML1);
        }

        [TestMethod]
        public void TestMucInvite()
        {
            var xmpp1 = XmppXElement.LoadXml(XML3);
            Assert.AreEqual(true, xmpp1 is Message);

            var msg = xmpp1 as Message;
            var xMuc = msg.Element<Matrix.Xmpp.Muc.User.X>();
            Assert.AreEqual(xMuc.Password, "cauldronburn");

            var invite = xMuc.GetInvites().First();
            Assert.AreEqual(invite.Reason, "FOO");
        }
    }
}