using Matrix.Xml;
using Matrix.Xmpp.Capabilities;
using Matrix.Xmpp.Disco;
using Matrix.Xmpp.Stream;
using Xunit;

namespace Matrix.Xmpp.Tests.Capabilities
{
    [Collection("Factory collection")]
    public class CapsTest
    {
        private const string XML1 = @"<query xmlns='http://jabber.org/protocol/disco#info'
         node='http://psi-im.org#q07IKJEyjvHSyhy//CH0CxmKi8w='>
    <identity xml:lang='en' category='client' name='Psi 0.11' type='pc'/>
    <identity xml:lang='el' category='client' name='Ψ 0.11' type='pc'/>
    <feature var='http://jabber.org/protocol/caps'/>
    <feature var='http://jabber.org/protocol/disco#info'/>
    <feature var='http://jabber.org/protocol/disco#items'/>
    <feature var='http://jabber.org/protocol/muc'/>
    <x xmlns='jabber:x:data' type='result'>
      <field var='FORM_TYPE' type='hidden'>
        <value>urn:xmpp:dataforms:softwareinfo</value>
      </field>
      <field var='ip_version'>
        <value>ipv4</value>
        <value>ipv6</value>
      </field>
      <field var='os'>
        <value>Mac</value>
      </field>
      <field var='os_version'>
        <value>10.5.1</value>
      </field>
      <field var='software'>
        <value>Psi</value>
      </field>
      <field var='software_version'>
        <value>0.11</value>
      </field>
    </x>
  </query>";


        private const string XML2 = @"<query xmlns='http://jabber.org/protocol/disco#info'
         node='http://psi-im.org#q07IKJEyjvHSyhy//CH0CxmKi8w='>
    <identity xml:lang='en' category='client' name='Psi 0.11' type='pc'/>
    <identity xml:lang='el' category='client' name='Ψ 0.11' type='pc'/>
    <feature var='http://jabber.org/protocol/caps'/>
    <feature var='http://jabber.org/protocol/disco#info'/>
    <feature var='http://jabber.org/protocol/disco#items'/>
    <feature var='http://jabber.org/protocol/muc'/>
    <x xmlns='jabber:x:data' type='result'>
      <field var='FORM_TYPE' type='hidden'>
        <value>urn:xmpp:dataforms:softwareinfo10</value>
      </field>
      <field var='ip_version'>
        <value>ipv4</value>
        <value>ipv6</value>
      </field>
      <field var='os'>
        <value>Mac</value>
      </field>
      <field var='os_version'>
        <value>10.5.1</value>
      </field>
      <field var='software'>
        <value>Psi</value>
      </field>
      <field var='software_version'>
        <value>0.11</value>
      </field>
    </x>    
    <x xmlns='jabber:x:data' type='result'>
      <field var='FORM_TYPE' type='hidden'>
        <value>urn:xmpp:dataforms:softwareinfo</value>
      </field>
      <field var='ip_version'>
        <value>ipv4</value>
        <value>ipv6</value>
      </field>
      <field var='os'>
        <value>Mac</value>
      </field>
      <field var='os_version'>
        <value>10.5.1</value>
      </field>
      <field var='software'>
        <value>Psi</value>
      </field>
      <field var='software_version'>
        <value>0.11</value>
      </field>
    </x>
  </query>";

        private const string XML3 = @"<stream:features xmlns:stream='http://etherx.jabber.org/streams'>
  <c xmlns='http://jabber.org/protocol/caps'
     hash='sha-1'
     node='http://jabberd.org'
     ver='ItBTI0XLDFvVxZ72NQElAzKS9sU='/>
</stream:features>";

        private const string XML4 = @"<query xmlns='http://jabber.org/protocol/disco#info'>
    <identity category='client' name='Swift' type='pc' />
    <feature var='http://jabber.org/protocol/chatstates' />
    <feature var='urn:xmpp:sec-label:0' />
  </query>";

