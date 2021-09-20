namespace Matrix.Extensions.Client.Roster
{
    using Matrix.Xmpp;
    using Matrix.Xmpp.Client;
    using Matrix.Xmpp.Roster;
    using System;

    public class RosterBuilder
    {
        /// <summary>
        /// Builds a request to retrieve the roster
        /// </summary>
        /// <param name="version">the optional roster version for roster versioning</param>
        /// <returns><see cref="RosterIq"/> stanza</returns>
        public static RosterIq RequestRoster(string version = null)
        {
            var riq = new RosterIq()
            {
                Type = IqType.Get
            };

            if (version != null)
                riq.Roster.Version = version;

            return riq;
        }

        /// <summary>
        /// Builds a request to remove a contact from the contact list, aka roster
        /// </summary>
        /// <param name="jid">The <see cref="Jid"/> to remove from the contact list</param>
        /// <returns><see cref="RosterIq"/> stanza</returns>
        public static RosterIq RemoveRosterItem(Jid jid)
        {
            var riq = new RosterIq { Type = IqType.Set };
            var ri = new RosterItem { Jid = jid, Subscription = Subscription.Remove };
            riq.Roster.AddRosterItem(ri);
            return riq;
        }

        /// <summary>
        /// Builds a request to add a contact to the contact list, aka roster
        /// </summary>
        /// <param name="jid">The <see cref="Jid"/> to add to the contact list</param>
        /// <param name="nickname">Optional nickname for this contact</param>
        /// <param name="groups">Option list of groups this contact should belong to</param>
        /// <returns><see cref="RosterIq"/> stanza</returns>
        public static RosterIq AddRosterItem(Jid jid, string nickname = null, string[] groups = null)
        {
            var riq = new RosterIq { Type = IqType.Set };

            var ri = new RosterItem { Jid = jid };

            if (!String.IsNullOrEmpty(nickname))
                ri.Name = nickname;

            if (groups != null)
            {
                foreach (string g in groups)
                {
                    ri.AddGroup(g);
                }
            }
            riq.Roster.AddRosterItem(ri);

            return riq;
        }
    }
}
