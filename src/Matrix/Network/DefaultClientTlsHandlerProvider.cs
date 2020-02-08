/*
 * Copyright (c) 2003-2020 by AG-Software <info@ag-software.de>
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

using DotNetty.Handlers.Tls;
using DotNetty.Transport.Channels;

using System.Net.Security;
using System.Threading.Tasks;

namespace Matrix.Network
{
    public class DefaultClientTlsHandlerProvider : ITlsHandlerProvider
    {
        /// <summary>
        /// A default implementation of a <see cref="ITlsHandlerProvider"/>.
        /// </summary>
        public async Task<IChannelHandler> ProvideAsync(XmppClient xmppClient)
        {
            return await Task<IChannelHandler>.Run(async() =>
            {
                var tlsSettings = await xmppClient.TlsSettingsProvider.ProvideAsync(xmppClient);
                var tlsHandler = new TlsHandler(stream
                        => new SslStream(stream,
                            true,
                            (sender, certificate, chain, errors) =>
                                xmppClient.CertificateValidator.RemoteCertificateValidationCallback(sender, certificate,
                                    chain, errors)),
                    tlsSettings);

                return tlsHandler;
            });
        }
    }
}
