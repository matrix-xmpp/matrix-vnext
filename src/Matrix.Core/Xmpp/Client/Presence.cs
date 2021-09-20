using Matrix.Attributes;
using Matrix.Xmpp.Nickname;

namespace Matrix.Xmpp.Client
{
    /// <summary>
    /// Presence object
    /// </summary>
    [XmppTag(Name = Tag.Presence, Namespace = Namespaces.Client)]
    public class Presence : Base.Presence
    {
        #region <<Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Presence"/> class.
        /// </summary>
        public Presence() : base(Namespaces.Client)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Presence"/> class.
        /// </summary>
        /// <param name="show">The <see cref="Show"/>.</param>
        public Presence(Show show) : this()
        {
            Show = show;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Presence"/> class.
        /// </summary>
        /// <param name="type">The <see cref="PresenceType"/>.</param>
        public Presence(PresenceType type)
            : this()
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Presence"/> class.
        /// </summary>
        /// <param name="show">The show.</param>
        /// <param name="status">The status.</param>
        public Presence(Show show, string status)
            : this(show)
        {
            Status = status;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Presence"/> class.
        /// </summary>
        /// <param name="show">The <see cref="Show"/>.</param>
        /// <param name="status">The status.</param>
        /// <param name="priority">The priority.</param>
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
            set { Replace(value); }
        }
        
        /// <summary>
        /// Gets or sets the nickname.
        /// </summary>
        /// <value>The nick.</value>
        public Nick Nick
        {
            get { return Element<Nick>(); }
            set { Replace(value); }
        }
    }
}
