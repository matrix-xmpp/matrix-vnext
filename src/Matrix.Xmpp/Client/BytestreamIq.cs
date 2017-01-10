using Matrix.Core;

namespace Matrix.Xmpp.Client
{
    public class BytestreamIq : Iq
    {
        #region << Constructors >>
        public BytestreamIq()
        {
            Add(new Bytestreams.Bytestream());
            GenerateId();
        }

        public BytestreamIq(IqType type)
            : this()
        {
            Type = type;
        }       

        public BytestreamIq(Jid to)
            : this()
        {
            To = to;
        }

        public BytestreamIq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        public BytestreamIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }

        public BytestreamIq(Jid to, Jid from, IqType type, string id)
            : this(to, from, type)
        {
            Id = id;
        }
        #endregion

        /// <summary>
        /// Bytestream object
        /// </summary>
        public Bytestreams.Bytestream Bytestream
        {
            get { return Element<Bytestreams.Bytestream>(); }
            set { Replace(value); }
        }
    }
}