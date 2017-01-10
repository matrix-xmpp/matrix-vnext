using Matrix.Core;

namespace Matrix.Xmpp.Client
{
    /// <summary>
    /// Vcard Iq
    /// </summary>
    public class VcardIq : Iq
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="VcardIq"/> class.
        /// </summary>
        public VcardIq()
        {
            GenerateId();
            Vcard = new Vcard.Vcard();
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="VcardIq"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public VcardIq(IqType type)
            : this()
        {
            Type = type;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="VcardIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        public VcardIq(Jid to)
            : this()
        {
            To = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VcardIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        public VcardIq(Jid to, Jid from)
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
        public VcardIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }       
        #endregion
        
        /// <summary>
        /// Gets or sets the vcard.
        /// </summary>
        /// <value>The vcard.</value>
        public Vcard.Vcard Vcard
        {
            get { return Element<Vcard.Vcard>(); }
            set { Replace(value); }
        }
    }
}