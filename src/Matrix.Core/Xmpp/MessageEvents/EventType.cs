using System;
using Matrix.Attributes;

namespace Matrix.Xmpp.MessageEvents
{
    /// <summary>
    /// XEP-0022: Message Event types
    /// </summary>
    [Flags]
    public enum EventType
    {
        /// <summary>
        /// No event type specified.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates that the message has been stored offline by the intended recipient's server.
        /// This event is triggered only if the intended recipient's server supports offline storage, 
        /// has that support enabled, and the recipient is offline when the server receives the message for delivery.
        /// </summary>
        [Name("offline")]
        Offline = 1,

        /// <summary>
        /// Indicates that the message has been delivered to the recipient. 
        /// This signifies that the message has reached the recipient's Jabber client, 
        /// but does not necessarily mean that the message has been displayed. 
        /// This event is to be raised by the Jabber client.
        /// </summary>
        [Name("delivered")]
        Delivered = 2,

        /// <summary>
        /// Once the message has been received by the recipient's Jabber client, 
        /// it may be displayed to the user. 
        /// This event indicates that the message has been displayed, and is to be raised by the Jabber client.
        /// Even if a message is displayed multiple times, this event should be raised only once.
        /// </summary>
        [Name("displayed")]
        Displayed = 4,

        /// <summary>
        /// In threaded chat conversations, this indicates that the recipient is composing a reply to a message.
        /// The event is to be raised by the recipient's Jabber client. 
        /// A Jabber client is allowed to raise this event multiple times in response to the same request, 
        /// providing the original event is cancelled first.
        /// </summary>
        [Name("composing")]
        Composing = 8
    }
}
