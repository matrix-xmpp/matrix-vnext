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

using System.Net;
using System.Threading.Tasks;
using DotNetty.Transport.Bootstrapping;

namespace Matrix.Network.Resolver
{
    /// <summary>
    /// Represents a resolver which allow us to pass the hostname or ip address
    /// of the server, in cases where the host does not match the XMPP domain,
    /// or no SRV records exist for automatic lookups in DNS.
    /// </summary>
    public class StaticNameResolver : INameResolver, IDirectTls
    {
        /// <summary>
        /// Ititialize a new <see cref="StaticNameResolver"/> with a given ip address and port number
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="directTls">direct Tls usage vs StartTls</param>
        public StaticNameResolver(IPAddress ip, int port = 5222, bool directTls = false)
        {
            Ip = ip;
            Port = port;
            DirectTls = directTls;
        }

        /// <summary>
        /// Ititialize a new <see cref="DnsNameResolver"/> with a given hostname and port number
        /// </summary>
        /// <param name="hostname">The hostname</param>
        /// <param name="port">The port number</param>
        /// <param name="directTls">direct Tls usage vs StartTls</param>
        public StaticNameResolver(string hostname, int port = 5222, bool directTls = false)
        {
            Hostname = hostname;
            Port = port;
            DirectTls = directTls;
        }

        /// <inheritdoc />        
        public bool IsResolved(EndPoint address) => !(address is DnsEndPoint);
        
        public IPAddress Ip { get; set; }

        /// <summary>
        /// Gets or sets the hostname
        /// </summary>
        public string Hostname { get; private set; }

        /// <summary>
        /// Gets or sets the port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets whether direct Tls should be used, versus StartTls
        /// </summary>
        public bool DirectTls { get; private set; }

        /// <inheritdoc />
        public async Task<EndPoint> ResolveAsync(EndPoint address)
        {
            return await Task<EndPoint>.Run(async () =>
            {
                var asDns = address as DnsEndPoint;
                if (asDns != null)
                {
                    if (Ip == null)
                        await ResolveIpFromHostnameAsync();

                    return new IPEndPoint(Ip, Port);
                }

                return address;
            });
        }

        private async Task ResolveIpFromHostnameAsync()
        {
            var addresses = await Dns.GetHostAddressesAsync(Hostname);
            if (addresses.Length > 0)
                Ip = addresses[0];
        }
    }
}
