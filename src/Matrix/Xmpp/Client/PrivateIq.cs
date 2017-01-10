namespace Matrix.Xmpp.Client
{
    public class PrivateIq : Iq
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateIq"/> class.
        /// </summary>
        public PrivateIq()
        {
            GenerateId();
            Private = new Private.Private();
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateIq"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public PrivateIq(IqType type)
            : this()
        {
            Type = type;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        public PrivateIq(Jid to)
            : this()
        {
            To = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        public PrivateIq(Jid to, Jid from)
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
        public PrivateIq(Jid to, Jid from, IqType type)
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