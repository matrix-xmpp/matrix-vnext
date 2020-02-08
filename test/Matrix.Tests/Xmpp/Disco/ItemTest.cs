/*
 * Copyright (c) 2003-2020 by AG-Software <info@ag-software.de>
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

using Matrix.Xml;
using Matrix.Xmpp.Disco;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Disco
{
    public class ItemTest
    {
        [Fact]
        public void ElementShouldBeOfTypeItem()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Disco.item1.xml")).ShouldBeOfType<Item>();
        }

        [Fact]
        public void TestItemProperties()
        {
            var item = XmppXElement.LoadXml(Resource.Get("Xmpp.Disco.item1.xml")).Cast<Item>();
            item.Node.ShouldBe("node1");
            item.Jid.Bare.ShouldBe("user1@server.com");
            item.Name.ShouldBe("name1");
        }
    }
}
