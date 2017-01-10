using System.Collections.Generic;
using System.Linq;
using Matrix.Xmpp.XData;
using Xunit;
using Matrix.Xml;
using Matrix.Xmpp.Disco;

namespace Matrix.Tests.Xmpp.Disco
{
    
    public class DiscoInfoTest
    {
        private const string xml1 = @"<query xmlns='http://jabber.org/protocol/disco#info'>
  <identity category='conference' name='Liverpool' type='text' />
  <feature var='http://jabber.org/protocol/muc' />
  <feature var='muc_public' />
  <feature var='muc_open' />
  <feature var='muc_unmoderated' />
  <feature var='muc_semianonymous' />
  <feature var='muc_unsecured' />
  <feature var='muc_persistent' />
  <feature var='http://jabber.org/protocol/disco#info' />
  <x xmlns='jabber:x:data' type='result'>
    <field var='FORM_TYPE' type='hidden'>
      <value>http://jabber.org/protocol/muc#roominfo</value>
    </field>
    <field label='Description' var='muc#roominfo_description'>
      <value>Premier League</value>
    </field>
    <field label='Subject' var='muc#roominfo_subject'>
      <value />
    </field>
    <field label='Number of occupants' var='muc#roominfo_occupants'>
      <value>3</value>
    </field>
    <field label='Creation date' var='x-muc#roominfo_creationdate'>
      <value>20090318T10:01:47</value>
    </field>
  </x>
</query>";
        
        [Fact]
        public void Test1()
        {
            var xmpp1 = XmppXElement.LoadXml(xml1);

            Assert.Equal(true, xmpp1 is Info);
            var info = xmpp1 as Info;

            if (info != null)
            {
                var xdata = info.XData;
                Assert.Equal(info != null, true);

                IEnumerable<Field> fields = xdata.GetFields();
                Assert.Equal(fields.Count(), 5);


                Assert.Equal(xdata.HasField("muc#roominfo_description"), true);
                Assert.Equal(xdata.HasField("muc#roominfo_occupants"), true);
                Assert.Equal(xdata.HasField("muc#roominfo_description2"), false);

                if (xdata.HasField("muc#roominfo_description"))
                    Assert.Equal(xdata.GetField("muc#roominfo_description").GetValue(), "Premier League");

                if (xdata.HasField("muc#roominfo_occupants"))
                    Assert.Equal(xdata.GetField("muc#roominfo_occupants").GetValue(), "3");
            }
        }
    }
}
