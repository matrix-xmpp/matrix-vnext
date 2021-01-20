namespace Matrix.Xmpp.Client
{
    /// <summary>
    /// Last activity IQ, XEP-0012
    /// </summary>
    public class LastIq : Iq
    {
        #region << Constructors >>
        public LastIq()
        {
            Add(new Last.Last());
            GenerateId();
        }

        public LastIq(IqType type)
            : this()
        {
            Type = type;
        }

        public LastIq(Jid to)
            : this()
        {
            To = to;
        }

        public LastIq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        public LastIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }

        public LastIq(Jid to, Jid from, IqType type, string id)
            : this(to, from, type)
        {
            Id = id;
        }
        #endregion

        /// <summary>
        /// Last object
        /// </summary>
        public Last.Last Last
        {
            get { return Element<Last.Last>(); }
            set { Replace(value); }
        }
    }
}
