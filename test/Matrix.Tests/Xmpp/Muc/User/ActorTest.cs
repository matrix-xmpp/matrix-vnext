using Matrix.Xml;
using Matrix.Xmpp.Muc.User;
using Xunit;

namespace Matrix.Tests.Xmpp.Muc.User
{
    
    public class ActorTest
    {
        const string XML1 = @"<actor xmlns='http://jabber.org/protocol/muc#user' jid='bard@shakespeare.lit'/>";
        
        [Fact]
        public void Test1()
        {
            var xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Actor);

            var a = xmpp1 as Actor;
            if (a != null) Assert.Equal(a.Jid.Equals("bard@shakespeare.lit"), true);
        }

        [Fact]
        public void Test2()
        {
            var act = new Actor("bard@shakespeare.lit");
            act.ShouldBe(XML1);
        }       
    }
}