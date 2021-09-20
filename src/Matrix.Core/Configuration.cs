namespace Matrix
{
    using Transport;

    public class Configuration
    {
        /// <summary>
        /// The transport which should be used for client
        /// </summary>
        public ITransport Transport { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether automatic reconnect is enabled or not
        /// </summary>
        public bool AutoReconnect { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use stream management or not
        /// </summary>
        public bool StreamManagement { get; set; }

        /// <summary>
        /// Enable auto reconnect
        /// </summary>
        /// <returns></returns>
        public Configuration UseAutoReconnect()
        {
            AutoReconnect = true;
            return this;
        }

        /// <summary>
        /// Enable the usage of stream management
        /// </summary>
        /// <returns></returns>
        public Configuration UseStreamManagement()
        {
            StreamManagement = true;
            return this;
        }
    }
}
