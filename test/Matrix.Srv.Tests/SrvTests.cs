namespace Matrix.Srv.Tests
{
    using Xunit;
    using Shouldly;

    public class XmppClientTests
    {
        // [Fact]
        //public void TestSrvResolverWithSingleRecord()
        //{
        //    var dnsEndPoint = new System.Net.DnsEndPoint("xmpp.ag-software.net", 5222);
        //    var ep = new DefaultNameResolver().ResolveAsync(dnsEndPoint).GetAwaiter().GetResult();

        //    var resolver = new SrvNameResolver();
        //    var ep2 = resolver.ResolveAsync(new System.Net.DnsEndPoint("ag-software.net", 80)).GetAwaiter().GetResult();
            
        //    // assert
        //    resolver.DirectTls.ShouldBe(false);
        //    ep2.ToString().ShouldBe(ep.ToString());
        //}

        //[Fact]
        //public void BuildQueryTest()
        //{
        //    const string PrefixClient = "_xmpp-client._tcp.";
        //    const string PrefixClientSecure = "_xmpps-client._tcp.";

        //    const string PrefixServer = "_xmpp-server._tcp.";
        //    const string PrefixServerSecure = "_xmpps-server._tcp.";

        //    var srvResolver = new SrvNameResolver();
        //    srvResolver.BuildQuery("example.com", false, true).ShouldBe(PrefixClient + "example.com");
        //    srvResolver.BuildQuery("example.com", false, false).ShouldBe(PrefixServer + "example.com");

        //    srvResolver.BuildQuery("example.com", true, true).ShouldBe(PrefixClientSecure + "example.com");
        //    srvResolver.BuildQuery("example.com", true, false).ShouldBe(PrefixServerSecure + "example.com");
        //}       
    }
}
