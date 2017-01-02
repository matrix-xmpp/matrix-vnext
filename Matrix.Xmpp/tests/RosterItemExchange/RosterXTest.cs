using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Matrix.Xml;
using Matrix.Xmpp.Roster;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Xmpp.RosterItemExchange
{
    [TestClass]
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

        [TestMethod]
        public void RosterItemExchangeTest()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML_1);
            Assert.AreEqual(true, xmpp1 is Matrix.Xmpp.RosterItemExchange.Exchange);
        }

        [TestMethod]
        public void RosterExchangeItemTest()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML_2);
            Assert.AreEqual(true, xmpp1 is Matrix.Xmpp.RosterItemExchange.RosterExchangeItem);
            var item = xmpp1 as Matrix.Xmpp.RosterItemExchange.RosterExchangeItem;
            Assert.AreEqual(item.Action == Matrix.Xmpp.RosterItemExchange.Action.Add, true);

            XmppXElement xmpp3 = XmppXElement.LoadXml(XML_3);
            Assert.AreEqual(true, xmpp3 is Matrix.Xmpp.RosterItemExchange.RosterExchangeItem);
            var item3 = xmpp3 as Matrix.Xmpp.RosterItemExchange.RosterExchangeItem;
            Assert.AreEqual(item3.Action == Matrix.Xmpp.RosterItemExchange.Action.Add, true);

            XmppXElement xmpp4 = XmppXElement.LoadXml(XML_4);
            Assert.AreEqual(true, xmpp4 is Matrix.Xmpp.RosterItemExchange.RosterExchangeItem);
            var item4 = xmpp4 as Matrix.Xmpp.RosterItemExchange.RosterExchangeItem;
            Assert.AreEqual(item4.Action == Matrix.Xmpp.RosterItemExchange.Action.Modify, true);
        }

        [TestMethod]
        public void RosterItemExchange_With_ItemsTest()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML_5);
            Assert.AreEqual(true, xmpp1 is Matrix.Xmpp.RosterItemExchange.Exchange);
            
            var rosterx = xmpp1 as Matrix.Xmpp.RosterItemExchange.Exchange;
            var items = rosterx.GetRosterExchangeItems();

            Assert.AreEqual(items.Count(), 2);
        }
    }
}
