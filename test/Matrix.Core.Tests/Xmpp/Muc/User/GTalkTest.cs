using Matrix.Xml;
using Matrix.Xmpp.Client;
using Xunit;

namespace Matrix.Tests.Xmpp.Muc.User
{
    public class GTalkTest
    {
        [Fact]
        public void TestGtalkPresence()
        {
            var pres = XmppXElement.LoadXml(Resource.Get("Xmpp.Muc.User.presence_gtalk1.xml")).Cast<Presence>();

            Assert.True(pres.Show == Matrix.Xmpp.Show.DoNotDisturb);
            Assert.Equal("Alex", pres.Nick.Value);
            
            var mucUser = pres.MucUser;
            var item = mucUser.Item;
            Assert.True(item.Role == Matrix.Xmpp.Muc.Role.Participant);
            Assert.True(item.Affiliation == Matrix.Xmpp.Muc.Affiliation.Member);
            Assert.True(item.Jid == "XXX@gmail.com/gmail.59477926");
        }
    }
}
