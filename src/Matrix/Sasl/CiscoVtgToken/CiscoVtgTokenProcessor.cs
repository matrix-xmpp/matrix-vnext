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

using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Matrix.Sasl.Processor.CiscoVtgToken
{
    /// <summary>
    /// XMPP implementation of CISCO VTG TOKEN SASL
    /// </summary>
    public class CiscoVtgTokenProcessor : ISaslProcessor
    {
        string AccessToken { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CiscoVtgTokenProcessor"/> class.
        /// </summary>
        /// <param name="accessToken">The token</param>
        public CiscoVtgTokenProcessor(string accessToken)
        {
            this.AccessToken = accessToken;
        }

        /// <inheritdoc/>
        public async Task<XmppXElement> AuthenticateClientAsync(XmppClient xmppClient, CancellationToken cancellationToken)
        {
            var authMessage = new Auth(SaslMechanism.CiscoVtgToken, GetMessage(xmppClient));

            return
                await xmppClient.SendAsync<Success, Failure>(authMessage, cancellationToken);
        }

        internal string GetMessage(XmppClient xmppClient)
        {
            // Base64(userid=user@domain, NULL, token=one-time-password)
            // userid=juliet@capulet.com/0/token=2345678
            var sb = new StringBuilder();

            sb.Append("userid=");
            sb.Append(xmppClient.Username);
            sb.Append("@");
            sb.Append(xmppClient.XmppDomain);
            
            sb.Append((char)0);
            
            sb.Append("token=");
            sb.Append(AccessToken);

            var msg = Encoding.UTF8.GetBytes(sb.ToString());
            return Convert.ToBase64String(msg, 0, msg.Length);
        }
    }
}