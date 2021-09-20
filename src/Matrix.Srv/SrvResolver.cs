namespace Matrix.Srv
{
    using DnsClient;
    using DnsClient.Protocol;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SrvResolver
    {
        public async Task<Uri> ResolveClientUriAsync(string xmppDomain)
        {
            return await ResolveEndPointAsync(xmppDomain).ConfigureAwait(false);
        }

        public async Task<Uri> ResolveServerUriAsync(string xmppDomain)
        {
            return await ResolveEndPointAsync(xmppDomain, false).ConfigureAwait(false);
        }

        private async Task<Uri> ResolveEndPointAsync(string xmppDomain, bool isClient = true)
        {
            // try first to lookup direct TLS records
            var srvRecord = await LookupSrvRecords(xmppDomain, true, isClient).ConfigureAwait(false);
            if (srvRecord != null)
            {
                return new Uri($"{Schemes.Tcps}://{srvRecord.Target}:{srvRecord.Port}");
            }

            // if no direct tls resources found, fall back to starts tls records
            srvRecord = await LookupSrvRecords(xmppDomain, false, isClient).ConfigureAwait(false);
            if (srvRecord != null)
            {
                return new Uri($"{Schemes.Tcps}://{srvRecord.Target}:{srvRecord.Port}");
            }


            return new Uri($"{Schemes.Tcp}://{xmppDomain}:5222");
        }

        /// <summary>
        /// Build the query part of the Srv request
        /// </summary>
        /// <param name="host">The hostname</param>
        /// <param name="directTls">defined whether direct Tls should be used or not</param>
        /// <param name="isClient">defined whether this should be a client or server query</param>
        /// <returns></returns>
        public string BuildQuery(string host, bool directTls, bool isClient)
        {
            return $"_{(directTls ? "xmpps" : "xmpp")}-{(isClient ? "client" : "server")}._tcp.{host}";           
        }

        private async Task<SrvRecord> LookupSrvRecords(string host, bool directTls = false, bool isClient = true)
        {
            try
            {               
                var lookup = new LookupClient();              

                string query = BuildQuery(host, directTls, isClient);

                var result = await lookup.QueryAsync(query, QueryType.SRV).ConfigureAwait(false);
                var srvRecords = result.Answers.SrvRecords().ToList();

                if (srvRecords.Count > 0)
                {
                    return PickSrvRecord(srvRecords);
                }            
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }

        /// <summary>
        /// Picks all records which the lowest priority. There can be more than just one.
        /// </summary>
        /// <param name="records"></param>
        /// <returns></returns>
        private IEnumerable<SrvRecord> GetMinServers(List<SrvRecord> records)
        {
            var minPrio = records.Min(s => s.Priority);
            return records
                        .Where(s1 => s1.Priority == minPrio);
        }

        private SrvRecord PickSrvRecord(List<SrvRecord> records)
        {
            SrvRecord ret;
            
            List<SrvRecord> minServers = GetMinServers(records).ToList();

            int minServersCount = minServers.Count;
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
                    SrvRecord result =
                        minServers.First(s2 => rnd > (count += s2.Weight) - s2.Weight && rnd <= count);

                    ret = result;
                }
            }
            else
            {
                ret = minServers.First();
            }

            return ret;
        }
    }
}
