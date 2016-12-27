using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace Matrix.Network.Dns
{

    /// <summary>
	/// Summary description for IPConfigurationInformation.
	/// </summary>
	internal class IPConfigurationInformation
    {
        private static List<IPAddress> publicDnsServers = new List<IPAddress>
        {
            IPAddress.Parse("8.8.8.8"), // Google 1
            IPAddress.Parse("8.8.4.4"), // Google 2
        };
        
        public static List<IPAddress> DnsServers => FindDnsByDotNet();

        /// <summary>
		/// Finds the dns servers with build in .NET functions. is not available on all platforms.
		/// And also does not work on all platforms. Eg. Xamarin
		/// </summary>
		/// <returns>The dns by dot net.</returns>
		private static List<IPAddress> FindDnsByDotNet()
		{
            var ret = new List<IPAddress>();

            var adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface networkInterface in
                adapters
                    .Where(p => p.OperationalStatus == OperationalStatus.Up
                    && p.NetworkInterfaceType != NetworkInterfaceType.Loopback))
            {
                foreach (IPAddress dnsAddress in networkInterface
                    .GetIPProperties()
                    .DnsAddresses
                    .Where(i =>
                        i.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork
                        || i.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6))
                {
                    ret.Add(dnsAddress);
                }
            }

		    if (ret.Count == 0)
		        return publicDnsServers;

            return ret;
		}
    }
}