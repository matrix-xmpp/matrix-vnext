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

namespace Matrix.Extensions.Client
{
    using Matrix.Xmpp.Client;
    using Network.Handlers;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A XmppStanzaHandler which implements the <see cref="IClientIqSender"/> interface.
    /// This allows us to send packets from a stanza handler with the helper in Matrix.Extensions
    /// </summary>
    public abstract class XmppClientStanzaHandler : XmppStanzaHandler, IClientIqSender
    {
        public async Task<Iq> SendIqAsync(Iq iq)
        {
            return await SendIqAsync(iq, XmppStanzaHandler.DefaultTimeout, CancellationToken.None);
        }

        public async Task<Iq> SendIqAsync(Iq iq, int timeout)
        {
            return await SendIqAsync(iq, timeout, CancellationToken.None);
        }

        public async Task<Iq> SendIqAsync(Iq iq, CancellationToken cancellationToken)
        {
            return await SendIqAsync(iq, XmppStanzaHandler.DefaultTimeout, cancellationToken);
        }

        public async Task<Iq> SendIqAsync(Iq iq, int timeout, CancellationToken cancellationToken)
        {
            return await SendIqAsync<Iq>(iq, timeout, cancellationToken);
        }
    }
}