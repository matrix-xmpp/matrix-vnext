using Xunit;
using Matrix;
using Matrix.Xml;
using Matrix.Xmpp.Bookmarks;
using Shouldly;

namespace Matrix.Tests.Xmpp.Bookmarks
{
    public class ConferenceTest
    {
        private const string XML1
           = @"<conference xmlns='storage:bookmarks'
                    name='Council of Oberon' 
                    autojoin='true'
                    jid='council@conference.underhill.org'>
        <nick>Puck</nick>
        <password>secret</password>
      </conference>";

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Conference);

            var conf = xmpp1 as Conference;
            if (conf != null)
            {
                Assert.Equal(conf.Name, "Council of Oberon");
                Assert.Equal(conf.AutoJoin, true);
                Assert.Equal(conf.Jid.Equals("council@conference.underhill.org"), true);
                Assert.Equal(conf.Nickname, "Puck");
                Assert.Equal(conf.Password, "secret");
            }
        }

        [Fact]
        public void Test2()
        {
            var conf = new Conference()
                           {
                               Name = "Council of Oberon",
                               AutoJoin = true,
                               Jid = "council@conference.underhill.org",
                               Nickname = "Puck",
                               Password = "secret"
                           };
            
            conf.ShouldBe(XML1);
        }
    }
}
