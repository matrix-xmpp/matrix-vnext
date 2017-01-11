using Matrix.Xml;
using Matrix.Xmpp.PubSub;

using NUnit.Framework;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub
{
    
    public class UnSubscribeTest
    {
        private const string XML1
            = @"<unsubscribe
                    xmlns='http://jabber.org/protocol/pubsub'
                    node='princely_musings'
                    jid='francisco@denmark.lit'
                    subid='abcd'/>";

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Unsubscribe);

            var unsub = xmpp1 as Unsubscribe;
            Assert.Equal(unsub.Node, "princely_musings");
            Assert.Equal(unsub.Jid.ToString(), "francisco@denmark.lit");
            Assert.Equal(unsub.SubId, "abcd");
        }

        [Fact]
        public void Test2()
        {
            var unsub = new Unsubscribe { Node = "princely_musings", Jid = "francisco@denmark.lit", SubId = "abcd" };
            
            unsub.ShouldBe(XML1);
        }

    }
}
