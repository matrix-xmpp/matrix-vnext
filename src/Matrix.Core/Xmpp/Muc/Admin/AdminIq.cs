using Matrix.Xmpp.Client;

namespace Matrix.Xmpp.Muc.Admin
{
    /// <summary>
    /// Represents a Muc Admin Iq.
    /// </summary>
    public class AdminIq : Iq
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="AdminIq"/> class.
        /// </summary>
        public AdminIq()
        {
            GenerateId();
            AdminQuery = new AdminQuery();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminIq"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public AdminIq(IqType type)
            : this()
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminIq"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        public AdminIq(Jid to)
            : this()
        {
            To = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminIq"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        public AdminIq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminIq"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="type">The type.</param>
        public AdminIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminIq"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="type">The type.</param>
        /// <param name="id">The id.</param>
        public AdminIq(Jid to, Jid from, IqType type, string id)
            : this(to, from, type)
        {
            Id = id;
        }
        #endregion

        /// <summary>
        /// Gets or sets the admin.
        /// </summary>
        /// <value>The admin.</value>
        public AdminQuery AdminQuery
        {
            get { return Element<AdminQuery>(); }
            set { Replace(value); }
        }
    }
}
