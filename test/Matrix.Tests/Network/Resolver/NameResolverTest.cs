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

using Matrix.Network.Resolver;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Matrix.Tests.Network.Resolver
{
    public class NameResolverTest
    {
        [Fact]
        public async Task Given_Ip_And_Port_Resolves_To_EndPoint()
        {         
            var ep = await new NameResolver().ResolveAsync(new DnsEndPoint("127.0.0.1", 5222)) as IPEndPoint;

            Assert.Equal(ep.Address.ToString(), "127.0.0.1");
            Assert.Equal(ep.Port, 5222);
        }
    }
}
