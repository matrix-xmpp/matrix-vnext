using Matrix.Xml;
using Matrix.Xmpp.Muc.User;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Muc.User
{
    public class ActorTest
    {
        [Fact]
        public void ShoudBeOfTypeActor()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Muc.User.actor1.xml")).ShouldBeOfType<Actor>();
        }

        [Fact]
        public void TestActor()
        {
            var a = XmppXElement.LoadXml(Resource.Get("Xmpp.Muc.User.actor1.xml")).Cast<Actor>();
            Assert.Equal(a.Jid.Equals("bard@shakespeare.lit"), true);
        }

        [Fact]
        public void TestBuildActor()
        {
            var act = new Actor("bard@shakespeare.lit");
            act.ShouldBe(Resource.Get("Xmpp.Muc.User.actor1.xml"));
        }       
    }
}