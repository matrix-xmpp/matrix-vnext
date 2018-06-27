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

using System.Text;
using System.Threading.Tasks;
using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using System.Threading;

namespace Matrix.Sasl.Digest
{
    /// <summary>
    /// XMPP implementation of DIGEST MD5 SASL
    /// </summary>
    public class DigestMd5Processor : ISaslProcessor
    {
        /// <inheritdoc/>
        public async Task<XmppXElement> AuthenticateClientAsync(XmppClient xmppClient, CancellationToken cancellationToken)
        {
            var authMessage = new Auth(SaslMechanism.DigestMd5);

            var ret1 = await xmppClient.SendAsync<Failure, Challenge>(authMessage, cancellationToken);

            if (ret1 is Challenge)
            {
                var ret2 = await HandleChallenge(ret1 as Challenge, xmppClient, cancellationToken);
                if (ret2 is Success)
                    return ret2;

                if (ret2 is Challenge)
                    return await HandleChallenge(ret2 as Challenge, xmppClient, cancellationToken);

                return ret2;
            }
            
            return ret1;
        }

        public async Task<XmppXElement> HandleChallenge(Challenge ch, XmppClient xmppClient, CancellationToken cancellationToken)
        {
            byte[] bytes = ch.Bytes;
            string s = Encoding.ASCII.GetString(bytes, 0, bytes.Length);

            var step1 = new Step1(s);

            if (step1.Rspauth == null)
            {
                var s2 = new Step2(step1, xmppClient);

                string message = s2.GetMessage();
                byte[] b = Encoding.UTF8.GetBytes(message);

                return await xmppClient.SendAsync<Failure, Challenge, Success>(new Response {Bytes = b}, cancellationToken);
            }

            return await xmppClient.SendAsync<Failure, Success>(new Response(), cancellationToken);
        }
    }
}
