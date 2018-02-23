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

namespace Matrix.Extensions.Tests.Client
{  
    using Xunit;

    using Matrix.Extensions.Client.Roster;
    using System.Threading.Tasks;

    public class RosterExtensionsTests
    {
        private readonly EchoClient echoClient = new EchoClient();

        [Fact]
        public async Task Request_Full_RosterTest()
        {
            string expectedResult = "<iq xmlns='jabber:client' type='get' id='foo'><query xmlns='jabber:iq:roster'/></iq>";

            var rosterIq = await echoClient.RequestRosterAsync();
            rosterIq.Id = "foo";

            // assert
            rosterIq.ShouldBe(expectedResult);
        }

        [Fact]
        public async Task Request_Roster_With_Given_Version_Test()
        {
            string expectedResult = "<iq xmlns='jabber:client' type='get' id='foo'><query xmlns='jabber:iq:roster' ver='99'/></iq>";
                        
            var rosterIq = await echoClient.RequestRosterAsync("99");
            rosterIq.Id = "foo";

            // assert
            rosterIq.ShouldBe(expectedResult);
        }
    }
}
