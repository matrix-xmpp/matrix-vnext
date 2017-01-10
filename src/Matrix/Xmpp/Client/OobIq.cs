namespace Matrix.Xmpp.Client
{
    public class OobIq : Iq
    {
        #region << Constructors >>

        /// <summary>
        /// Initializes a new instance of the <see cref="OobIq"/> class.
        /// </summary>
        public OobIq()
        {
            GenerateId();
            Oob = new Oob.Oob();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OobIq"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public OobIq(IqType type)
            : this()
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OobIq"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        public OobIq(Jid to)
            : this()
        {
            To = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OobIq"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        public OobIq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OobIq"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="type">The type.</param>
        public OobIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OobIq"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="type">The type.</param>
        /// <param name="id">The id.</param>
        public OobIq(Jid to, Jid from, IqType type, string id)
            : this(to, from, type)
        {
            Id = id;
        }
        #endregion

        /// <summary>
        /// Gets or sets the oob.
        /// </summary>
        /// <value>
        /// The oob.
        /// </value>
        public Oob.Oob Oob
        {
            get { return Element<Oob.Oob>(); }
            set { Replace(value); }
        }
    }
}