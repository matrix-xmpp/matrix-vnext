using Matrix.Core.Attributes;

namespace Matrix.Xmpp.PubSub
{
    public enum SubscriptionState
    {
        /// <summary>
        /// The node MUST NOT send event notifications or payloads to the Entity.
        /// </summary>
        [Name("none")]
        None,

        /// <summary>
        /// An entity has requested to subscribe to a node and the request 
        /// has not yet been approved by a node owner. The node MUST NOT 
        /// send event notifications or payloads to the entity while it 
        /// is in this state.
        /// </summary>
        [Name("pending")]
        Pending,

        /// <summary>
        /// An entity has subscribed but its subscription options have not yet 
        /// been configured. The node MAY send event notifications or payloads 
        /// to the entity while it is in this state. 
        /// The service MAY timeout unconfigured subscriptions.
        /// </summary>
        [Name("unconfigured")]
        Unconfigured,

        /// <summary>
        /// An entity is subscribed to a node. The node MUST send all event 
        /// notifications (and, if configured, payloads) to the entity while 
        /// it is in this state (subject to subscriber configuration and 
        /// content filtering).
        /// </summary>
        [Name("subscribed")]
        Subscribed
    }
}