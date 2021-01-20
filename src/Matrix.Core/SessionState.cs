namespace Matrix
{
    public enum SessionState
    {
        /// <summary>
        /// The session is disconnected
        /// </summary>
        Disconnected = 0,

        /// <summary>
        /// the session is Connected
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
        /// Register new Account in progress
        /// </summary>
        Registering,

        /// <summary>
        /// New account ws greistered
        /// </summary>
        Registered,

        /// <summary>
        /// Authentication is in progress
        /// </summary>
        Authenticating,

        /// <summary>
        /// Session is authenticated
        /// </summary>
        Authenticated,

        /// <summary>
        /// Stream resume failed
        /// </summary>
        ResumeFailed,

        /// <summary>
        /// Negotiating stream compression
        /// </summary>
        Compressing,

        /// <summary>
        /// Stream compression is enabled
        /// </summary>
        Compressed,

        /// <summary>
        /// Resource binding is in progress
        /// </summary>
        Binding,

        /// <summary>
        /// Resource binding finsihed with success
        /// </summary>
        Binded,

        /// <summary>
        /// Trying to resume the previous stream
        /// </summary>
        Resuming,

        /// <summary>
        /// Stream was resumed
        /// </summary>
        Resumed,

        /// <summary>
        /// The session is in process of getting disconnected
        /// </summary>
        Disconnecting,
    }
}
