using System.Collections.Generic;
using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Disco;

using Xunit;

namespace Matrix.Xmpp.Tests.Disco
{
    [Collection("Factory collection")]
    public class DiscoTest
    {
        string xml1 = @"<iq from='romeo@montague.net'
    id='items4'
    to='juliet@capulet.com/chamber'
    xmlns='jabber:client' 
    type='result'>
  <query xmlns='http://jabber.org/protocol/disco#items' 
         node='http://jabber.org/protocol/tune'>
    <item jid='pubsub.shakespeare.lit'
          name='Romeo&apos;s CD player'
          node='s623nms9s3bfh8js'/>
    <item jid='pubsub.montague.net'
          node='music/R/Romeo/iPod'/>
    <item jid='tunes.characters.lit'
          node='g8k4kds9sd89djf3'/>
  </query>
</iq>";

        string xml2 = @"<query xmlns='http://jabber.org/protocol/disco#items' 
         node='http://jabber.org/protocol/tune'>
    <item jid='user1@server.com'
          name='name1'
          node='node1'/>
    <item jid='user2@server.com'
          name='name2'
          node='node2'/>
    <item jid='user3@server.com'
          name='name3'
          node='node3'/>    
  </query>";

        string xml3 = @"<iq type='result'
    from='plays.shakespeare.lit'
    to='romeo@montague.net/orchard'
    id='info1'>
  <query xmlns='http://jabber.org/protocol/disco#info'>
    <identity
        category='conference'
        type='text'
        name='Play-Specific Chatrooms'/>
    <identity
        category='directory'
        type='chatroom'
        name='Play-Specific Chatrooms'/>
    <feature var='http://jabber.org/protocol/disco#info'/>
    <feature var='http://jabber.org/protocol/disco#items'/>
    <feature var='http://jabber.org/protocol/muc'/>
    <feature var='jabber:iq:register'/>
    <feature var='jabber:iq:search'/>
    <feature var='jabber:iq:time'/>
    <feature var='jabber:iq:version'/>
  </query>
</iq>
";

        [Fact]
        public void DiscoTest1()
        {

            XmppXElement xmpp1 = XmppXElement.LoadXml(xml1);
            
            Assert.Equal(true, xmpp1 is Iq);
            Iq iq = xmpp1 as Iq;
                        
            Assert.Equal(true, iq.Query is Items);
            Items items = iq.Query as Items;

            Assert.Equal(3, items.GetItems().Count());
            IEnumerable<Item> query = items.GetItems();           
        }

        [Fact]
        public void DiscoTest2()
        {

            Items items = new Items();
            items.Node = "http://jabber.org/protocol/tune";

            items.AddItem(new Item("user1@server.com", "node1", "name1"));
            items.AddItem(new Item("user2@server.com", "node2", "name2"));
            items.AddItem(new Item("user3@server.com", "node3", "name3"));

            items.ShouldBe(xml2);
        }


        [Fact]
        public void DiscoTest3()
        {
            string xml = @"<item jid='user1@server.com'
            xmlns='http://jabber.org/protocol/disco#items'
            name='name1'
            node='node1'/>";

            XmppXElement el = XmppXElement.LoadXml(xml);

            Assert.Equal(true, el is Item);
            Item item = el as Item;

            Assert.Equal(item.Node, "node1");
            Assert.Equal(item.Jid.Bare, "user1@server.com");
            Assert.Equal(item.Name, "name1");
        }

        [Fact]
        public void DiscoTest4()
        {
            string xml = @"<identity
        xmlns='http://jabber.org/protocol/disco#info'
        category='conference'
        type='text'
        name='Play-Specific Chatrooms'/>";
    

            XmppXElement el = XmppXElement.LoadXml(xml);

            Assert.Equal(true, el is Identity);
            Identity id = el as Identity;

            Assert.Equal(id.Category, "conference");
            Assert.Equal(id.Type, "text");
            Assert.Equal(id.Name, "Play-Specific Chatrooms");

            Identity id2 = new Identity("text", "Play-Specific Chatrooms", "conference");
            id2.ShouldBe(xml);
        }

        [Fact]
        public void DiscoTest5()
        {
            string xml = @"<feature xmlns='http://jabber.org/protocol/disco#info' var='http://jabber.org/protocol/disco#info'/>";


            XmppXElement el = XmppXElement.LoadXml(xml);

            Assert.Equal(true, el is Feature);
            Feature feat = el as Feature;

            Assert.Equal(feat.Var, "http://jabber.org/protocol/disco#info");

            Feature feat2 = new Feature("http://jabber.org/protocol/disco#info");
            feat2.ShouldBe(xml);
        }

        [Fact]
        public void BuildDiscoFeaturesTest()
        {
            const string XML =
                @"<iq xmlns='jabber:client'
                id='id_from_request'
                to='user@server.com/resource'
                type='result'>
              <query xmlns='http://jabber.org/protocol/disco#info'>
                <feature var='urn:xmpp:jingle:1'/>                
                <feature var='urn:xmpp:jingle:apps:rtp:audio'/>
                <feature var='urn:xmpp:jingle:apps:rtp:video'/>
              </query>
            </iq>";


            var dIq = new DiscoInfoIq
                          {
                              Type = IqType.Result,
                              To = "user@server.com/resource",
                              Id = "id_from_request"
                          };
            dIq.Info.AddFeature(new Feature("urn:xmpp:jingle:1"));
            dIq.Info.AddFeature(new Feature("urn:xmpp:jingle:apps:rtp:audio"));
            dIq.Info.AddFeature(new Feature("urn:xmpp:jingle:apps:rtp:video"));
            dIq.ShouldBe(XML);
        }
    }
}
