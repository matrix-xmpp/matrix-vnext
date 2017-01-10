namespace Matrix.Xmpp.Client
{
    /// <summary>
    /// Software Version IQ, XEP-0092
    /// </summary>
    public class VersionIq : Iq
    {
        #region << Constructors >>
        public VersionIq()
        {
            Add(new Version.Version());
            GenerateId();
        }

        public VersionIq(IqType type)
            : this()
        {
            Type = type;
        }

        public VersionIq(Jid to)
            : this()
        {
            To = to;
        }

        public VersionIq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        public VersionIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }

        public VersionIq(Jid to, Jid from, IqType type, string id)
            : this(to, from, type)
        {
            Id = id;
        }
        #endregion

        /// <summary>
        /// Version object
        /// </summary>
        public Version.Version Version
        {
            get { return Element<Version.Version>(); }
            set { Replace(value); }
        }
    }
}