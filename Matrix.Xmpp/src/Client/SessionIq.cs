using Matrix.Core;

namespace Matrix.Xmpp.Client
{
    public class SessionIq : Iq
    {
        #region << Constructors >>
        public SessionIq()
        {
            Add(new Session.Session());
            GenerateId();
        }

        public SessionIq(IqType type)
            : this()
        {
            Type = type;
        }

        public SessionIq(Jid to)
            : this()
        {
            To = to;
        }

        public SessionIq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        public SessionIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }

        public SessionIq(Jid to, Jid from, IqType type, string id)
            : this(to, from, type)
        {
            Id = id;
        }
        #endregion

        /// <summary>
        /// Session object
        /// </summary>
        public Session.Session Session
        {
            get { return Element<Session.Session>(); }
            set { Replace(value); }
        }
    }
}