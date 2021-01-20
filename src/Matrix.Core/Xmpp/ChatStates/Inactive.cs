using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Chatstates
{
    /// <summary>
    /// User has not been actively participating in the chat session.
    /// User has not interacted with the chat interface for an intermediate period of time (e.g., 30 seconds).
    /// </summary>
    [XmppTag(Name = "inactive", Namespace = Namespaces.Chatstates)]
    public class Inactive : XmppXElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Inactive"/> class.
        /// </summary>
        public Inactive()
            : base(Namespaces.Chatstates, Chatstate.Inactive.ToString().ToLower())
        {
        }
    }
}
