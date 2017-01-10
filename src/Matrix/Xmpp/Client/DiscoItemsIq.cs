using Matrix.Xmpp.Disco;

namespace Matrix.Xmpp.Client
{
    /// <summary>
    /// Creates a Disco Items iq request
    /// </summary>
    public class DiscoItemsIq : Iq
    {
         #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoItemsIq"/> class.
        /// </summary>
        public DiscoItemsIq()
        {            
            GenerateId();
            Items = new Items();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoItemsIq"/> class.
        /// </summary>
        /// <param name="node">The node.</param>
        public DiscoItemsIq(string node) : this()
        {
           Items.Node = node;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoItemsIq"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public DiscoItemsIq(IqType type)
            : this()
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoItemsIq"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="node">The node.</param>
        public DiscoItemsIq(IqType type, string node)
            : this(type)
        {
            Items.Node = node;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoItemsIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        public DiscoItemsIq(Jid to)
            : this()
        {
            To = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoItemsIq"/> class.
        /// </summary>
        /// <param name="to">To Jis.</param>
        /// <param name="node">The node.</param>
        public DiscoItemsIq(Jid to, string node)
            : this(to)
        {
            Items.Node = node;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoItemsIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        public DiscoItemsIq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoItemsIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        /// <param name="node">The node.</param>
        public DiscoItemsIq(Jid to, Jid from, string node)
            : this(to, from)
        {
            Items.Node = node;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoItemsIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        /// <param name="type">The type.</param>
        public DiscoItemsIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoItemsIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        /// <param name="type">The type.</param>
        /// <param name="node">The node.</param>
        public DiscoItemsIq(Jid to, Jid from, IqType type, string node)
            : this(to, from, type)
        {
            Items.Node = node;
        }
        #endregion


        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        public Items Items
        {
            get { return Element<Items>(); }
            set { Replace(value); }
        }
    }
}