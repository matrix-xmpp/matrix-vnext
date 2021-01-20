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
            Assert.True(nick1 == "Ishmael");
        }

        [Fact]
        public void TestBuildNick()
        {
            Matrix.Xmpp.Nickname.Nick nick1 = "Alex";
            Assert.Equal("Alex", nick1.Value);
            Assert.True(nick1 == "Alex");

            Matrix.Xmpp.Nickname.Nick nick2 = "Ishmael";
            Assert.Equal("Ishmael", nick2.Value);
            Assert.True(nick2 == "Ishmael");

            Matrix.Xmpp.Nickname.Nick nick3 = new Matrix.Xmpp.Nickname.Nick("Alex");
            Assert.Equal("Alex", nick3.Value);
            Assert.True(nick3 == "Alex");
        }

        [Fact]
        public void TestNickInPresence()
        {
            Matrix.Xmpp.Client.Presence pres = XmppXElement.LoadXml(Resource.Get("Xmpp.Nickname.presence1.xml")).Cast<Matrix.Xmpp.Client.Presence>();
            Matrix.Xmpp.Nickname.Nick nick1 = pres.Nick;
            Assert.True(nick1 == "Ishmael");
        }

        [Fact]
        public void TestBuildPresenceWithNick()
        {
            Matrix.Xmpp.Client.Presence pres = new Matrix.Xmpp.Client.Presence { Nick = "Alex" };

            Assert.Equal("Alex", pres.Nick.Value);

            pres.Nick.Value = "Ishmael";
            Assert.Equal("Ishmael", pres.Nick.Value);
        }
    }
}
