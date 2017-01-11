using Matrix.Attributes;

namespace Matrix.Xmpp.Muc.Admin
{
    /// <summary>
    /// 
    /// </summary>
    [XmppTag(Name = Tag.Item, Namespace = Namespaces.MucAdmin)]
    public class Item : Muc.Item
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item()
            : base(Namespaces.MucAdmin)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="affiliation">The affiliation.</param>
        public Item(Affiliation affiliation)
            : this()
        {
            Affiliation = affiliation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="role">The role.</param>
        public Item(Role role)
            : this()
        {
            Role = role;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="nick">The nick.</param>
        public Item(Role role, string nick)
            : this(role)
        {
            Nickname = nick;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="nick">The nick.</param>
        /// <param name="reason">The reason.</param>
        public Item(Role role, string nick, string reason)
            : this(role, nick)
        {
            Reason = reason;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="affiliation">The affiliation.</param>
        /// <param name="role">The role.</param>
        public Item(Affiliation affiliation, Role role)
            : this(affiliation)
        {
            Role = role;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="affiliation">The affiliation.</param>
        /// <param name="role">The role.</param>
        /// <param name="jid">The jjid.</param>
        public Item(Affiliation affiliation, Role role, Jid jid)
            : this(affiliation, role)
        {
            Jid = jid;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="affiliation">The affiliation.</param>
        /// <param name="role">The role.</param>
        /// <param name="jid">The jid.</param>
        /// <param name="nick">The nick.</param>
        public Item(Affiliation affiliation, Role role, Jid jid, string nick)
            : this(affiliation, role, jid)
        {
            Nickname = nick;
        }
        #endregion
    }
}