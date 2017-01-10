namespace Matrix.Network.Codecs
{
    public enum XmlStreamEventType
    {
        /// <summary>
        /// stream header received
        /// </summary>
        StreamStart,

        /// <summary>
        /// stream footer received
        /// </summary>
        StreamEnd,

        /// <summary>
        /// xmpp stanza/element received
        /// </summary>
        StreamElement,

        /// <summary>
        /// Event for XML  errors
        /// </summary>
        StreamError,
    }
}
