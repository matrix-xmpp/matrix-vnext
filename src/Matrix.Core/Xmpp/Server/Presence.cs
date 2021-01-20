using Matrix.Attributes;

namespace Matrix.Xmpp.Server
{
    [XmppTag(Name = Tag.Presence, Namespace = Namespaces.Server)]
    public class Presence : Base.Presence
    {
        #region <<Constructors >>
        public Presence() : base(Namespaces.Server)
        {
        }

        public Presence(Show show) : this()
        {
            Show = show;
        }

        public Presence(Show show, string status)
            : this(show)
        {
            Status = status;
        }

        public Presence(Show show, string status, int priority)
            : this(show, status)
        {
            Priority = priority;
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
