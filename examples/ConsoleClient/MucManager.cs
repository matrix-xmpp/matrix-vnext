namespace ConsoleClient
{
    using Matrix;
    using Matrix.Xml;
    using Matrix.Xmpp;
    using Matrix.Xmpp.Client;
    using Matrix.Xmpp.Muc;
    using Matrix.Xmpp.Muc.Admin;
    using Matrix.Xmpp.Muc.Owner;
    using Matrix.Xmpp.XData;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Small helper class for group chat (aka MUC)
    /// </summary>
    public class MucManager
    {
        public MucManager(XmppClient xmppClient)
        {
            XmppClient = xmppClient;
        }

        public XmppClient XmppClient { get; internal set; }

        public async Task<Iq> RequestRoomConfigurationAsync(Jid room)
        {
            var iq = new OwnerIq { Type = IqType.Get, To = room };
            return await XmppClient.SendIqAsync(iq).ConfigureAwait(false);
        }

        public async Task<Iq> SubmitRoomConfigurationAsync(Jid room, Data xdata)
        {
            var iq = new OwnerIq { Type = IqType.Set, To = room, OwnerQuery = { XData = xdata } };
            return await XmppClient.SendIqAsync(iq).ConfigureAwait(false);
        }

        public async Task<XmppXElement> EnterRoomAsync(Jid jid, string nick)
        {
            var createRoomStanza = CreateEnterRoomStanza(jid, nick, null, false, null);

            Func<XmppXElement, bool> predicate = e =>
                e.OfType<Presence>()
                && e.Cast<Presence>().From.Equals(jid, new BareJidComparer());

            return await XmppClient.SendAsync(createRoomStanza, predicate, 10000, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<XmppXElement> ExitRoomAsync(Jid jid, string nick)
        {
            var exitRoomStanza = CreateExitRoomStanza(jid, nick);

            Func<XmppXElement, bool> predicate = e =>
                e.OfType<Presence>()
                && e.Cast<Presence>().From.Equals(jid, new BareJidComparer());

            return await XmppClient.SendAsync(exitRoomStanza, predicate, 10000, CancellationToken.None).ConfigureAwait(false);
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

        //public void RequestMemberList(Jid room)
        //{
        //    RequestList(Affiliation.Member, room, cb, state);
        //}

        //private Iq CreateRequestListStanza(Affiliation affiliation, Jid room)
        //{
        //    var aIq = new AdminIq {To = room, Type = IqType.Get};
        //    aIq.AdminQuery.AddItem(new Matrix.Xmpp.Muc.Admin.Item(affiliation));

        //    return aIq;
        //}
    }
}
