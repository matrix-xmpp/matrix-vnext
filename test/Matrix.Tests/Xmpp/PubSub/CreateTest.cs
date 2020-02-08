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
using Matrix.Xmpp.PubSub;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub
{
    public class CreateTest
    {
        [Fact]
        public void ShoudBeOfTypeConfigure()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.create1.xml")).ShouldBeOfType<Create>();
        }

        [Fact]
        public void TestCreate()
        {
            var c = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.create1.xml")).Cast<Create>(); ;
            Assert.Equal(c.Node, "generic/pgm-mp3-player");
        }

        [Fact]
        public void TestBuildCreate()
        {
            var create = new Create { Node = "generic/pgm-mp3-player" };
            create.ShouldBe(Resource.Get("Xmpp.PubSub.Owner.create1.xml"));
        }
    }
}
