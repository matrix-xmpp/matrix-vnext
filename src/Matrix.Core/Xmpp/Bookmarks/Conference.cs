using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Bookmarks
{
    /// <summary>
    /// represents a conference bookmark.
    /// </summary>
    [XmppTag(Name = "conference", Namespace = Namespaces.StorageBookmarks)]
    public class Conference : XmppXElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Conference"/> class.
        /// </summary>
        public Conference() : base(Namespaces.StorageBookmarks, "conference")
        {
        }

        /// <summary>
        /// A name/description for this bookmarked room
        /// </summary>
        public new string Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }

        /// <summary>
        /// Should the client join this room automatically after successfuil login?
        /// </summary>
        public bool AutoJoin
        {
            get { return GetAttributeBool("autojoin"); }
            set { SetAttribute("autojoin", value); }
        }

        /// <summary>
        /// The Jid of the bookmarked room
        /// </summary>
        public Jid Jid
        {
            get { return GetAttributeJid("jid"); }
            set { SetAttribute("jid", value); }
        }

        /// <summary>
        /// The Nickname for this room
        /// </summary>
        public string Nickname
        {
            get { return GetTag("nick"); }
            set { SetTag("nick", value); }
        }

        /// <summary>
        /// The password for password protected rooms
        /// </summary>
        public string Password
        {
            get { return GetTag("password"); }
            set { SetTag("password", value); }
        }
    }
}
