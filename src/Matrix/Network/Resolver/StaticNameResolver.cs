/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
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
            return await Task<EndPoint>.Factory.StartNew(() =>
            {
                var asDns = address as DnsEndPoint;
                if (asDns != null)
                {
                    return new IPEndPoint(Ip, Port);
                }

                return address;
            });
        }
    }
}
