using Xunit;

using Matrix.Xml;
using Shouldly;


namespace Matrix.Tests.Xmpp.Nickname
{
    public class Nick
    {
        [Fact]
        public void ShoudBeOfTypeX()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Nickname.nick1.xml")).ShouldBeOfType<Matrix.Xmpp.Nickname.Nick>();
        }

        [Fact]
        public void TestNick()
        {
            Matrix.Xmpp.Nickname.Nick nick1 = XmppXElement.LoadXml(Resource.Get("Xmpp.Nickname.nick1.xml")).Cast<Matrix.Xmpp.Nickname.Nick>();
            Assert.Equal(nick1 == "Ishmael", true);
        }

        [Fact]
        public void TestBuildNick()
        {            
            Matrix.Xmpp.Nickname.Nick nick1 = "Alex";
            Assert.Equal(nick1.Value, "Alex");
            Assert.Equal(nick1 == "Alex", true);
                       
            Matrix.Xmpp.Nickname.Nick nick2 = new Matrix.Xmpp.Nickname.Nick();
            nick2 = "Ishmael";
            Assert.Equal(nick2.Value, "Ishmael");
            Assert.Equal(nick2 == "Ishmael", true);

            Matrix.Xmpp.Nickname.Nick nick3 = new Matrix.Xmpp.Nickname.Nick("Alex");            
            Assert.Equal(nick3.Value, "Alex");
            Assert.Equal(nick3 == "Alex", true);
        }

        [Fact]
        public void TestNickInPresence()
        {
            Matrix.Xmpp.Client.Presence pres = XmppXElement.LoadXml(Resource.Get("Xmpp.Nickname.presence1.xml")).Cast<Matrix.Xmpp.Client.Presence>();
            Matrix.Xmpp.Nickname.Nick nick1 =  pres.Nick;
            Assert.Equal(nick1 == "Ishmael", true);
        }

        [Fact]
        public void TestBuildPresenceWithNick()
        {
            Matrix.Xmpp.Client.Presence pres = new Matrix.Xmpp.Client.Presence {Nick = "Alex"};

            Assert.Equal(pres.Nick.Value, "Alex");
            
            pres.Nick.Value = "Ishmael";
            Assert.Equal(pres.Nick.Value, "Ishmael");
        }
    }
}
