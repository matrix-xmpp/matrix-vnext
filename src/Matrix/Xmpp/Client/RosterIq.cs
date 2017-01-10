namespace Matrix.Xmpp.Client
{
    /// <summary>
    /// This class creates a Roster Iq.
    /// </summary>
    public class RosterIq : Iq
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="RosterIq"/> class.
        /// </summary>
        public RosterIq()
        {
            GenerateId();
            Roster = new Roster.Roster();            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RosterIq"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public RosterIq(IqType type)
            : this()
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RosterIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        public RosterIq(Jid to)
            : this()
        {
            To = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RosterIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        public RosterIq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RosterIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        /// <param name="type">The type.</param>
        public RosterIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RosterIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        /// <param name="type">The type.</param>
        /// <param name="id">The id.</param>
        public RosterIq(Jid to, Jid from, IqType type, string id)
            : this(to, from, type)
        {
            Id = id;
        }
        #endregion

        /// <summary>
        /// Roster object
        /// </summary>
        /// <value>The roster.</value>
        public Roster.Roster Roster
        {
            get { return Element<Roster.Roster>(); }
            set { Replace(value); }
        }
    }
}