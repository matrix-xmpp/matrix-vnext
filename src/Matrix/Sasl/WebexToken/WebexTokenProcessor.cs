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
using System.Threading;
using System.Threading.Tasks;

using Matrix.Sasl;
using Matrix.Xml;

namespace Matrix.Xmpp.Sasl.Processor.WebexToken
{
    /// <summary>
    /// XMPP implementation of CISCO Webex TOKEN SASL
    /// </summary>
    public class WebexTokenProcessor : ISaslProcessor
    {
        string AccessToken { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebexTokenProcessor"/> class.
        /// </summary>
        /// <param name="accessToken">The token</param>
        public WebexTokenProcessor(string accessToken)
        {
            this.AccessToken = accessToken;
        }

        /// <inheritdoc/>
        public async Task<XmppXElement> AuthenticateClientAsync(XmppClient xmppClient, CancellationToken cancellationToken)
        {
            var authMessage = new Auth(SaslMechanism.WebexToken, GetMessage());

            return
                await xmppClient.SendAsync<Success, Failure>(authMessage, cancellationToken);
        }      
   
        internal string GetMessage()
        {
            // TODO, check if this is correct
            var sb = new StringBuilder();
           
            sb.Append(AccessToken);

            var msg = Encoding.UTF8.GetBytes(sb.ToString());
            return Convert.ToBase64String(msg, 0, msg.Length);
        }
    }
}