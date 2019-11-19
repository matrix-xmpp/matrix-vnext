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

using Xunit;
using Shouldly;
using DotNetty.Transport.Bootstrapping;

namespace Matrix.Srv.Tests
{
    public class XmppClientTests
    {
        //[Fact]
        public void TestSrvResolverWithSingleRecord()
        {
            var dnsEndPoint = new System.Net.DnsEndPoint("xmpp.ag-software.net", 5222);
            var ep = new DefaultNameResolver().ResolveAsync(dnsEndPoint).GetAwaiter().GetResult();

            var resolver = new SrvNameResolver();
            var ep2 = resolver.ResolveAsync(new System.Net.DnsEndPoint("ag-software.net", 80)).GetAwaiter().GetResult();
            
            // assert
            resolver.DirectTls.ShouldBe(false);
            ep2.ToString().ShouldBe(ep.ToString());
        }

        [Fact]
        public void BuildQueryTest()
        {
            const string PrefixClient = "_xmpp-client._tcp.";
            const string PrefixClientSecure = "_xmpps-client._tcp.";

            const string PrefixServer = "_xmpp-server._tcp.";
            const string PrefixServerSecure = "_xmpps-server._tcp.";

            var srvResolver = new SrvNameResolver();
            srvResolver.BuildQuery("example.com", false, true).ShouldBe(PrefixClient + "example.com");
            srvResolver.BuildQuery("example.com", false, false).ShouldBe(PrefixServer + "example.com");

            srvResolver.BuildQuery("example.com", true, true).ShouldBe(PrefixClientSecure + "example.com");
            srvResolver.BuildQuery("example.com", true, false).ShouldBe(PrefixServerSecure + "example.com");
        }       
    }
}
