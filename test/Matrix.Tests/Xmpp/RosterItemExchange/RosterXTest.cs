using System.Linq;
using Matrix.Xml;
using Xunit;

namespace Matrix.Tests.Xmpp.RosterItemExchange
{
    
    public class RosterXTest
    {
        /*
         * <message from='horatio@denmark.lit' to='hamlet@denmark.lit'>
                <body>Some visitors, m'lord!</body>
                <x xmlns='http://jabber.org/protocol/rosterx'> 
                <item action='add'
                        jid='rosencrantz@denmark.lit'
                        name='Rosencrantz'>
                    <group>Visitors</group>
                </item>
                <item action='add'
                        jid='guildenstern@denmark.lit'
                        name='Guildenstern'>
                    <group>Visitors</group>
                </item>
                </x>
            </message>
         */

        private const string XML_1 = " <x xmlns='http://jabber.org/protocol/rosterx'/>";
        private const string XML_2 = @"<item xmlns='http://jabber.org/protocol/rosterx'
                            action='add'
                            jid='rosencrantz@denmark.lit'
                            name='Rosencrantz'>
                      <group>Visitors</group>
                    </item>";
        
        private const string XML_3 = @"<item xmlns='http://jabber.org/protocol/rosterx'
                            jid='rosencrantz@denmark.lit'
                            name='Rosencrantz'>
                      <group>Visitors</group>
                    </item>";

        private const string XML_4 = @"<item xmlns='http://jabber.org/protocol/rosterx'
                            action='modify'                            
                            jid='rosencrantz@denmark.lit'
                            name='Rosencrantz'>
                      <group>Visitors</group>
                    </item>";


        private const string XML_5 = @"<x xmlns='http://jabber.org/protocol/rosterx'> 
                        <item action='add'
                              jid='rosencrantz@denmark.lit'
                              name='Rosencrantz'>
                          <group>Visitors</group>
                        </item>
                        <item action='add'
                              jid='guildenstern@denmark.lit'
                              name='Guildenstern'>
                          <group>Visitors</group>
                        </item>
                      </x>";

        [Fact]
        public void RosterItemExchangeTest()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML_1);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.RosterItemExchange.Exchange);
        }

        [Fact]
        public void RosterExchangeItemTest()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML_2);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.RosterItemExchange.RosterExchangeItem);
            var item = xmpp1 as Matrix.Xmpp.RosterItemExchange.RosterExchangeItem;
            Assert.Equal(item.Action == Matrix.Xmpp.RosterItemExchange.Action.Add, true);

            XmppXElement xmpp3 = XmppXElement.LoadXml(XML_3);
            Assert.Equal(true, xmpp3 is Matrix.Xmpp.RosterItemExchange.RosterExchangeItem);
            var item3 = xmpp3 as Matrix.Xmpp.RosterItemExchange.RosterExchangeItem;
            Assert.Equal(item3.Action == Matrix.Xmpp.RosterItemExchange.Action.Add, true);

            XmppXElement xmpp4 = XmppXElement.LoadXml(XML_4);
            Assert.Equal(true, xmpp4 is Matrix.Xmpp.RosterItemExchange.RosterExchangeItem);
            var item4 = xmpp4 as Matrix.Xmpp.RosterItemExchange.RosterExchangeItem;
            Assert.Equal(item4.Action == Matrix.Xmpp.RosterItemExchange.Action.Modify, true);
        }

        [Fact]
        public void RosterItemExchange_With_ItemsTest()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML_5);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.RosterItemExchange.Exchange);
            
            var rosterx = xmpp1 as Matrix.Xmpp.RosterItemExchange.Exchange;
            var items = rosterx.GetRosterExchangeItems();

            Assert.Equal(items.Count(), 2);
        }
    }
}
