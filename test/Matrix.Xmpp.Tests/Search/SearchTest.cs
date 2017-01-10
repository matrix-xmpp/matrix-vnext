using Matrix.Xmpp.Client;
using Matrix.Xmpp.Search;
using Xunit;
using Matrix.Xml;
using System.Linq;


namespace Matrix.Xmpp.Tests.Search
{
    [Collection("Factory collection")]
    public class SearchTest
    {
        // response to a reoster request
        string xml1 = @"<query xmlns='jabber:iq:search'>
    <instructions>foo</instructions>
    <first/>
    <last/>
    <nick/>
    <email>gnauck@ag-software.de</email>
  </query>";


        private string xml2 = @"<query xmlns='jabber:iq:search'>
    <item jid='juliet@capulet.com'>
      <first>Juliet</first>
      <last>Capulet</last>
      <nick>JuliC</nick>
      <email>juliet@shakespeare.lit</email>
    </item>
    <item jid='tybalt@shakespeare.lit'>
      <first>Tybalt</first>
      <last>Capulet</last>
      <nick>ty</nick>
      <email>tybalt@shakespeare.lit</email>
    </item>
  </query>";

        private string xml3 = @"<item xmlns='jabber:iq:search' jid='tybalt@shakespeare.lit'>
      <first>Tybalt</first>
      <last>Capulet</last>
      <nick>ty</nick>
      <email>tybalt@shakespeare.lit</email>
    </item>";

        private string xml4 = @"<iq xmlns='jabber:client' type='result'
    from='search.shakespeare.lit'   
    id='search2'>
 <query xmlns='jabber:iq:search'>
    <item jid='juliet@capulet.com'>
      <first>Juliet</first>
      <last>Capulet</last>
      <nick>JuliC</nick>
      <email>juliet@shakespeare.lit</email>
    </item>
    <item jid='tybalt@shakespeare.lit'>
      <first>Tybalt</first>
      <last>Capulet</last>
      <nick>ty</nick>
      <email>tybalt@shakespeare.lit</email>
    </item>
 </query>
</iq>";

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(xml1);
            
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.Search.Search);
            var search = xmpp1 as Matrix.Xmpp.Search.Search;
            
            Assert.Equal(search.Instructions, "foo");
            Assert.Equal(search.Email, "gnauck@ag-software.de");

            Matrix.Xmpp.Client.SearchIq siq = new Matrix.Xmpp.Client.SearchIq();
            siq.Search.Last = "";
        }

        [Fact]
        public void TestSeachWithItems()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(xml2);

            Assert.Equal(true, xmpp1 is Matrix.Xmpp.Search.Search);
            var search = xmpp1 as Matrix.Xmpp.Search.Search;

            var items = search.GetItems();

            Assert.Equal(items != null, true);
            Assert.Equal(items.Count(), 2);

        }


        [Fact]
        public void TestSeachItem()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(xml3);

            Assert.Equal(true, xmpp1 is Matrix.Xmpp.Search.SearchItem);
            var item = xmpp1 as Matrix.Xmpp.Search.SearchItem;

            Assert.Equal(item.Jid.ToString(), "tybalt@shakespeare.lit");
            Assert.Equal(item.First, "Tybalt");
            Assert.Equal(item.Last, "Capulet");
            Assert.Equal(item.Nick, "ty");
            Assert.Equal(item.Email, "tybalt@shakespeare.lit");
        }

        [Fact]
        public void TestCreateSearchItem()
        {
            var item = new SearchItem
                           {
                               Jid = "tybalt@shakespeare.lit",
                               First = "Tybalt",
                               Last = "Capulet",
                               Nick = "ty",
                               Email = "tybalt@shakespeare.lit"
                           };

            item.ShouldBe(xml3);
        }

        [Fact]
        public void TestCreateSearchResults()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(xml4);

            Assert.Equal(true, xmpp1 is Iq);
            var iq = xmpp1 as Iq;
            var searchQuery = iq.Query as Matrix.Xmpp.Search.Search;
            
            var first   = new [] {"Juliet", "Tybalt"};
            var jid     = new [] {"juliet@capulet.com", "tybalt@shakespeare.lit"};
            var last    = new [] { "Capulet", "Capulet" };
            var nick    = new [] { "JuliC", "ty" };
            var email   = new [] { "juliet@shakespeare.lit", "tybalt@shakespeare.lit" };
            int i = 0;
            foreach (var sItem in searchQuery.GetItems())
            {
                Assert.Equal(sItem.Jid.Bare, jid[i]);
                Assert.Equal(sItem.First, first[i]);
                Assert.Equal(sItem.Last, last[i]);
                Assert.Equal(sItem.First, first[i]);
                Assert.Equal(sItem.Nick, nick[i]);
                Assert.Equal(sItem.Email, email[i]);
                i++;
            }
            
        }
    }
}