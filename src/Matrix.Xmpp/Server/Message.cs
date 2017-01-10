using Matrix.Core;
using Matrix.Core.Attributes;

namespace Matrix.Xmpp.Server
{
    [XmppTag(Name = Tag.Message, Namespace = Namespaces.Server)]
    public class Message : Base.Message
    {        
        #region << Constructors >>
        public Message() : base(Namespaces.Server)
        {            
        }      

        public Message(Jid to)
            : this()
        {
            To = to;
        }
        public Message(Jid to, string body)
            : this(to)
        {
            Body = body;
        }

        public Message(Jid to, Jid from)
            : this()
        {
            To = to;
            From = from;
        }

        public Message(string to, string body)
            : this()
        {
            To = new Jid(to);
            Body = body;
        }

        public Message(Jid to, string body, string subject)
            : this()
        {
            To = to;
            Body = body;
            Subject = subject;
        }

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

        public Message(Jid to, string body, string subject, string thread)
            : this()
        {
            To = to;
            Body = body;
            Subject = subject;
            Thread = thread;
        }

        public Message(string to, MessageType type, string body)
            : this()
        {
            To = new Jid(to);
            Type = type;
            Body = body;
        }

        public Message(Jid to, MessageType type, string body)
            : this()
        {
            To = to;
            Type = type;
            Body = body;
        }

        public Message(string to, MessageType type, string body, string subject)
            : this()
        {
            To = new Jid(to);
            Type = type;
            Body = body;
            Subject = subject;
        }

        public Message(Jid to, MessageType type, string body, string subject)
            : this()
        {
            To = to;
            Type = type;
            Body = body;
            Subject = subject;
        }

        public Message(string to, MessageType type, string body, string subject, string thread)
            : this()
        {
            To = new Jid(to);
            Type = type;
            Body = body;
            Subject = subject;
            Thread = thread;
        }

        public Message(Jid to, MessageType type, string body, string subject, string thread)
            : this()
        {
            To = to;
            Type = type;
            Body = body;
            Subject = subject;
            Thread = thread;
        }

        public Message(Jid to, Jid from, string body)
            : this()
        {
            To = to;
            From = from;
            Body = body;
        }

        public Message(Jid to, Jid from, string body, string subject)
            : this()
        {
            To = to;
            From = from;
            Body = body;
            Subject = subject;
        }

        public Message(Jid to, Jid from, string body, string subject, string thread)
            : this()
        {
            To = to;
            From = from;
            Body = body;
            Subject = subject;
            Thread = thread;
        }

        public Message(Jid to, Jid from, MessageType type, string body)
            : this()
        {
            To = to;
            From = from;
            Type = type;
            Body = body;
        }

        public Message(Jid to, Jid from, MessageType type, string body, string subject)
            : this()
        {
            To = to;
            From = from;
            Type = type;
            Body = body;
            Subject = subject;
        }

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
        /// Error object
        /// </summary>
        public Error Error
        {
            get { return Element<Error>(); }
            set { Replace<Error>(value); }
        }
    }
}