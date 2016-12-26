using Matrix.Core;
using Matrix.Xmpp.Client;

namespace Matrix.Xmpp.Muc.Owner
{
    /// <summary>
    /// Represents a Muc Owner Iq.
    /// </summary>
    public class OwnerIq : Iq
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerIq"/> class.
        /// </summary>
        public OwnerIq()
        {
            GenerateId();
            OwnerQuery = new OwnerQuery();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerIq"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public OwnerIq(IqType type)
            : this()
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerIq"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        public OwnerIq(Jid to)
            : this()
        {
            To = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerIq"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        public OwnerIq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerIq"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="type">The type.</param>
        public OwnerIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerIq"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="type">The type.</param>
        /// <param name="id">The id.</param>
        public OwnerIq(Jid to, Jid from, IqType type, string id)
            : this(to, from, type)
        {
            Id = id;
        }
        #endregion

        /// <summary>
        /// Gets or sets the owner query.
        /// </summary>
        /// <value>The admin.</value>
        public OwnerQuery OwnerQuery
        {
            get { return Element<OwnerQuery>(); }
            set { Replace(value); }
        }
    }
}