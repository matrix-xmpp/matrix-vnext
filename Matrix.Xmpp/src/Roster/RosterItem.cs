using Matrix.Core;
using Matrix.Core.Attributes;

namespace Matrix.Xmpp.Roster
{
    /// <summary>
    /// RosterItem represents a contact object.
    /// </summary>
    [XmppTag(Name = Tag.Item, Namespace = Namespaces.IqRoster)]
    public class RosterItem : Base.RosterItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RosterItem"/> class.
        /// </summary>
        public RosterItem() : base(Namespaces.IqRoster)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RosterItem"/> class.
        /// </summary>
        /// <param name="jid">The jid.</param>
        public RosterItem(Jid jid)
            : this()
        {
            Jid = jid;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RosterItem"/> class.
        /// </summary>
        /// <param name="jid">The jid.</param>
        /// <param name="name">The name.</param>
        public RosterItem(Jid jid, string name)
            : this(jid)
        {
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RosterItem"/> class.
        /// </summary>
        /// <param name="jid">The jid.</param>
        /// <param name="name">The name.</param>
        /// <param name="group">The group.</param>
        public RosterItem(Jid jid, string name, string group)
            : this(jid, name)
        {
            AddGroup(group);
        }

        /// <summary>
        /// Gets or sets the subscription.
        /// </summary>
        /// <value>The subscription.</value>
        public Subscription Subscription
        {
            get { return GetAttributeEnum<Subscription>("subscription"); }
            set { SetAttribute("subscription", value.ToString().ToLower()); }
        }

        /// <summary>
        /// Gets or sets the ask.
        /// </summary>
        /// <value>The ask.</value>
        public Ask Ask
        {
            get { return GetAttributeEnum<Ask>("ask"); }
            set
            {
                if (value == Ask.None)
                    RemoveAttribute("ask");
                else
                    SetAttribute("ask", value.ToString().ToLower());
            }
        }

        /// <summary>
        /// Approved is used to signal subscription pre-approval.
        /// </summary>
        public bool Approved
        {
            get { return GetAttributeBool("approved"); }
            set
            {
                if (value)
                    SetAttribute("approved", true);
                else
                    RemoveAttribute("approved");
            }
        }
    }
}