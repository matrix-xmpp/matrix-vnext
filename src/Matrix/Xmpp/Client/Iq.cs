using Matrix.Attributes;

namespace Matrix.Xmpp.Client
{
    [XmppTag(Name = Tag.Iq, Namespace = Namespaces.Client)]
    public class Iq : Base.Iq
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Iq"/> class.
        /// </summary>
        public Iq()
            : base(Namespaces.Client)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Iq"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public Iq(IqType type)
            : this()
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Iq"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        public Iq(Jid to)
            : this()
        {
            To = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Iq"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        public Iq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Iq"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="type">The type.</param>
        public Iq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Iq"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="type">The type.</param>
        /// <param name="id">The id.</param>
        public Iq(Jid to, Jid from, IqType type, string id)
            : this(to, from, type)
        {
            Id = id;
        }
        #endregion

        /// <summary>
        /// Error object
        /// </summary>
        public Error Error
        {
            get { return Element<Error>(); }
            set { Replace(value); }
        }
    }
}