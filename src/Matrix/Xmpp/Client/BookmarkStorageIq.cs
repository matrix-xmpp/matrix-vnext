using Matrix.Xmpp.Bookmarks;

namespace Matrix.Xmpp.Client
{
    public class BookmarkStorageIq : Iq
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateIq"/> class.
        /// </summary>
        public BookmarkStorageIq()
        {
            GenerateId();
            Private = new Private.Private();
            Private.Add(new Storage());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateIq"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public BookmarkStorageIq(IqType type)
            : this()
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        public BookmarkStorageIq(Jid to)
            : this()
        {
            To = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        public BookmarkStorageIq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        /// <param name="type">The type.</param>
        public BookmarkStorageIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }
        #endregion

        /// <summary>
        /// Gets or sets the private object.
        /// </summary>
        /// <value>The vcard.</value>
        public Private.Private Private
        {
            get { return Element<Private.Private>(); }
            set { Replace(value); }
        }
    }
}