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
using System.Text;
using System.Threading.Tasks;
using Matrix.Idn;
using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using System.Threading;

namespace Matrix.Sasl.Scram
{
    /// <summary>
    /// XMPP implementation of SCRAM SHA-1 SASL
    /// </summary>
    public class ScramSha1Processor : ISaslProcessor
    {
        /// <inheritdoc/>
        public async Task<XmppXElement> AuthenticateClientAsync(XmppClient xmppClient, CancellationToken cancellationToken)
        {
            var scramHelper = new ScramHelper();

            var username = xmppClient.Username;
#if STRINGPREP
           var  password = StringPrep.SaslPrep(xmppClient.Password);
#else
            var password = xmppClient.Password;
#endif

            string msg = ToB64String(scramHelper.GenerateFirstClientMessage(username));
            var authMessage = new Auth(SaslMechanism.ScramSha1, msg);

            var ret1 = await xmppClient.SendAsync<Failure, Challenge>(authMessage, cancellationToken);

            if (ret1 is Challenge)
            {
                var resp = GenerateFinalMessage(ret1 as Challenge, scramHelper, password);
                var ret2 = await xmppClient.SendAsync<Failure, Success>(resp, cancellationToken);

                return ret2;
            }

            return ret1;
        }

        private Response GenerateFinalMessage(Challenge ch, ScramHelper scramHelper, string password)
        {
            byte[] b = ch.Bytes;
            string firstServerMessage = Encoding.UTF8.GetString(b, 0, b.Length);
            string clientFinalMessage = scramHelper.GenerateFinalClientMessage(firstServerMessage, password);
            return new Response(ToB64String(clientFinalMessage));
        }

        private static string ToB64String(string sin)
        {
            byte[] msg = Encoding.UTF8.GetBytes(sin);
            return Convert.ToBase64String(msg, 0, msg.Length);
        }

        private string FromB64String(string sin)
        {
            var b = Convert.FromBase64String(sin);
            return Encoding.UTF8.GetString(b, 0, b.Length);
        }
    }
}