        private const string XML5 = @"<query xmlns='http://jabber.org/protocol/disco#info' node='http://gajim.org#8ungGR8ouA8Bi9LIUp8r3+1tgbY='>
    <identity type='pc' name='Gajim' category='client' />
    <feature var='http://jabber.org/protocol/bytestreams' />
    <feature var='http://jabber.org/protocol/si' />
    <feature var='http://jabber.org/protocol/si/profile/file-transfer' />
    <feature var='http://jabber.org/protocol/muc' />
    <feature var='http://jabber.org/protocol/muc#user' />
    <feature var='http://jabber.org/protocol/muc#admin' />
    <feature var='http://jabber.org/protocol/muc#owner' />
    <feature var='http://jabber.org/protocol/muc#roomconfig' />
    <feature var='http://jabber.org/protocol/commands' />
    <feature var='http://jabber.org/protocol/disco#info' />
    <feature var='ipv6' />
    <feature var='jabber:iq:gateway' />
    <feature var='jabber:iq:last' />
    <feature var='jabber:iq:privacy' />
    <feature var='jabber:iq:private' />
    <feature var='jabber:iq:register' />
    <feature var='jabber:iq:version' />
    <feature var='jabber:x:data' />
    <feature var='jabber:x:encrypted' />
    <feature var='msglog' />
    <feature var='sslc2s' />
    <feature var='stringprep' />
    <feature var='urn:xmpp:ping' />
    <feature var='urn:xmpp:time' />
    <feature var='urn:xmpp:ssn' />
    <feature var='http://jabber.org/protocol/mood' />
    <feature var='http://jabber.org/protocol/activity' />
    <feature var='http://jabber.org/protocol/nick' />
    <feature var='http://jabber.org/protocol/rosterx' />
    <feature var='urn:xmpp:sec-label:0' />
    <feature var='http://jabber.org/protocol/mood+notify' />
    <feature var='http://jabber.org/protocol/activity+notify' />
    <feature var='http://jabber.org/protocol/tune+notify' />
    <feature var='http://jabber.org/protocol/nick+notify' />
    <feature var='http://jabber.org/protocol/geoloc+notify' />
    <feature var='http://jabber.org/protocol/chatstates' />
    <feature var='http://jabber.org/protocol/xhtml-im' />
    <feature var='urn:xmpp:receipts' />
    <feature var='urn:xmpp:jingle:1' />
    <feature var='urn:xmpp:jingle:apps:rtp:1' />
    <feature var='urn:xmpp:jingle:apps:rtp:audio' />
    <feature var='urn:xmpp:jingle:apps:rtp:video' />
    <feature var='urn:xmpp:jingle:transports:ice-udp:1' />
  </query>";

        [Fact]
        public void TestBuildHash()
        {
            var el = XmppXElement.LoadXml(XML1);
            if (el is Info)
            {
                var hash = Caps.BuildHash(el as Info);
                Assert.Equal(hash, "q07IKJEyjvHSyhy//CH0CxmKi8w=");
            }
        }

        [Fact]
        public void TestBuildHash2()
        {
            var el = XmppXElement.LoadXml(XML4);
            if (el is Info)
            {
                string hash = Caps.BuildHash(el as Info);
                Assert.Equal("XH3meI11JZj00/DhOlop7p7jKBE=", hash);
            }
        }
        
        [Fact]
        public void TestBuildHash5()
        {
            var el = XmppXElement.LoadXml(XML5);
            if (el is Info)
            {
                string hash = Caps.BuildHash(el as Info);
                Assert.Equal("8ungGR8ouA8Bi9LIUp8r3+1tgbY=", hash);

            }
        }

        [Fact]
        public void TestCapsInStreamFeatures()
        {
            var el = XmppXElement.LoadXml(XML3);
            if (el is StreamFeatures)
            {
                var feat = el as StreamFeatures;
                var caps = feat.Caps;
                Assert.Equal(caps.Node, "http://jabberd.org");
                Assert.Equal(caps.Version, "ItBTI0XLDFvVxZ72NQElAzKS9sU=");
                Assert.Equal(caps.Hash, "sha-1");
            }
        }
    }
}
