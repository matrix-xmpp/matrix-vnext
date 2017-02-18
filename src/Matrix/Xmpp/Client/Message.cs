using Matrix.Attributes;

namespace Matrix.Xmpp.Client
{
    /// <summary>
    /// Message class
    /// </summary>
    [XmppTag(Name = Tag.Message, Namespace = Namespaces.Client)]
    public class Message : Base.Message
    {        
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        public Message() : base(Namespaces.Client)
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        public Message(Jid to)
            : this()
        {
            To = to;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="body">The body.</param>
        public Message(Jid to, string body)
            : this(to)
        {
            Body = body;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        public Message(Jid to, Jid from)
            : this()
        {
            To = to;
            From = from;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="body">The body.</param>
        public Message(string to, string body)
            : this()
        {
            To = new Jid(to);
            Body = body;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="body">The body.</param>
        /// <param name="subject">The subject.</param>
        public Message(Jid to, string body, string subject)
            : this()
        {
            To = to;
            Body = body;
            Subject = subject;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="body">The body.</param>
        /// <param name="subject">The subject.</param>
        public Message(string to, string body, string subject)
            : this()
        {
            To = new Jid(to);
            Body = body;
            Subject = subject;
        }

        public Message(string to, string body, string subject, string thread)
            : this()
        {
            To = new Jid(to);
            Body = body;
            Subject = subject;
            Thread = thread;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="body">The body.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="thread">The thread.</param>
        public Message(Jid to, string body, string subject, string thread)
            : this()
        {
            To = to;
            Body = body;
            Subject = subject;
            Thread = thread;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="type">The type.</param>
        /// <param name="body">The body.</param>
        public Message(string to, MessageType type, string body)
            : this()
        {
            To = new Jid(to);
            Type = type;
            Body = body;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="type">The type.</param>
        /// <param name="body">The body.</param>
        public Message(Jid to, MessageType type, string body)
            : this()
        {
            To = to;
            Type = type;
            Body = body;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="type">The type.</param>
        /// <param name="body">The body.</param>
        /// <param name="subject">The subject.</param>
        public Message(string to, MessageType type, string body, string subject)
            : this()
        {
            To = new Jid(to);
            Type = type;
            Body = body;
            Subject = subject;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="type">The type.</param>
        /// <param name="body">The body.</param>
        /// <param name="subject">The subject.</param>
        public Message(Jid to, MessageType type, string body, string subject)
            : this()
        {
            To = to;
            Type = type;
            Body = body;
            Subject = subject;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="type">The type.</param>
        /// <param name="body">The body.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="thread">The thread.</param>
        public Message(string to, MessageType type, string body, string subject, string thread)
            : this()
        {
            To = new Jid(to);
            Type = type;
            Body = body;
            Subject = subject;
            Thread = thread;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="type">The type.</param>
        /// <param name="body">The body.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="thread">The thread.</param>
        public Message(Jid to, MessageType type, string body, string subject, string thread)
            : this()
        {
            To = to;
            Type = type;
            Body = body;
            Subject = subject;
            Thread = thread;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="body">The body.</param>
        public Message(Jid to, Jid from, string body)
            : this()
        {
            To = to;
            From = from;
            Body = body;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="body">The body.</param>
        /// <param name="subject">The subject.</param>
        public Message(Jid to, Jid from, string body, string subject)
            : this()
        {
            To = to;
            From = from;
            Body = body;
            Subject = subject;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="body">The body.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="thread">The thread.</param>
        public Message(Jid to, Jid from, string body, string subject, string thread)
            : this()
        {
            To = to;
            From = from;
            Body = body;
            Subject = subject;
            Thread = thread;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="type">The type.</param>
        /// <param name="body">The body.</param>
        public Message(Jid to, Jid from, MessageType type, string body)
            : this()
        {
            To = to;
            From = from;
            Type = type;
            Body = body;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="type">The type.</param>
        /// <param name="body">The body.</param>
        /// <param name="subject">The subject.</param>
        public Message(Jid to, Jid from, MessageType type, string body, string subject)
            : this()
        {
            To = to;
            From = from;
            Type = type;
            Body = body;
            Subject = subject;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="type">The type.</param>
        /// <param name="body">The body.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="thread">The thread.</param>
        public Message(Jid to, Jid from, MessageType type, string body, string subject, string thread)
            : this()
        {
            To = to;
            From = from;
            Type = type;
            Body = body;
            Subject = subject;
            Thread = thread;
        }
        #endregion

        /// <summary>
        /// Gets or sets the <see cref="Error"/>.
        /// </summary>
        /// <value>The error.</value>
        public new Error Error
        {
            get { return Element<Error>(); }
            set { Replace(value); }
        }        
    }
}