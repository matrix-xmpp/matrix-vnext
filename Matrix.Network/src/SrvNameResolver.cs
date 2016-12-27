using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using DotNetty.Transport.Bootstrapping;
using Matrix.Network.Dns;

namespace Matrix.Network
{
    public class SrvNameResolver : INameResolver
    {
        const string PrefixSrvClient = "_xmpp-client._tcp.";
        const string PrefixSrvServer = "_xmpp-server._tcp.";

        public bool IsResolved(EndPoint address) => false;

        public async Task<EndPoint> ResolveAsync(EndPoint address)
        {
            var asDns = address as DnsEndPoint;


            var srvRecord = await  LookupSrvRecords(asDns.Host);
            if (srvRecord != null)
            {
                var dnsEndPoint = new DnsEndPoint(srvRecord.Target, srvRecord.Port);
                return await new DefaultNameResolver().ResolveAsync(dnsEndPoint);
            }
            
            return await new DefaultNameResolver().ResolveAsync(address);
        }

        private async Task<SRVRecord> LookupSrvRecords(string host)
        {
            try
            {
                var dnsServers = IPConfigurationInformation.DnsServers;
                if (dnsServers.Count > 0)
                {
                    foreach (var server in dnsServers)
                    {
                        try
                        {
                            var srvRecords = await Resolver.SRVLookup(PrefixSrvClient + host, server);
                            if (srvRecords.Count > 0)
                            {
                                return PickSrvRecord(srvRecords);
                            }
                            break;
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        private SRVRecord PickSrvRecord(List<SRVRecord> records)
        {
            SRVRecord ret;
            IEnumerable<SRVRecord> srv = records.OrderBy(s => s.Priority);

            // minServer is the servers with the lowest priority,
            // we can have multiple minServer when there is more than 1 server
            // with the lowest priority
            IEnumerable<SRVRecord> minServers = srv.Where(s1 => s1.Priority == srv.Min(s => s.Priority));

            int minServersCount = minServers.Count();
            if (minServersCount > 1)
            {
                int rnd;
                // summarize the weight of all min servers
                int sumWeight = minServers.Sum(s => s.Weight);

                // eg collecta has a weight on 0 on all servers
                if (sumWeight == 0)
                {
                    // no weight, pick random
                    rnd = new Random().Next(0, minServersCount);
                    ret = minServers.ElementAt(rnd);
                }
                else
                {

                    // Create a random value between 1 - total Weight
                    rnd = new Random().Next(1, sumWeight);

                    int count = 0;
                    SRVRecord result =
                        minServers.First(s2 => rnd > ((count += s2.Weight) - s2.Weight) && rnd <= count);

                    ret = result;
                }
            }
            else
                ret = minServers.First();

            return ret;
        }
    }
}
