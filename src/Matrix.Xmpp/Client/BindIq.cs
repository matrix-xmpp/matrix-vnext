using Matrix.Core;

namespace Matrix.Xmpp.Client
{
    public class BindIq : Iq
    {
        #region << Constructors >>
        public BindIq()
        {
            Add(new Bind.Bind());
            GenerateId();
        }

        public BindIq(IqType type)
            : this()
        {
            Type = type;
        }       

        public BindIq(Jid to)
            : this()
        {
            To = to;
        }

        public BindIq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        public BindIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }

        public BindIq(Jid to, Jid from, IqType type, string id)
            : this(to, from, type)
        {
            Id = id;
        }
        #endregion

        /// <summary>
        /// Bind object
        /// </summary>
        public Bind.Bind Bind
        {
            get { return Element<Bind.Bind>(); }
            set { Replace(value); }
        }
    }
}