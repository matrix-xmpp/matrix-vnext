using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Matrix.Xml;
using Matrix.Xmpp.PubSub;
using NUnit.Framework;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub
{
    
    public class SubscribeTest
    {
        private const string XML1
            = @"<subscribe
                    xmlns='http://jabber.org/protocol/pubsub'
                    node='princely_musings'
                    jid='francisco@denmark.lit'/>";

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Subscribe);

            var sub = xmpp1 as Subscribe;
            Assert.Equal(sub.Node, "princely_musings");
            Assert.Equal(sub.Jid.ToString(), "francisco@denmark.lit");
        }

        [Fact]
        public void Test2()
        {
            var sub = new Subscribe { Node = "princely_musings", Jid = "francisco@denmark.lit" };
            sub.ShouldBe(XML1);
        }
    }
}
