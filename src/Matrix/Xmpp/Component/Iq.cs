using Matrix.Attributes;

namespace Matrix.Xmpp.Component
{
    [XmppTag(Name = Tag.Iq, Namespace = Namespaces.Accept)]
    public class Iq : Base.Iq
    {
        #region << Constructors >>
        public Iq()
            : base(Namespaces.Accept)
        {
        }

        public Iq(IqType type)
            : this()
        {
            Type = type;
        }

        public Iq(Jid to)
            : this()
        {
            To = to;
        }

        public Iq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        public Iq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }

        public Iq(Jid to, Jid from, IqType type, string id)
            : this(to, from, type)
        {
            Id = id;
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