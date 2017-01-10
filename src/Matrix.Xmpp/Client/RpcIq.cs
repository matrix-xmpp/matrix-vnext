using Matrix.Core;

namespace Matrix.Xmpp.Client
{
    /// <summary>
    /// This class creates a Roster Iq.
    /// </summary>
    public class RpcIq : Iq
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="RpcIq" /> class.
        /// </summary>
        public RpcIq()
        {
            GenerateId();
            Rpc = new Rpc.Rpc();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RpcIq"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public RpcIq(IqType type)
            : this()
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RpcIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        public RpcIq(Jid to)
            : this()
        {
            To = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RpcIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        public RpcIq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RpcIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        /// <param name="type">The type.</param>
        public RpcIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RpcIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        /// <param name="type">The type.</param>
        /// <param name="id">The id.</param>
        public RpcIq(Jid to, Jid from, IqType type, string id)
            : this(to, from, type)
        {
            Id = id;
        }
        #endregion

        /// <summary>
        /// The Rpc object
        /// </summary>
        /// <value>The Rpc object.</value>
        public Rpc.Rpc Rpc
        {
            get { return Element<Rpc.Rpc>(); }
            set { Replace(value); }
        }
    }
}