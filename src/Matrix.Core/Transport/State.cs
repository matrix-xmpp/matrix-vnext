namespace Matrix.Transport
{
    public enum State
    {
        /// <summary>
        /// Transport is in disconnected state
        /// </summary>
        Disconnected = 0,
        
        /// <summary>
        /// Transport is connected
        /// </summary>
        Connected,
        
        /// <summary>
        /// The stream footer was sent
        /// </summary>
        StreamFooterSent,

        /// <summary>
        /// The stream footer was received
        /// </summary>
        StreamFooterReceived,
    }
}
