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
using Matrix.Xmpp.ResultSetManagement;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.ResultSetManagement
{
    public class ResultSetManagementTest
    {
        [Fact]
        public void ShouldBeOfTypeSet()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.ResultSetManagement.set1.xml")).ShouldBeOfType<Set>();
        }

        [Fact]
        public void TestSet()
        {
            var set = XmppXElement.LoadXml(Resource.Get("Xmpp.ResultSetManagement.set1.xml")).Cast<Set>();
            Assert.Equal(10, set.Maximum);
        }

        [Fact]
        public void TestBuildSet()
        {
            new Set { Maximum = 10 }.ShouldBe(Resource.Get("Xmpp.ResultSetManagement.set1.xml"));
        }

        [Fact]
        public void TestSett2()
        {
            var set = XmppXElement.LoadXml(Resource.Get("Xmpp.ResultSetManagement.set2.xml")).Cast<Set>();
            Assert.Equal(800, set.Count);
            Assert.Equal("peterpan@neverland.lit", set.Last);
            Assert.Equal("stpeter@jabber.org", set.First.Value);
            Assert.Equal(0, set.First.Index);
        }

        [Fact]
        public void TestBuildSett2()
        {
            new Set
            {
                First = new First { Value = "stpeter@jabber.org", Index = 0 },
                Last = "peterpan@neverland.lit",
                Count = 800
            }.ShouldBe(Resource.Get("Xmpp.ResultSetManagement.set2.xml"));
        }
    }
}
