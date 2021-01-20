using Matrix.Attributes;

namespace Matrix.Xmpp
{
    /// <summary>
    /// Enumeration for the Presence Type structure. 
    /// This enum is used to describe what type of Subscription Type the current subscription is.
    /// When sending a presence or receiving a subscription this type is used to easily identify the type of subscription it is.
    /// </summary>
    public enum PresenceType
    {
        /// <summary>
        /// Used when one wants to send presence to someone/server/transport that you’re available. 
        /// </summary>
        [Name("available")]
        Available = -1,

        /// <summary>
        /// Used to send a subscription request to someone.
        /// </summary>
        [Name("subscribe")]
        Subscribe,

        /// <summary>
        /// Used to accept a subscription request.
        /// </summary>		
        [Name("subscribed")]
        Subscribed,

        /// <summary>
        /// Used to unsubscribe someone from your presence. 
        /// </summary>
        [Name("unsubscribe")]
        Unsubscribe,

        /// <summary>
        /// Used to deny a subscription request.
        /// </summary>
        [Name("unsubscribed")]
        Unsubscribed,

        /// <summary>
        /// Used when one wants to send presence to someone/server/transport that you’re unavailable.
        /// </summary>
        [Name("unavailable")]
        Unavailable,

        /// <summary>
        /// Used when you want to see your roster, but don't want anyone on you roster to see you
        /// </summary>
        [Name("invisible")]
        Invisible,

        /// <summary>
        /// If a user chooses to become visible after being invisible, the client will send undirected presence with a type="visible" attribute.
        /// </summary>
        [Name("visible")]
        Visible,

        /// <summary>
        /// presence error
        /// </summary>
        [Name("error")]
        Error,

        /// <summary>
        /// used in server to server protocol to request presences
        /// </summary>
        [Name("probe")]
        Probe
    }
}
