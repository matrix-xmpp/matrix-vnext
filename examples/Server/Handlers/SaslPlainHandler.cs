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

using System;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Matrix.Network.Handlers;
using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using Matrix.Xmpp.Stream;

namespace Server.Handlers
{
    public class SaslPlainHandler : XmppStanzaHandler, IStreamFeature
    {
        public void AddStreamFeatures(ServerConnectionHandler serverSession, StreamFeatures features)
        {
            if (serverSession.SessionState < Matrix.SessionState.Authenticated)
            {
                if (features.Mechanisms == null)
                    features.Mechanisms = new Mechanisms();

                features.Mechanisms.AddMechanism(SaslMechanism.Plain);
            }
        }
        public SaslPlainHandler()
        {
            Handle(
                el => el.OfType<Auth>()
                      && el.Cast<Auth>().SaslMechanism == SaslMechanism.Plain
                      ,
                  async (context, xmppXElement) =>
                {
                    var auth = xmppXElement.Cast<Auth>();
                    await ProcessSaslPlainAuth(context, auth);
                    context.Channel.Pipeline.Remove(this);
                });
        }

        private async Task ProcessSaslPlainAuth(IChannelHandlerContext context, Auth auth)
        {
            string pass = null;
            string user = null;

            byte[] bytes = Convert.FromBase64String(auth.Value);
            string sasl = Encoding.UTF8.GetString(bytes);
            // trim nullchars
            sasl = sasl.Trim((char)0);
            string[] split = sasl.Split((char)0);

            if (split.Length == 3)
            {
                user = split[1];
                pass = split[2];
            }
            else if (split.Length == 2)
            {
                user = split[0];
                pass = split[1];
            }

            string dbPass = "secret"; // TODO // Server.Storage.GetPassword(serverSession.XmppDomain, user);
            if (dbPass == null || pass != dbPass)
            {
                // user does not exist or wrong password
                await SendAsync(new Failure(FailureCondition.NotAuthorized));
            }
            else if (pass == dbPass)
            {
                var serverSession = context.Channel.Pipeline.Get<ServerConnectionHandler>();
                // pass correct
                serverSession.User = user;

                // stream reset
                serverSession.ResetStream();

                serverSession.SessionState = Matrix.SessionState.Authenticated;
                await SendAsync(new Success());
            }
        }
    }
}
