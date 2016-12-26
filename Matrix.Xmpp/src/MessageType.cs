using Matrix.Core.Attributes;

namespace Matrix.Xmpp
{
    /// <summary>
    /// Enumeration that represents the type of a message
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// This in a normal message, much like an email. You don't expect a fast reply.
        /// </summary>
        [Name("normal")]
        Normal = -1,

        /// <summary>
        /// a error messages
        /// </summary>
        [Name("error")]
        Error,

        /// <summary>
        /// is for chat like messages, person to person. Send this if you expect a fast reply. reply or no reply at all.
        /// </summary>
        [Name("chat")]
        Chat,

        /// <summary>
        /// is used for sending/receiving messages from/to a chatroom (IRC style chats) 
        /// </summary>
        [Name("groupchat")]
        GroupChat,

        /// <summary>
        /// Think of this as a news broadcast, or RRS Feed, the message will normally have a URL and Description Associated with it.
        /// </summary>
        [Name("headline")]
        Headline
    }
}