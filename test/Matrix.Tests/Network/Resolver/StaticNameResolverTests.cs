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
    }
}
