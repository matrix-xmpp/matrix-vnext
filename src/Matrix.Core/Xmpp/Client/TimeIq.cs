namespace Matrix.Xmpp.Client
{
    /// <summary>
    /// Time Iq query
    /// </summary>
    public class TimeIq : Iq
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeIq"/> class.
        /// </summary>
        public TimeIq()
        {
            GenerateId();
            Time = new Time.Time();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeIq"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public TimeIq(IqType type)
            : this()
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        public TimeIq(Jid to)
            : this()
        {
            To = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        public TimeIq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VcardIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        /// <param name="type">The type.</param>
        public TimeIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }
        #endregion
        /// <summary>
        /// The <see cref="Time.Time"/>
        /// </summary>
        public Time.Time Time
        {
            get { return Element<Time.Time>(); }
            set { Replace(value); }
        }
    }
}
