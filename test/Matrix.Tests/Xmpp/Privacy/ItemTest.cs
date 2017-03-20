/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
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

using Matrix.Xml;
using Matrix.Xmpp.Privacy;
using Shouldly;
using Xunit;
using Item = Matrix.Xmpp.Privacy.Item;

namespace Matrix.Tests.Xmpp.Privacy
{
    
    public class ItemTest
    {
        [Fact]
        public void ShoudBeOfTypeItem()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Privacy.item1.xml")).ShouldBeOfType<Item>();
        }

        [Fact]
        public void TestItem()
        {
            var item = XmppXElement.LoadXml(Resource.Get("Xmpp.Privacy.item1.xml")).Cast<Item>();
            Assert.Equal(item.Val,  "none");
            Assert.Equal(item.Action, Matrix.Xmpp.Privacy.Action.Deny);
            Assert.Equal(item.Order, 5);
            Assert.Equal(item.Type, Matrix.Xmpp.Privacy.Type.Subscription);
        }

        [Fact]
        public void TestBuildItem()
        {
            var item = new Item { Val = "none", Type = Type.Subscription, Action = Action.Deny, Order = 5};
            item.ShouldBe(Resource.Get("Xmpp.Privacy.item1.xml"));

            item.Type = Type.None;
            item.ShouldBe(Resource.Get("Xmpp.Privacy.item2.xml"));

            item = new Item { Stanza = Stanza.Message | Stanza.IncomingPresence };
            item.ShouldBe(Resource.Get("Xmpp.Privacy.item3.xml"));

            item = new Item { Stanza = Stanza.All };
            item.ShouldBe(Resource.Get("Xmpp.Privacy.item4.xml"));
        }
    }
}
