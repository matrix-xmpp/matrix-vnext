using Matrix.Xmpp.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matrix.Xmpp.Tests.Client
{
    [TestClass]
    public class IqQueryTest
    {
        private string XML1 = @"<iq xmlns='jabber:client'>
            <query xmlns='jabber:iq:roster'/>
            </iq>";

        private string XML2 = @"<query xmlns='jabber:iq:roster'/>";

        private string XML3 = @"<iq xmlns='jabber:client' id='foo'>
            <query xmlns='jabber:iq:roster'/>
            </iq>";

        private string XML4 = @"<iq xmlns='jabber:client' id='foo'>
            <query xmlns='jabber:iq:roster'>
                <item jid='bar@server.com'/>
            </query>
            </iq>";

        [TestMethod]
        public void Test()
        {
            var rosterIq = new IqQuery<Matrix.Xmpp.Roster.Roster>();

            rosterIq.RemoveAttribute("id");
            rosterIq.ShouldBe(XML1);
            
            Matrix.Xmpp.Roster.Roster roster = rosterIq.Query;
            roster.ShouldBe(XML2);
        }

        [TestMethod]
        public void Test2()
        {
            var rosterIq = new IqQuery<Matrix.Xmpp.Roster.Roster> {Id = "foo"};
            rosterIq.ShouldBe(XML3);
        }

        [TestMethod]
        public void Test3()
        {
            var roster = new Matrix.Xmpp.Roster.Roster();
            roster.AddRosterItem(new Matrix.Xmpp.Roster.RosterItem("bar@server.com"));

            var rosterIq = new IqQuery<Matrix.Xmpp.Roster.Roster>(roster) {Id = "foo"};
            rosterIq.ShouldBe(XML4);
        }
    }
}
