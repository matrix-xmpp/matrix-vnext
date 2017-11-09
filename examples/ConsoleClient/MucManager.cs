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

namespace ConsoleClient
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Matrix;
    using Matrix.Xml;
    using Matrix.Xmpp.Client;
    using Matrix.Xmpp;
    using Matrix.Xmpp.Muc;
    using Matrix.Xmpp.Muc.Owner;
    using Matrix.Xmpp.XData;

    /// <summary>
    /// Small helper class for group chat (aka MUC)
    /// </summary>
    public class MucManager    {

        public MucManager(XmppClient xmppClient)
        {
            XmppClient = xmppClient;
        }

        public XmppClient XmppClient { get; internal set; }

        public async Task<Iq> RequestRoomConfigurationAsync(Jid room)
        {
            var iq = new OwnerIq { Type = IqType.Get, To = room };
            return await XmppClient.SendIqAsync(iq);
        }

        public async Task<Iq> SubmitRoomConfigurationAsync(Jid room, Data xdata)
        {
            var iq = new OwnerIq { Type = IqType.Set, To = room, OwnerQuery = { XData = xdata } };
            return await XmppClient.SendIqAsync(iq);
        }

        public async Task<XmppXElement> EnterRoomAsync(Jid jid, string nick)
        {
            var createRoomStanza = CreateEnterRoomStanza(jid, nick, null, false, null);

            Func<XmppXElement, bool> predicate = e =>
                e.OfType<Presence>()
                && e.Cast<Presence>().From.Equals(jid, new BareJidComparer());

            return await XmppClient.SendAsync(createRoomStanza, predicate, 10000, CancellationToken.None);
        }

        public async Task<XmppXElement> ExitRoomAsync(Jid jid, string nick)
        {
            var exitRoomStanza = CreateExitRoomStanza(jid, nick);

            Func<XmppXElement, bool> predicate = e =>
                e.OfType<Presence>()
                && e.Cast<Presence>().From.Equals(jid, new BareJidComparer());

            return await XmppClient.SendAsync(exitRoomStanza, predicate, 10000, CancellationToken.None);
        }

        public Presence CreateEnterRoomStanza(Jid room, string nickname, string password = null, bool disableHistory = false, History history = null)
        {           
            var to = new Jid(room.ToString())
            {
                Resource = nickname
            };

            var pres = new Presence
            {
                To = to
            };

            var x = new X();
            if (password != null)
                x.Password = password;

            if (disableHistory)
            {
                var hist = new History { MaxCharacters = 0 };
                x.History = hist;
            }

            if (history != null)
                x.History = history;

            pres.Add(x);

            return pres;
        }

        public Presence CreateExitRoomStanza(Jid room, string nickname)
        {
            var to = new Jid(room.ToString())
            {
                Resource = nickname
            };

            var pres = new Presence
            {
                To = to,
                Type = PresenceType.Unavailable
            };

            return pres;
        }
    }
}
