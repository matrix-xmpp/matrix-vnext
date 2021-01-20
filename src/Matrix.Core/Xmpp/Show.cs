using Matrix.Attributes;

namespace Matrix.Xmpp
{
    /// <summary>
    /// Enumeration that represents the online state.
    /// </summary>
    public enum Show
    {
        /// <summary>
        /// 
        /// </summary>
        None = -1,

        /// <summary>
        /// The entity or resource is temporarily away.
        /// </summary>
        [Name("away")]
        Away,

        /// <summary>
        /// The entity or resource is actively interested in chatting.
        /// </summary>
        [Name("chat")]
        Chat,

        /// <summary>
        /// The entity or resource is busy (dnd = "Do Not Disturb").
        /// </summary>
        [Name("dnd")]
        DoNotDisturb,

        /// <summary>
        /// The entity or resource is away for an extended period (xa = "eXtended Away").
        /// </summary>
        [Name("xa")]
        ExtendedAway,
    }
}
