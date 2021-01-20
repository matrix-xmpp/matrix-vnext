namespace Matrix.Xmpp.Chatstates
{
    /// <summary>
    /// Enumeration of supported Chatstates (JEP-0085)
    /// </summary>
    public enum Chatstate
    {
        /// <summary>
        /// No Chatstate at all
        /// </summary>
        None,

        /// <summary>
        /// Active Chatstate
        /// </summary>
        Active,

        /// <summary>
        /// Inactive Chatstate
        /// </summary>
        Inactive,

        /// <summary>
        /// Composing Chatstate
        /// </summary>
        Composing,

        /// <summary>
        /// Gone Chatstate
        /// </summary>
        Gone,

        /// <summary>
        /// Paused Chatstate
        /// </summary>
        Paused
    }
}
