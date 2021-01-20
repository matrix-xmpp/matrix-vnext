using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Disco;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Disco
{
    
    public class DiscoTest
    {
        [Fact]
        public void TestDiscoQuery()
        {
            var iq = XmppXElement.LoadXml(Resource.Get("Xmpp.Disco.iq1.xml")).Cast<Iq>();

            iq.Query.ShouldBeOfType<Items>();
            Items items = iq.Query.Cast<Items>();
            items.GetItems().Count().ShouldBe(3);
        }

        [Fact]
        public void BuildDiscoItems()
        {
            var expectedXml = Resource.Get("Xmpp.Disco.discoitems1.xml");

            Items items = new Items {Node = "http://jabber.org/protocol/tune"};

            items.AddItem(new Item("user1@server.com", "node1", "name1"));
            items.AddItem(new Item("user2@server.com", "node2", "name2"));
            items.AddItem(new Item("user3@server.com", "node3", "name3"));

            items.ShouldBe(expectedXml);
        }

        [Fact]
        public void BuildDiscoIqWithFeatures()
        {
            var expectedXml = Resource.Get("Xmpp.Disco.iq2.xml");
            var dIq = new DiscoInfoIq
                          {
                              Type = IqType.Result,
                              To = "user@server.com/resource",
                              Id = "id_from_request"
                          };
            dIq.Info.AddFeature(new Feature("urn:xmpp:jingle:1"));
            dIq.Info.AddFeature(new Feature("urn:xmpp:jingle:apps:rtp:audio"));
            dIq.Info.AddFeature(new Feature("urn:xmpp:jingle:apps:rtp:video"));

            dIq.ShouldBe(expectedXml);
        }
    }
}
