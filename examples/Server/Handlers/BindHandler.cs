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
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Matrix;
using Matrix.Network.Handlers;
using Matrix.Xml;
using Matrix.Xmpp;
using Matrix.Xmpp.Bind;
using Matrix.Xmpp.Stream;
using Matrix.Xmpp.Client;

namespace Server.Handlers
{
    public class BindHandler : XmppStanzaHandler, IStreamFeature
    {
        public void AddStreamFeatures(ServerConnectionHandler serverSession, StreamFeatures features)
        {
           if (serverSession.SessionState < SessionState.Binded)
                features.Add(new Bind());
        }
        public BindHandler()
        {
            Handle(
                el =>
                    el.OfType<Iq>()
                    && el.Cast<Iq>().Type == IqType.Set
                    && el.Cast<Iq>().Query.OfType<Bind>(),

                async (context, xmppXElement) =>
                {
                    await ProcessStanza(context, xmppXElement.Cast<Iq>());
                    context.Channel.Pipeline.Remove(this);
                });
        }
        bool SecureResource { get; set; }

        public async Task ProcessStanza(IChannelHandlerContext context, Iq iq)
        {
            var bind = iq.Query as Bind;

            // read desired resource
            string res = bind.Resource;
            if (String.IsNullOrEmpty(res)
                || SecureResource)
            {
                // no resource given, assign random
                res = Guid.NewGuid().ToString();
            }

            var serverSession = context.Channel.Pipeline.Get<ServerConnectionHandler>();

            var jid = new Jid(serverSession.User, serverSession.XmppDomain, res);
            var resIq = new BindIq
            {
                Id = iq.Id,
                Type = Matrix.Xmpp.IqType.Result,
                Bind = { Jid = jid }
            };

            await SendAsync(resIq);

            serverSession.Resource = res;
            serverSession.SessionState = SessionState.Binded;          
        }
    }
}
