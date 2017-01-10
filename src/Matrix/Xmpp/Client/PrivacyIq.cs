namespace Matrix.Xmpp.Client
{
    public class PrivacyIq : Iq
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivacyIq"/> class.
        /// </summary>
        public PrivacyIq()
        {
            GenerateId();
            Privacy = new Privacy.Privacy();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivacyIq"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public PrivacyIq(IqType type)
            : this()
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivacyIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        public PrivacyIq(Jid to)
            : this()
        {
            To = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivacyIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        public PrivacyIq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivacyIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        /// <param name="type">The type.</param>
        public PrivacyIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }
        #endregion

        /// <summary>
        /// Gets or sets the privacy object.
        /// </summary>
        /// <value>The vcard.</value>
        public Privacy.Privacy Privacy
        {
            get { return Element<Privacy.Privacy>(); }
            set { Replace(value); }
        }
    }
}