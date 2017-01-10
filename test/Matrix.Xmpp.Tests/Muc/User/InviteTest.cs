using Xunit;

using Matrix.Xml;
using Matrix.Xmpp.Muc.User;

namespace Matrix.Xmpp.Tests.Muc.User
{
    [Collection("Factory collection")]
    public class InviteTest
    {
        const string XML1 = @"<invite xmlns='http://jabber.org/protocol/muc#user' to='hecate@shakespeare.lit'>
                  <reason>The reason.</reason>
                </invite>";

        [Fact]
        public void Test1()
        {
            var xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Invite);

            var invite = xmpp1 as Invite;

            if (invite != null)
            {
                Assert.Equal(invite.To.Equals("hecate@shakespeare.lit"), true);
                Assert.Equal(invite.Reason, "The reason.");
            }
        }

        [Fact]
        public void Test2()
        {
            var invite = new Invite("hecate@shakespeare.lit", "The reason.");
            invite.ShouldBe(XML1);
        }
    }
}