namespace Matrix.Xmpp.Client
{
    /// <summary>
    /// This class creates a Roster Iq.
    /// </summary>
    public class SearchIq : Iq
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchIq"/> class.
        /// </summary>
        public SearchIq()
        {
            GenerateId();
            Search = new Search.Search();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchIq"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public SearchIq(IqType type)
            : this()
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        public SearchIq(Jid to)
            : this()
        {
            To = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        public SearchIq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        /// <param name="type">The type.</param>
        public SearchIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        /// <param name="type">The type.</param>
        /// <param name="id">The id.</param>
        public SearchIq(Jid to, Jid from, IqType type, string id)
            : this(to, from, type)
        {
            Id = id;
        }
        #endregion

        /// <summary>
        /// The Search object
        /// </summary>
        /// <value>The search.</value>
        public Search.Search Search
        {
            get { return Element<Search.Search>(); }
            set { Replace(value); }
        }
    }
}
