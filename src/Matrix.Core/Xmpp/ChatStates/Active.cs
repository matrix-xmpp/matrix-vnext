using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Chatstates
{
    /// <summary>
    /// User is actively participating in the chat session.
    /// User accepts an initial content message, sends a content message, 
    /// gives focus to the chat interface, or is otherwise paying attention to the conversation.
    /// </summary>
    [XmppTag(Name = "active", Namespace = Namespaces.Chatstates)]
    public class Active : XmppXElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Active"/> class.
        /// </summary>
        public Active()
            : base(Namespaces.Chatstates, Chatstate.Active.ToString().ToLower())
        {
        }
    }
}
