using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Chatstates
{
    /// <summary>
    /// User had been composing but now has stopped.
    /// User was composing but has not interacted with the message input interface for a short period of time (e.g., 5 seconds).
    /// </summary>
    [XmppTag(Name = "paused", Namespace = Namespaces.Chatstates)]
    public class Paused : XmppXElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Paused"/> class.
        /// </summary>
        public Paused()
            : base(Namespaces.Chatstates, Chatstate.Paused.ToString().ToLower())
        {
        }
    }
}
