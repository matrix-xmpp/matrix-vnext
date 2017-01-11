using Matrix.Xml;
using Matrix.Xmpp.PubSub.Event;
using NUnit.Framework;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub.Event
{
    
    public class AssociateTest
    {
        private const string XML1 = @"<associate node='new-node-id' xmlns='http://jabber.org/protocol/pubsub#event'/>";

        [Fact]
        public void Test()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Associate);

            var ass = xmpp1 as Associate;
            if (ass != null)
            {
                Assert.Equal(ass.Node, "new-node-id");
            }

        }

        [Fact]
        public void Test2()
        {
            new Associate { Node = "new-node-id" }.
            ShouldBe(XML1);
        }
    }
}