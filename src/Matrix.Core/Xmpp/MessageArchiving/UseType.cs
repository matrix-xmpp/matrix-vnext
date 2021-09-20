namespace Matrix.Xmpp.MessageArchiving
{
    public enum UseType
    {
        /// <summary>
        /// this method MAY be used if no other methods are available.
        /// </summary>
        Concede,

        /// <summary>
        /// this method MUST NOT be used.
        /// </summary>
        Forbid,

        /// <summary>
        /// this method SHOULD be used if available.
        /// </summary>
        Prefer
    }
}
