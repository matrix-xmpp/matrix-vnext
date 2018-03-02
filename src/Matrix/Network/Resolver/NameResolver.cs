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

using System;
using System.Net;
using System.Threading.Tasks;
using DotNetty.Transport.Bootstrapping;

namespace Matrix.Network.Resolver
{
    /// <summary>
    /// Resolves ip addresses from hostnames
    /// </summary>
    public class NameResolver : INameResolver
    {
        /// <inheritdoc/>     
        public bool IsResolved(EndPoint address) => !(address is DnsEndPoint);

        /// <inheritdoc/>        
        public async Task<EndPoint> ResolveAsync(EndPoint address)
        {
            var asDns = address as DnsEndPoint;
            if (asDns != null)
            {
                if (IsIPAddress(asDns.Host))
                {
                    return new IPEndPoint(IPAddress.Parse(asDns.Host), asDns.Port);
                }
                else
                {
                    IPHostEntry resolved = await Dns.GetHostEntryAsync(asDns.Host);
                    return new IPEndPoint(resolved.AddressList[0], asDns.Port);
                }                
            }
            else
            {
                return address;
            }
        }

        private bool IsIPAddress(string host)
        {
            var hostNameType = Uri.CheckHostName(host);
            return hostNameType == UriHostNameType.IPv4 || hostNameType == UriHostNameType.IPv6;
        }
    }
}
