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

using System.Collections.Generic;
using Matrix.Xml;
using Xunit;
using Matrix.Xmpp.Disco;
using Shouldly;

namespace Matrix.Tests.Xmpp.Disco
{
    public class IdendityTest
    {
        [Fact]
        public void TestEquals()
        {
            var id = new Identity {Type = "t", Name = "n", Category = "c"};
            var id1 = new Identity { Type = "t", Name = "n", Category = "c" };
            var id2 = new Identity { Type = "t2", Name = "n2", Category = "c2" };

            Assert.Equal(id.Equals(id1), true);
            Assert.Equal(id.Equals(id2), false);

            var list = new List<Identity> {id};

            list.Contains(id).ShouldBeTrue();
            list.Contains(id1).ShouldBeTrue();
            list.Contains(id2).ShouldBeFalse();
        }

        [Fact]
        public void ElementShouldbeOfTypeIdendity()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Disco.idendity1.xml")).ShouldBeOfType<Identity>();
        }

        [Fact]
        public void TestIdendity()
        {
            var identity = XmppXElement.LoadXml(Resource.Get("Xmpp.Disco.idendity1.xml")).Cast<Identity>();

            identity.Category.ShouldBe("conference");
            identity.Type.ShouldBe("text");
            identity.Name.ShouldBe("Play-Specific Chatrooms");
        }

        [Fact]
        public void BuildIdendity()
        {
            var expectedXml = Resource.Get("Xmpp.Disco.idendity1.xml");

            Identity identity = new Identity("text", "Play-Specific Chatrooms", "conference");
            identity.ShouldBe(expectedXml);
        }
    }
}
