using Matrix.Core;
using Matrix.Xml;

namespace Matrix.Xmpp.Component
{
    /// <summary>
    /// A class to create Iq queries with payloads of the given type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IqQuery<T> : Iq where T : XmppXElement, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IqQuery&lt;T&gt;"/> class.
        /// A new id gets automatically created.
        /// </summary>
        public IqQuery()
        {
            GenerateId();
            Add(new T());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IqQuery&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="query">The query.</param>
        public IqQuery(T query)
        {
            GenerateId();
            Add(query);
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="IqQuery&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public IqQuery(IqType type)
            : this()
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IqQuery&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        public IqQuery(Jid to)
            : this()
        {
            To = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IqQuery&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        public IqQuery(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IqQuery&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="type">The type.</param>
        public IqQuery(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="IqQuery&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="type">The type.</param>
        /// <param name="id">The id.</param>
        public IqQuery(Jid to, Jid from, IqType type, string id)
            : this(to, from, type)
        {
            Id = id;
        }

        public new T Query
        {
            get { return base.Query as T; }
            set { base.Query = value; }
        }
    }
}