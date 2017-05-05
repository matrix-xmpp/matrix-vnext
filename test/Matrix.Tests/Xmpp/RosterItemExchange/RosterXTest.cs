/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
 * See the COPYING file for more information.
 *
 * This file is part of the MatriX project.
 *
 * NOTICE: All information contained herein is, and remains the property
 * of AG-Software and its suppliers, if any.
 * The intellectual and technical concepts contained herein are proprietary
 * to AG-Software and its suppliers and may be covered by German and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 *
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from AG-Software.
 *
 * Contact information for AG-Software is available at http://www.ag-software.de
 */

using System.Linq;
using Matrix.Xml;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.RosterItemExchange
{
    public class RosterXTest
    {
        [Fact]
        public void ShouldBeOfTypeExchange()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.RosterItemExchange.x1.xml")).ShouldBeOfType<Matrix.Xmpp.RosterItemExchange.Exchange>();
            XmppXElement.LoadXml(Resource.Get("Xmpp.RosterItemExchange.x2.xml")).ShouldBeOfType<Matrix.Xmpp.RosterItemExchange.Exchange>();
        }

        [Fact]
        public void ShouldBeOfTypeRosterExchangeItem()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.RosterItemExchange.item1.xml")).ShouldBeOfType<Matrix.Xmpp.RosterItemExchange.RosterExchangeItem>();
        }

        [Fact]
        public void TestRosterExchangeItem()
        {
            var item = XmppXElement.LoadXml(Resource.Get("Xmpp.RosterItemExchange.item1.xml")).ShouldBeOfType<Matrix.Xmpp.RosterItemExchange.RosterExchangeItem>();
            Assert.Equal(item.Action == Matrix.Xmpp.RosterItemExchange.Action.Add, true);

            var item3 = XmppXElement.LoadXml(Resource.Get("Xmpp.RosterItemExchange.item2.xml")).ShouldBeOfType<Matrix.Xmpp.RosterItemExchange.RosterExchangeItem>();
            Assert.Equal(item3.Action == Matrix.Xmpp.RosterItemExchange.Action.Add, true);
            
            var item4 = XmppXElement.LoadXml(Resource.Get("Xmpp.RosterItemExchange.item3.xml")).ShouldBeOfType<Matrix.Xmpp.RosterItemExchange.RosterExchangeItem>();
            Assert.Equal(item4.Action == Matrix.Xmpp.RosterItemExchange.Action.Modify, true);
        }

        [Fact]
        public void TestRosterItemExchangeWithItems()
        {
            var rosterx = XmppXElement.LoadXml(Resource.Get("Xmpp.RosterItemExchange.x2.xml")).ShouldBeOfType<Matrix.Xmpp.RosterItemExchange.Exchange>();
            var items = rosterx.GetRosterExchangeItems();

            Assert.Equal(items.Count(), 2);
        }
    }
}
