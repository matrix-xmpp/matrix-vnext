using Xunit;
using Matrix.Xml;
using Matrix.Xmpp.Roster;
using Shouldly;

namespace Matrix.Tests.Xmpp.Roster
{
    public class RosterTest
    {
        [Fact]
        public void ShouldBeOfTypeRosterItem()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Roster.item1.xml")).ShouldBeOfType<RosterItem>();
        }

        [Fact]
        public void TestGroups()
        {
            RosterItem ri2 = XmppXElement.LoadXml(Resource.Get("Xmpp.Roster.item1.xml")).Cast<RosterItem>();
            RosterItem ri3 = XmppXElement.LoadXml(Resource.Get("Xmpp.Roster.item2.xml")).Cast<RosterItem>();

            Assert.Equal(3, ri2.GetGroups().Count);
            Assert.Equal(0, ri3.GetGroups().Count);

            ri3.AddGroup("Test");
            Assert.Equal(1, ri3.GetGroups().Count);
            Assert.Equal("Test", ri3.GetGroups()[0]);
        }

        [Fact]
        public void TestApproved()
        {
            RosterItem ri = XmppXElement.LoadXml(Resource.Get("Xmpp.Roster.item3.xml")).Cast<RosterItem>();
            Assert.Equal(ri.Approved, true);

            ri.Approved = false;

            ri.ShouldBe(Resource.Get("Xmpp.Roster.item2.xml"));
        }
        
        [Fact]
        public void TestAsk1()
        {
            RosterItem ri = XmppXElement.LoadXml(Resource.Get("Xmpp.Roster.item4.xml")).Cast<RosterItem>();
            Assert.Equal(ri.Ask == Ask.Subscribe, true);
        }

        [Fact]
        public void TestAsk2()
        {
            RosterItem ri = XmppXElement.LoadXml(Resource.Get("Xmpp.Roster.item5.xml")).Cast<RosterItem>();
            Assert.Equal(ri.Ask == Ask.Unsubscribe, true);
        }

        [Fact]
        public void TestAsk3()
        {
            RosterItem ri = XmppXElement.LoadXml(Resource.Get("Xmpp.Roster.item6.xml")).Cast<RosterItem>();
            Assert.Equal(ri.Ask == Ask.None, true);
        }

        [Fact]
        public void TestSubscription1()
        {
            RosterItem ri = XmppXElement.LoadXml(Resource.Get("Xmpp.Roster.item1.xml")).Cast<RosterItem>();
            Assert.Equal(ri.Subscription == Subscription.Both, true);
        }

        [Fact]
        public void TestSubscription2()
        {
            RosterItem ri = XmppXElement.LoadXml(Resource.Get("Xmpp.Roster.item5.xml")).Cast<RosterItem>();
            Assert.Equal(ri.Subscription == Subscription.From, true);
        }
    }
}