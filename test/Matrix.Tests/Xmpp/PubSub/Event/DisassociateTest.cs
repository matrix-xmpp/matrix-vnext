using Matrix.Xml;
using Matrix.Xmpp.PubSub.Event;
using NUnit.Framework;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub.Event
{
    
    public class DisassociateTest
    {
        private const string XML1 = @"<disassociate node='new-node-id' xmlns='http://jabber.org/protocol/pubsub#event'/>";
        
        [Fact]
        public void Test()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Disassociate);

            var dis = xmpp1 as Disassociate;
            if (dis != null)
            {
                Assert.Equal(dis.Node, "new-node-id");
            }   
        }

        [Fact]
        public void Test2()
        {
            new Disassociate {Node = "new-node-id"}.ShouldBe(XML1);
        }
    }
}