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

using Matrix;
using Matrix.Network.Handlers;
using Matrix.Xml;
using Matrix.Xmpp;
using Matrix.Xmpp.Stream;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Session;

namespace Server.Handlers
{
    public class SessionHandler : XmppStanzaHandler, IStreamFeature
    {
        public void AddStreamFeatures(ServerConnectionHandler serverSession, StreamFeatures features)
        {
            if (serverSession.SessionState < SessionState.Binded)
                features.Add(new Session());
        }

        public SessionHandler()
        {
            Handle(
                el =>
                    el.OfType<Iq>()
                    && el.Cast<Iq>().Type == IqType.Set
                    && el.Cast<Iq>().Query.OfType<Session>(),

                async (context, xmppXElement) =>
                {
                    /*            
                        <iq type="set" id="aabca" >
                            <session xmlns="urn:ietf:params:xml:ns:xmpp-session"/>
                        </iq>            
                     */
                    await SendAsync(new Iq { Id = xmppXElement.Cast<Iq>().Id, Type = IqType.Result });
                    context.Channel.Pipeline.Remove(this);
                });
        }
    }
}
