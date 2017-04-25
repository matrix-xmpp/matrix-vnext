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
using Matrix.Xmpp.Stream.Features;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Stream.Features
{
    public class FeatureTest
    {
        [Fact]
        public void TestShouldbeOfTypeRosterVersioning()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Stream.Features.ver1.xml")).ShouldBeOfType<RosterVersioning>();
        }

        [Fact]
        public void TestRosterVersioning()
        {
            var rv = XmppXElement.LoadXml(Resource.Get("Xmpp.Stream.Features.ver1.xml")).Cast<RosterVersioning>();
            Assert.Equal(rv.Optional, true);
            Assert.Equal(rv.Required, false);
        }

        [Fact]
        public void TestBuildRosterVersioning()
        {
            new RosterVersioning {Required = true}
                .ShouldBe(Resource.Get("Xmpp.Stream.Features.ver2.xml"));
        }
    }
}
