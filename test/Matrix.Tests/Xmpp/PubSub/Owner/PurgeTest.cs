using Matrix.Xml;
using Matrix.Xmpp.PubSub.Owner;
using NUnit.Framework;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub.Owner
{
    
    public class PurgeTest
    {
        private const string XML1 =
            @"<purge node='princely_musings' xmlns='http://jabber.org/protocol/pubsub#owner'/>";
        
        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Purge);

            var purge = xmpp1 as Purge;
            if (purge != null)
            {
                Assert.Equal(purge.Node, "princely_musings");
            }

            new Purge {Node = "princely_musings"}.ShouldBe(XML1);
        }
    }
}
