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


/*
 * There are known issues in Mono where it behaves different than netcore and netstandard.
 * Because this was not addressed in MONO yet the default DotNetty TlsHandler
 * has issues with Mono platforms like Xamarin or Unity.
 *
 * This PR is addressing those issues in DotNetty:
 * https://github.com/yyjdelete/DotNetty/commit/3593c407466d894c811c55471e29e93c4435b11b#diff-163c148e25ee6a01bf7ce7d1db7f7699
 *
 * This package is based on the PR above and provides a fixed TlsHandler implementation for MONO platforms
 *
 */
namespace Matrix.Tls.Mono
{
    using System.Net.Security;
    using System.Threading.Tasks;
    using DotNetty.Transport.Channels;
    using Network;

    public class MonoTlsHandlerProvider : ITlsHandlerProvider
    {
        /// <summary>
        /// A default implementation of a <see cref="ITlsHandlerProvider"/>.
        /// </summary>
        public async Task<IChannelHandler> ProvideAsync(XmppClient xmppClient)
        {
            return await Task<IChannelHandler>.Run(async () =>
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
