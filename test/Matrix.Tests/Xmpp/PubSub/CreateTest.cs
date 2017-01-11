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
    
    public class CreateTest
    {
        private const string XML1 
            = @"<create xmlns='http://jabber.org/protocol/pubsub' node='generic/pgm-mp3-player'/>";
        
        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Create);

            var c = xmpp1 as Create;
            Assert.Equal(c.Node, "generic/pgm-mp3-player");
        }

        [Fact]
        public void Test2()
        {
            var create = new Create { Node = "generic/pgm-mp3-player" };
            create.ShouldBe(XML1);
        }
    }
}
