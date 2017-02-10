using Xunit;
using Matrix.Xml;
using Matrix.Xmpp.Muc.User;

using Shouldly;

namespace Matrix.Tests.Xmpp.Muc.User
{
    public class InviteTest
    {
        [Fact]
        public void ShoudBeOfTypeInvite()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Muc.User.invite1.xml")).ShouldBeOfType<Invite>();
        }

        [Fact]
        public void TestInvite()
        {
            var invite = XmppXElement.LoadXml(Resource.Get("Xmpp.Muc.User.invite1.xml")).Cast<Invite>();
            Assert.Equal(invite.To.Equals("hecate@shakespeare.lit"), true);
            Assert.Equal(invite.Reason, "The reason.");
        }

        [Fact]
        public void TestBuildInvite()
        {
            var invite = new Invite("hecate@shakespeare.lit", "The reason.");
            invite.ShouldBe(Resource.Get("Xmpp.Muc.User.invite1.xml"));
        }
    }
}