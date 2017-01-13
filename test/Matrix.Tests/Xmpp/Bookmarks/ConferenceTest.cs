using Matrix.Xml;
using Matrix.Xmpp.Bookmarks;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Bookmarks
{
    public class ConferenceTest
    {
        [Fact]
        public void XmlShouldBeOfTypeConference()
        {
             XmppXElement.LoadXml(Resource.Get("Xmpp.Bookmarks.conference1.xml"))
                .ShouldBeOfType<Conference>();
        }

        [Fact]
        public void TestConferenceName()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Bookmarks.conference1.xml"))
                .Cast<Conference>()
                .Name.ShouldBe("Council of Oberon");
        }

        [Fact]
        public void TestConferenceAutoJoin()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Bookmarks.conference1.xml"))
                .Cast<Conference>()
                .AutoJoin.ShouldBeTrue();
        }

        [Fact]
        public void TestConferenceJid()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Bookmarks.conference1.xml"))
                .Cast<Conference>()
                .Jid.ToString().ShouldBe("council@conference.underhill.org");
        }

        [Fact]
        public void TestConferenceNickame()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Bookmarks.conference1.xml"))
                .Cast<Conference>()
                .Nickname
                .ShouldBe("Puck");
        }

        [Fact]
        public void TestConferencePassword()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Bookmarks.conference1.xml"))
                .Cast<Conference>()
                .Password
                .ShouldBe("secret");
        }

        [Fact]
        public void BuildConference()
        {
            var expectedXml = Resource.Get("Xmpp.Bookmarks.conference1.xml");
            new Conference
                {
                    Name = "Council of Oberon",
                    AutoJoin = true,
                    Jid = "council@conference.underhill.org",
                    Nickname = "Puck",
                    Password = "secret"
                }
                .ShouldBe(expectedXml);
        }
    }
}
