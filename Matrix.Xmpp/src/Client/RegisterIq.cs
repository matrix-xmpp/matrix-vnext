using Matrix.Core;

namespace Matrix.Xmpp.Client
{
    /// <summary>
    /// This class creates a Register Iq.
    /// </summary>
    public class RegisterIq : Iq
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterIq"/> class.
        /// </summary>
        public RegisterIq()
        {
            GenerateId();
            Register = new Register.Register();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterIq"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public RegisterIq(IqType type)
            : this()
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        public RegisterIq(Jid to)
            : this()
        {
            To = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        public RegisterIq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        /// <param name="type">The type.</param>
        public RegisterIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        /// <param name="type">The type.</param>
        /// <param name="id">The id.</param>
        public RegisterIq(Jid to, Jid from, IqType type, string id)
            : this(to, from, type)
        {
            Id = id;
        }
        #endregion

        /// <summary>
        /// Gets or sets the register.
        /// </summary>
        /// <value>The register.</value>
        public Register.Register Register
        {
            get { return Element<Register.Register>(); }
            set { Replace(value); }
        }
    }
}