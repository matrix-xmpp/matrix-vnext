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

using Matrix.Xmpp.Client;
using Xunit;

namespace Matrix.Tests.Xmpp.Client
{
    public class IqQueryTest
    {
        [Fact]
        public void BuildRosterIq()
        {
            var expectedXml1 = Resource.Get("Xmpp.Client.rosteriq1.xml");
            var expectedXml2 = Resource.Get("Xmpp.Client.rosteriq2.xml");

            var rosterIq = new IqQuery<Matrix.Xmpp.Roster.Roster>();
            rosterIq.RemoveAttribute("id");
            rosterIq.ShouldBe(expectedXml1);
            
            Matrix.Xmpp.Roster.Roster roster = rosterIq.Query;
            roster.ShouldBe(expectedXml2);
        }

        [Fact]
        public void Test2()
        {
            var expectedXml = Resource.Get("Xmpp.Client.rosteriq3.xml");

            var rosterIq = new IqQuery<Matrix.Xmpp.Roster.Roster> {Id = "foo"};
            rosterIq.ShouldBe(expectedXml);
        }

        [Fact]
        public void Test3()
        {
            var expectedXml = Resource.Get("Xmpp.Client.rosteriq4.xml");

            var roster = new Matrix.Xmpp.Roster.Roster();
            roster.AddRosterItem(new Matrix.Xmpp.Roster.RosterItem("bar@server.com"));

            var rosterIq = new IqQuery<Matrix.Xmpp.Roster.Roster>(roster) {Id = "foo"};
            rosterIq.ShouldBe(expectedXml);
        }
    }
}
