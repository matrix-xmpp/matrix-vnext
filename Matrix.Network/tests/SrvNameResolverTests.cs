using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Matrix.Network.Tests
{
    [TestClass]
    public class SrvNameResolverTests
    {
        [TestMethod]
        public void ResoveTest()
        {
            SrvNameResolver resolv = new SrvNameResolver();
            var ep = resolv.ResolveAsync(new DnsEndPoint("ag-software.net", 5222)).GetAwaiter().GetResult() as IPEndPoint;
            ep.Address.ToString().ShouldBe("54.227.205.19");
        }
    }
}
