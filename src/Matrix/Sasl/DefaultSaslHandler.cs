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

using System.Threading.Tasks;
using Matrix.Sasl.Digest;
using Matrix.Sasl.Plain;
using Matrix.Sasl.Scram;
using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using System.Threading;

namespace Matrix.Sasl
{
    public class DefaultSaslHandler : IAuthenticate
    {
        public async Task<XmppXElement> AuthenticateAsync(Mechanisms mechanisms, XmppClient xmppClient, CancellationToken cancellationToken)
        {
            ISaslProcessor saslProc = null;
            if (mechanisms.SupportsMechanism(SaslMechanism.ScramSha1))
                saslProc = new ScramSha1Processor();

            else if (mechanisms.SupportsMechanism(SaslMechanism.DigestMd5))
                saslProc = new DigestMd5Processor();

            else if (mechanisms.SupportsMechanism(SaslMechanism.Plain))
                saslProc = new PlainProcessor();

            return await saslProc.AuthenticateClientAsync(xmppClient, cancellationToken);
        }
    }
}
