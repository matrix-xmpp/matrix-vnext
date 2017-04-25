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

using DotNetty.Handlers.Tls;
using Matrix.Network.Handlers;
using Matrix.Xml;
using Matrix.Xmpp.Stream;
using Matrix.Xmpp.Tls;

namespace Server.Handlers
{
    public class StartTlsHandler : XmppStanzaHandler, IStreamFeature
    {
        private ServerConnectionHandler ServerSession;
        public void AddStreamFeatures(ServerConnectionHandler serverSession, StreamFeatures features)
        {
            ServerSession = serverSession;
            if (serverSession.SessionState < Matrix.SessionState.Secure)
                features.Add(new StartTls());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StartTlsHandler" /> class.
        /// </summary>
        /// <param name="certificateProvider">The certificate provider.</param>
        public StartTlsHandler(ICertificateProvider certificateProvider)
        {
            Handle(
               el => el.OfType<StartTls>(),

               async (context, xmppXElement) =>
               {                  
                   var serverSession = context.Channel.Pipeline.Get<ServerConnectionHandler>();

                    serverSession.ResetStream();
                   await SendAsync(new Proceed());

                   var certConfig = ServerSettings.Certificate(serverSession.XmppDomain);
                   var tlsCertificate =  certificateProvider.RequestCertificate(serverSession.XmppDomain);
                   context.Channel.Pipeline.AddFirst(TlsHandler.Server(tlsCertificate));

                   serverSession.SessionState = Matrix.SessionState.Secure;
               });         
        }
    }
}
