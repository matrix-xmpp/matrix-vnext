using Matrix.Core;

namespace Matrix.Xmpp.Component
{
    public class PingIq : Iq
    {
        #region << Constructors >>
        public PingIq()
        {
            Add(new Ping.Ping());
            GenerateId();
        }

        public PingIq(IqType type)
            : this()
        {
            Type = type;
        }

        public PingIq(Jid to)
            : this()
        {
            To = to;
        }

        public PingIq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        public PingIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }

        public PingIq(Jid to, Jid from, IqType type, string id)
            : this(to, from, type)
        {
            Id = id;
        }
        #endregion

        /// <summary>
        /// Ping object
        /// </summary>
        public Ping.Ping Ping
        {
            get { return Element<Ping.Ping>(); }
            set { Replace(value); }
        }
    }
}