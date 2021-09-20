using Matrix.Xmpp.Client;
using Xunit;

namespace Matrix.Tests.Xmpp.Client
{
    public class IqQueryTest
    {
        [Fact]
        public void BuildRosterIq()
        {
            var expectedXml1 = Resource.Get("Xmpp.Client.rosteriq1.xml");
            var expectedXml2 = Resource.Get("Xmpp.Client.rosteriq2.xml");

            var rosterIq = new IqQuery<Matrix.Xmpp.Roster.Roster>();
            rosterIq.RemoveAttribute("id");
            rosterIq.ShouldBe(expectedXml1);
            
            Matrix.Xmpp.Roster.Roster roster = rosterIq.Query;
            roster.ShouldBe(expectedXml2);
        }

        [Fact]
        public void Test2()
        {
            var expectedXml = Resource.Get("Xmpp.Client.rosteriq3.xml");

            var rosterIq = new IqQuery<Matrix.Xmpp.Roster.Roster> {Id = "foo"};
            rosterIq.ShouldBe(expectedXml);
        }

        [Fact]
        public void Test3()
        {
            var expectedXml = Resource.Get("Xmpp.Client.rosteriq4.xml");

            var roster = new Matrix.Xmpp.Roster.Roster();
            roster.AddRosterItem(new Matrix.Xmpp.Roster.RosterItem("bar@server.com"));

            var rosterIq = new IqQuery<Matrix.Xmpp.Roster.Roster>(roster) {Id = "foo"};
            rosterIq.ShouldBe(expectedXml);
        }
    }
}
