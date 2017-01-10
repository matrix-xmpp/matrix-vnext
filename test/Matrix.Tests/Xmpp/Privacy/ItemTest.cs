using Matrix.Xml;
using Matrix.Xmpp.Privacy;
using Xunit;

namespace Matrix.Tests.Xmpp.Privacy
{
    
    public class ItemTest
    {
        private const string XML1
           = @"<item type='subscription' xmlns='jabber:iq:privacy' value='none' action='deny' order='5'/>";

        private const string XML2
          = @"<item xmlns='jabber:iq:privacy' value='none' action='deny' order='5'/>";
        
        private const string XML3
          = @"<item xmlns='jabber:iq:privacy'><message/><presence-in/></item>";

        private const string XML4
          = @"<item xmlns='jabber:iq:privacy'/>";

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Item);

            var item = xmpp1 as Item;
            Assert.Equal(item.Val,  "none");
            Assert.Equal(item.Action, Matrix.Xmpp.Privacy.Action.Deny);
            Assert.Equal(item.Order, 5);
            Assert.Equal(item.Type, Matrix.Xmpp.Privacy.Type.Subscription);
        }

        [Fact]
        public void Test2()
        {
            var item = new Item { Val = "none", Type = Matrix.Xmpp.Privacy.Type.Subscription, Action = Matrix.Xmpp.Privacy.Action.Deny, Order = 5};
            item.ShouldBe(XML1);

            item.Type = Matrix.Xmpp.Privacy.Type.None;
            item.ShouldBe(XML2);
        }

        [Fact]
        public void Test3()
        {
            var item = new Item {Stanza = Stanza.Message | Stanza.IncomingPresence};
            item.ShouldBe(XML3);
        }

        
        [Fact]
        public void Test4()
        {
            var item = new Item {Stanza = Stanza.All};
            item.ShouldBe(XML4);
        }

    }
}
