using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Chatstates
{
    /// <summary>
    /// User has effectively ended their participation in the chat session.
    /// User has not interacted with the chat interface, system, or device for a relatively long period of time 
    /// (e.g., 2 minutes), or has terminated the chat interface (e.g., by closing the chat window).
    /// </summary>
    [XmppTag(Name = "gone", Namespace = Namespaces.Chatstates)]
    public class Gone : XmppXElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Gone"/> class.
        /// </summary>
        public Gone()
            : base(Namespaces.Chatstates, Chatstate.Gone.ToString().ToLower())
        {
        }
    }
}