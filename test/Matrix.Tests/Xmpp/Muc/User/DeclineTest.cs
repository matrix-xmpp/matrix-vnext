using Xunit;
using Matrix.Xml;
using Matrix.Xmpp.Muc.User;
using Shouldly;

namespace Matrix.Tests.Xmpp.Muc.User
{
    
    public class DeclineTest
    {
        const string XML1 = @"<decline xmlns='http://jabber.org/protocol/muc#user' to='crone1@shakespeare.lit'>
                  <reason>Sorry, I'm too busy right now.</reason>
                </decline>";

        [Fact]
        public void Test1()
        {
            var xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Decline);

            var d = xmpp1 as Decline;
            if (d != null)
            {
                Assert.Equal(d.Reason, "Sorry, I'm too busy right now.");
                Assert.Equal(d.To.Equals("crone1@shakespeare.lit"), true);
            }
        }

        [Fact]
        public void Test2()
        {
            var dec = new Decline(new Jid("crone1@shakespeare.lit"), "Sorry, I'm too busy right now.");
            dec.ShouldBe(XML1);
        }
    }
}