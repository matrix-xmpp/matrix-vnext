namespace Matrix.Xmpp.MessageArchiveManagement
{
    using Attributes;

    public enum DefaultPreference
    {
        /// <summary>
        /// all messages are archived by default.
        /// </summary>
        [Name("always")]
        Always,

        /// <summary>
        /// messages are never archived by default.
        /// </summary>
        [Name("never")]
        Never,

        /// <summary>
        /// Messages are archived only if the contact's bare JID is in the user's roster.
        /// </summary>
        [Name("roster")]
        Roster
    }
}