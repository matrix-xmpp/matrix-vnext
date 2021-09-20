using Xunit;
using Matrix.Xml;
using Matrix.Xmpp.Muc.User;
using Shouldly;

namespace Matrix.Tests.Xmpp.Muc.User
{
    
    public class DeclineTest
    {
        [Fact]
        public void ShoudBeOfTypeDecline()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Muc.User.decline1.xml")).ShouldBeOfType<Decline>();
        }

        [Fact]
        public void TestDecline()
        {
            var d = XmppXElement.LoadXml(Resource.Get("Xmpp.Muc.User.decline1.xml")).Cast<Decline>();
            Assert.Equal("Sorry, I'm too busy right now.", d.Reason);
            Assert.True(d.To.Equals("crone1@shakespeare.lit"));
        }

        [Fact]
        public void TestBuildDecline()
        {
            var dec = new Decline(new Jid("crone1@shakespeare.lit"), "Sorry, I'm too busy right now.");
            dec.ShouldBe(Resource.Get("Xmpp.Muc.User.decline1.xml"));
        }
    }
}
