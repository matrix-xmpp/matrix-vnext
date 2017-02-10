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

            Assert.Equal(pres.Show == Matrix.Xmpp.Show.DoNotDisturb, true);
            Assert.Equal(pres.Nick.Value, "Alex");
            
            var mucUser = pres.MucUser;
            var item = mucUser.Item;
            Assert.Equal(item.Role == Matrix.Xmpp.Muc.Role.Participant, true);
            Assert.Equal(item.Affiliation == Matrix.Xmpp.Muc.Affiliation.Member, true);
            Assert.Equal(item.Jid == "XXX@gmail.com/gmail.59477926", true);
        }
    }
}
