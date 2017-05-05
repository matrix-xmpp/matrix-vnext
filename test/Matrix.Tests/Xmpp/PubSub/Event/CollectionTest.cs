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

using Matrix.Xml;
using Matrix.Xmpp.PubSub.Event;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub.Event
{
    
    public class CollectionTest
    {
        [Fact]
        public void ShoudBeOfTypeCollection()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.collection1.xml")).ShouldBeOfType<Collection>();
        }

        [Fact]
        public void TestColelciton()
        {
            var col = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.collection1.xml")).Cast<Collection>();
            Assert.Equal(col.Node, "some-collection");
            Assert.Equal(col.FirstElement is Disassociate, true);
        
            var col2 = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.collection2.xml")).Cast<Collection>();
            Assert.Equal(col2.Node, "some-collection");
            Assert.Equal(col2.FirstElement is Associate, true);
        }

        [Fact]
        public void TestBuildCollection()
        {
            var col = new Collection {Node = "some-collection"};
            col.Add(new Disassociate {Node = "new-node-id"});

            col.ShouldBe(Resource.Get("Xmpp.PubSub.Event.collection1.xml"));

            var col2 = new Collection
            {
                Node = "some-collection",
                Disassociate = new Disassociate {Node = "new-node-id"}
            };
            col2.ShouldBe(Resource.Get("Xmpp.PubSub.Event.collection1.xml"));

            var col3 = new Collection
            {
                Node = "some-collection",
                Associate = new Associate {Node = "new-node-id"}
            };

            col3.ShouldBe(Resource.Get("Xmpp.PubSub.Event.collection2.xml"));

            Assert.Equal(col3.Associate != null, true);
            col3.Disassociate = new Disassociate {Node = "123"};
            Assert.Equal(col3.Associate == null, true);
            Assert.Equal(col3.Disassociate != null, true);
        }
    }
}
