using Matrix.Attributes;
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.Muc
{
    /// <summary>
    /// XEP-0249: Direct MUC Invitations
    /// </summary>
    [XmppTag(Name = "x", Namespace = Namespaces.XConference)]
    public class Conference : XmppXElementWithJidAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Conference"/> class.
        /// </summary>
        public Conference()
            : base(Namespaces.XConference, "x")
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Conference"/> class.
        /// </summary>
        /// <param name="jid">The jid of the conference room.</param>
        public Conference(Jid jid) : this()
        {
            Jid = jid;
        }

        /// <summary>
        /// specifies a password needed for entry into a password-protected room.
        /// </summary>
        public string Password
        {
            get { return GetAttribute("password"); }
            set { SetAttribute("password", value); }
        }

        /// <summary>
        /// specifies a human-readable purpose for the invitation.
        /// </summary>
        public string Reason
        {
            get { return GetAttribute("reason"); }
            set { SetAttribute("reason", value); }
        }
    }
}