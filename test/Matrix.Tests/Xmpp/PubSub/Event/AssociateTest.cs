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
    public class AssociateTest
    {
        [Fact]
        public void ShoudBeOfTypeAssociate()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.associate1.xml")).ShouldBeOfType<Associate>();
        }

        [Fact]
        public void TestAssociate()
        {
            var ass = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Event.associate1.xml")).Cast<Associate>();
            Assert.Equal(ass.Node, "new-node-id");
        }

        [Fact]
        public void TestBuildAssociate()
        {
            new Associate { Node = "new-node-id" }.
            ShouldBe(Resource.Get("Xmpp.PubSub.Event.associate1.xml"));
        }
    }
}
