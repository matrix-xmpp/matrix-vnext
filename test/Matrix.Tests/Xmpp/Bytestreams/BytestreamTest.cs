using System.Collections.Generic;
using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.Bytestreams;
using NUnit.Framework;
using Xunit;
using Shouldly;


namespace Matrix.Tests.Xmpp.Bytestreams
{
    
    public class BytestreamTest
    {
        const string XML1 = @"<query xmlns='http://jabber.org/protocol/bytestreams' 
         sid='vxf9n471bn46'
         mode='tcp'>
    <streamhost 
        jid='initiator@example.com/foo' 
        host='192.168.4.1' 
        port='5086'/>
    <streamhost 
        jid='streamhostproxy.example.net' 
        host='24.24.24.1' 
        zeroconf='_jabber.bytestreams'/>
  </query>";

        private const string XML2 = @" <streamhost xmlns='http://jabber.org/protocol/bytestreams'
        jid='initiator@example.com/foo' 
        host='192.168.4.1' 
        port='5086'/>";

        private const string XML3 = @"<streamhost xmlns='http://jabber.org/protocol/bytestreams'
        jid='streamhostproxy.example.net' 
        host='24.24.24.1' 
        zeroconf='_jabber.bytestreams'/>";

        private const string XML4 = "<streamhost-used xmlns='http://jabber.org/protocol/bytestreams' jid='streamhostproxy.example.net'/>";

        private const string XML5 = "<activate xmlns='http://jabber.org/protocol/bytestreams'>target@example.org/bar</activate>";
        private const string XML6 = "<activate xmlns='http://jabber.org/protocol/bytestreams'/>";

        [Fact]
        public void Test1()
        {
            var xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Bytestream);

            var bs = xmpp1 as Bytestream;
            if (bs != null)
            {
                Assert.Equal(bs.Sid, "vxf9n471bn46");
                Assert.Equal(bs.Mode, Mode.Tcp);
                IEnumerable<Streamhost> hosts = bs.GetStreamhosts();
                Assert.Equal(hosts.Count(), 2);

                var host1 = bs.GetStreamhosts().First(h => h.Port == 5086);

                Assert.Equal(host1.Host, "192.168.4.1");
                Assert.Equal(host1.Jid.Equals("initiator@example.com/foo"), true);

                var host2 = bs.GetStreamhosts().First(h => h.Jid.Equals("streamhostproxy.example.net"));

                Assert.Equal(host2.Host, "24.24.24.1");
                Assert.Equal(host2.Zeroconf, "_jabber.bytestreams");
            }
        }

        [Fact]
        public void Test2()
        {
            var host = new Streamhost { Jid = "initiator@example.com/foo", Port = 5086, Host = "192.168.4.1" };
            host.ShouldBe(XML2);

            var host2 = new Streamhost { Jid = "streamhostproxy.example.net", Host = "24.24.24.1", Zeroconf = "_jabber.bytestreams" };
            host2.ShouldBe(XML3);
        }
        
        [Fact]
        public void Test3()
        {
            var hu = new StreamhostUsed {Jid = "streamhostproxy.example.net"};
            hu.ShouldBe(XML4);
        }

        [Fact]
        public void Test4()
        {
            var xmpp1 = XmppXElement.LoadXml(XML4);
            Assert.Equal(true, xmpp1 is StreamhostUsed);

            var su = xmpp1 as StreamhostUsed;
            if (su != null)
            {
                Assert.Equal(su.Jid.Equals("streamhostproxy.example.net"), true);
            }
        }

        [Fact]
        public void Test5()
        {
            var xmpp1 = XmppXElement.LoadXml(XML5);
            Assert.Equal(true, xmpp1 is Activate);

            var activate = xmpp1 as Activate;
            if (activate != null)
            {
                Assert.Equal(activate.Jid.Equals("target@example.org/bar"), true);
                
                activate.Jid = null;
                activate.ShouldBe(XML6);
            }
        }
    }
}