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
            Assert.True(invite.To.Equals("hecate@shakespeare.lit"));
            Assert.Equal("The reason.", invite.Reason);
        }

        [Fact]
        public void TestBuildInvite()
        {
            var invite = new Invite("hecate@shakespeare.lit", "The reason.");
            invite.ShouldBe(Resource.Get("Xmpp.Muc.User.invite1.xml"));
        }
    }
}
