using Matrix.Xmpp.Disco;

namespace Matrix.Xmpp.Client
{
    /// <summary>
    /// Creates a Disco info iq request
    /// </summary>
    public class DiscoInfoIq : Iq
    {
        #region << Constructors >>

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoInfoIq"/> class.
        /// </summary>
        public DiscoInfoIq()
        {            
            GenerateId();
            Info = new Info();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoInfoIq"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public DiscoInfoIq(IqType type)
            : this()
        {
            Type = type;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoInfoIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        public DiscoInfoIq(Jid to)
            : this()
        {
            To = to;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoInfoIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        public DiscoInfoIq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoInfoIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        /// <param name="type">The type.</param>
        public DiscoInfoIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoInfoIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        /// <param name="type">The type.</param>
        /// <param name="id">The id.</param>
        public DiscoInfoIq(Jid to, Jid from, IqType type, string id)
            : this(to, from, type)
        {
            Id = id;
        }
        #endregion


        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        public Info Info
        {
            get { return Element<Info>(); }
            set { Replace(value); }
        }
    }
}