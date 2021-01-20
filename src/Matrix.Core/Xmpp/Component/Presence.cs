using Matrix.Attributes;

namespace Matrix.Xmpp.Component
{
    [XmppTag(Name = Tag.Presence, Namespace = Namespaces.Accept)]
    public class Presence : Base.Presence
    {
        #region <<Constructors >>
        public Presence() : base(Namespaces.Accept)
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
