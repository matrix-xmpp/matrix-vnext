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
using System.Text;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Matrix.Network.Handlers;
using Matrix.Sasl.Scram;
using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using Matrix.Xmpp.Stream;

namespace Server.Handlers
{
    public class SaslScramHandler : XmppStanzaHandler, IStreamFeature
    {
        ScramHelper scramHelper = new ScramHelper();

        public void AddStreamFeatures(ServerConnectionHandler serverSession, StreamFeatures features)
        {
            if (serverSession.SessionState < Matrix.SessionState.Authenticated)
            {
                if (features.Mechanisms == null)
                    features.Mechanisms = new Mechanisms();

                features.Mechanisms.AddMechanism(SaslMechanism.ScramSha1);
            }
        }
        public SaslScramHandler()
        {
            Handle(
                el => el.OfType<Auth>()
                      && el.Cast<Auth>().SaslMechanism == SaslMechanism.ScramSha1
                      ,
                  async (context, xmppXElement) =>
                  {
                      var auth = xmppXElement.Cast<Auth>();
                      await ProcessSaslFirstClientMessage(context, auth);
                      context.Channel.Pipeline.Remove(this);
                  });
        }

        private async Task ProcessSaslFirstClientMessage(IChannelHandlerContext context, Auth auth)
        {
            /*
             * parse the first client message and return teh first server message embedded in a <challenge/> tag
             * <challenge xmlns="urn:ietf:params:xml:ns:xmpp-sasl">aXRobT1tZDUtc2Vzcw==</challenge>
             */
            string firstServerMessage;
            var serverSession = context.Channel.Pipeline.Get<ServerConnectionHandler>();
            try
            {
                string firstClientmessage = Encoding.UTF8.GetString(auth.Bytes);
                string username = scramHelper.GetUserFromFirstClientMessage(firstClientmessage);
                string passwordPlain = "secret"; // TODO add interface
                firstServerMessage = scramHelper.GenerateFirstServerMessage(firstClientmessage, passwordPlain);

                serverSession.User = username;
            }
            catch (Exception)
            {
                // something went wrong
                await SendAsync(new Failure
                {
                    Condition = FailureCondition.NotAuthorized
                });
                return;
            }

            // Part 2
            var response = await SendAsync<Response>(new Challenge { Bytes = Encoding.UTF8.GetBytes(firstServerMessage) });

            string serverFinalMessage;
            try
            {
                string finalClientMessage = Encoding.UTF8.GetString(response.Bytes);
                serverFinalMessage = scramHelper.GenerateFinalServerMessage(finalClientMessage);
            }
            catch (Exception)
            {
                // something went wrong
                await SendAsync(new Failure { Condition = FailureCondition.NotAuthorized });
                return;
            }

            if (serverFinalMessage != null)
            {
                // success
                serverSession.SessionState = Matrix.SessionState.Authenticated;
                serverSession.ResetStream();
                await SendAsync(new Success { Bytes = Encoding.UTF8.GetBytes(serverFinalMessage) });
            }
            else
            {
                // auth failure
                await SendAsync(new Failure
                {
                    Condition = FailureCondition.NotAuthorized,
                    Text = "The response provided by the client doesn't match the one we calculated."
                });
            }
        }
    }
}
