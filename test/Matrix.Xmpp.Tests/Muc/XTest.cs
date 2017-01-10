using Matrix.Xmpp.Client;
using Xunit;

using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.Muc;
using X=Matrix.Xmpp.Muc.X;

namespace Matrix.Xmpp.Tests.Muc
{
    [Collection("Factory collection")]
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

        [Fact]
        public void Test1()
        {
            var xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is X);

            var x = xmpp1 as X;
            if (x != null)
                Assert.Equal(x.Password, "secret");
        }

        [Fact]
        public void Test2()
        {
            var xmpp2 = XmppXElement.LoadXml(XML2);
            Assert.Equal(true, xmpp2 is X);

            var x = xmpp2 as X;
            if (x != null)
                Assert.Equal(x.History.MaxStanzas, 20);
        }

        [Fact]
        public void Test3()
        {
            var x = new X(new History(20));
            x.ShouldBe(XML2);
        }

        [Fact]
        public void Test4()
        {
            var x = new X("secret");
            x.ShouldBe(XML1);
        }

        [Fact]
        public void TestMucInvite()
        {
            var xmpp1 = XmppXElement.LoadXml(XML3);
            Assert.Equal(true, xmpp1 is Message);

            var msg = xmpp1 as Message;
            var xMuc = msg.Element<Matrix.Xmpp.Muc.User.X>();
            Assert.Equal(xMuc.Password, "cauldronburn");

            var invite = xMuc.GetInvites().First();
            Assert.Equal(invite.Reason, "FOO");
        }
    }
}