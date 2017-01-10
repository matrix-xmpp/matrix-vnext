using System.Net;
using System.Threading.Tasks;
using DotNetty.Transport.Bootstrapping;

namespace Matrix.Network.Resolver
{
    public class StaticNameResolver : INameResolver
    {
        public StaticNameResolver(IPAddress ip, int port = 5222)
        {
            Ip = ip;
            Port = port;
        }

        public bool IsResolved(EndPoint address) => !(address is DnsEndPoint);

        public int Port { get; set; }
        public IPAddress Ip { get; set; }

        public async Task<EndPoint> ResolveAsync(EndPoint address)
        {
            var asDns = address as DnsEndPoint;
            if (asDns != null)
            {
                return new IPEndPoint(Ip, Port);
            }
            
            return address;
        }
    }
}
