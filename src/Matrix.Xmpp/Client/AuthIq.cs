using Matrix.Core;

namespace Matrix.Xmpp.Client
{
    /// <summary>
    /// This class creates a Auth Iq.
    /// </summary>
    public class AuthIq : Iq
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthIq"/> class.
        /// </summary>
        public AuthIq()
        {
            GenerateId();
            Auth = new Auth.Auth();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthIq"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public AuthIq(IqType type)
            : this()
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        public AuthIq(Jid to)
            : this()
        {
            To = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        public AuthIq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        /// <param name="type">The type.</param>
        public AuthIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        /// <param name="type">The type.</param>
        /// <param name="id">The id.</param>
        public AuthIq(Jid to, Jid from, IqType type, string id)
            : this(to, from, type)
        {
            Id = id;
        }
        #endregion

        /// <summary>
        /// Auth object
        /// </summary>
        /// <value>The Auth object.</value>
        public Auth.Auth Auth
        {
            get { return Element<Auth.Auth>(); }
            set { Replace(value); }
        }
    }
}