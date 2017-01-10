using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Chatstates
{
    /// <summary>
    /// User is composing a message.
    /// User is interacting with a message input interface specific to this chat session 
    /// (e.g., by typing in the input area of a chat window).
    /// </summary>
    [XmppTag(Name = "composing", Namespace = Namespaces.Chatstates)]
    public class Composing : XmppXElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Composing"/> class.
        /// </summary>
        public Composing()
            : base(Namespaces.Chatstates, Chatstate.Composing.ToString().ToLower())
        {
        }
    }
}