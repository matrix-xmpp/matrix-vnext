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

using System.Collections.Generic;
using System.Linq;

using Matrix.Xml;
using Matrix.Xmpp.PubSub;

using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub
{
    public class PublishTest
    {
        [Fact]
        public void ShoudBeOfTypePublish()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.publish1.xml")).ShouldBeOfType<Publish>();
        }

        [Fact]
        public void TestPublish()
        {
            var publish = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.publish1.xml")).Cast<Publish>();
            Assert.Equal(publish.Node, "princely_musings");
            IEnumerable<Item> items = publish.GetItems();
            Assert.Equal(items.Count(), 3);
            Assert.Equal(items.ToArray()[0].Id, "ae890ac52d0df67ed7cfdf51b644e901");
            Assert.Equal(items.ToArray()[1].Id, "abc");
            Assert.Equal(items.ToArray()[2].Id, "def");
        }
    }
}
