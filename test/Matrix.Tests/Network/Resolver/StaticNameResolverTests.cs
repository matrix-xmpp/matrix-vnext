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

using Matrix.Network.Resolver;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Matrix.Tests.Network.Resolver
{
    public class StaticNameResolverTests
    {
        [Fact]
        public async Task Given_Ip_And_Port_Resolves_To_EndPoint()
        {
            IPAddress givenIp = IPAddress.Parse("127.0.0.1");
            int givenPort = 9999;
            var resolver = new StaticNameResolver(givenIp, givenPort);
            var ep = await resolver.ResolveAsync(new DnsEndPoint("1", 1)) as IPEndPoint;

            Assert.Equal(ep.Address, givenIp);
            Assert.Equal(ep.Port, givenPort);
        }

        [Fact]
        public async Task Given_Ip_Only_Should_Default_To_Port_5222()
        {
            IPAddress givenIp = IPAddress.Parse("127.0.0.1");
            
            var resolver = new StaticNameResolver(givenIp);
            var ep = await resolver.ResolveAsync(new DnsEndPoint("1", 1)) as IPEndPoint;

            Assert.Equal(ep.Address, givenIp);
            Assert.Equal(ep.Port, 5222);
        }

        [Fact]
        public async Task Given_Hostname_Should_Resolve_Ip_5222()
        {
            IPAddress expectedIp = IPAddress.Parse("8.8.8.8");

            var resolver = new StaticNameResolver("google-public-dns-a.google.com");
            var ep =await  resolver.ResolveAsync(new DnsEndPoint("1", 1)) as IPEndPoint;

            Assert.Equal(ep.Address, expectedIp);
            Assert.Equal(ep.Port, 5222);
        }

        [Fact]
        public void Direct_Tls_Should_Be_True()
        {
            var resolver = new StaticNameResolver("example.com", 5222, true);
            Assert.Equal(resolver.DirectTls, true);
        }

        [Fact]
        public void Direct_Tls_Should_Be_False()
        {
            var resolver = new StaticNameResolver("example.com", 5222);
            Assert.Equal(resolver.DirectTls, false);

            var resolver2 = new StaticNameResolver("example.com", 5222, false);
            Assert.Equal(resolver2.DirectTls, false);
        }
    }
}
