namespace Matrix
{
    public enum SessionState
    {
        /// <summary>
        /// The socket is disconnected
        /// </summary>
        Disconnected,

        /// <summary>
        /// the socket is Connected
        /// </summary>
        Connected,

        /// <summary>
        /// The socket gets upgraded to TLS
        /// </summary>
        Securing,

        /// <summary>
        /// The socket was updates to TLS successful
        /// </summary>
        Secure,

        /// <summary>
        /// Authentication is in progress
        /// </summary>
        Authenticating,

        /// <summary>
        /// Session is authenticated
        /// </summary>
        Authenticated,

        /// <summary>
        /// Resource binding is in progress
        /// </summary>
        Binding,

        /// <summary>
        /// Resource binding finsihed with success
        /// </summary>
        Binded,
    }
}
