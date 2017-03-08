using Xunit;
using Shouldly;
using DotNetty.Transport.Bootstrapping;

namespace Matrix.Srv.Tests
{
    public class XmppClientTests
    {
        [Fact]
        public void TestSrvResolverWithSingleRecord()
        {
            var dnsEndPoint = new System.Net.DnsEndPoint("xmpp.ag-software.net", 5222);
            var ep = new DefaultNameResolver().ResolveAsync(dnsEndPoint).GetAwaiter().GetResult();

            var resolver = new SrvNameResolver();
            var ep2 = resolver.ResolveAsync(new System.Net.DnsEndPoint("ag-software.net", 80)).GetAwaiter().GetResult();

            ep2.ToString().ShouldBe(ep.ToString());         
        }
    }
}
