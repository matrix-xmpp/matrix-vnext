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

using System.Linq;
using Matrix.Network.Handlers;
using Matrix.Xml;
using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Roster;

namespace Server.Handlers
{
    public class RosterHandler : XmppStanzaHandler
    {
        public RosterHandler()
        {
            Handle(
                el =>
                    el.OfType<Iq>()
                    && el.Cast<Iq>().Type == IqType.Get
                    && el.Cast<Iq>().Query.OfType<Roster>(),

                async (context, xmppXElement) =>
                {
                    var iq = xmppXElement.Cast<Iq>();

                    var resIq = new RosterIq();
                    resIq.Id = iq.Id;
                    resIq.To = iq.From;
                    resIq.Type = IqType.Result;

                    // TODO populate/store the roster from a database
                    for (int i = 0; i < 10; i++)                    
                        resIq.Roster.AddRosterItem(new RosterItem($"user{i}@localhost", $"user {i}"));                 

                    await SendAsync(resIq);
                });
        }
    }
}
