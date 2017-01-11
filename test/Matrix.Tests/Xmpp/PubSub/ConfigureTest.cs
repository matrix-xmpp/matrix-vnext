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
    
    public class ConfigureTest
    {
        private const string XML1
            = @"<configure xmlns='http://jabber.org/protocol/pubsub' node='princely_musings'/>";

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Configure);

            var conf = xmpp1 as Configure;
            Assert.Equal(conf.Node, "princely_musings");
        }

        [Fact]
        public void Test2()
        {
            var conf = new Configure { Node = "princely_musings" };
            conf.ShouldBe(XML1);
        }

    }
}
