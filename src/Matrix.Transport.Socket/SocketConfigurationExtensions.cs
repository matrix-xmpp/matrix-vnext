namespace Matrix.Transport.Socket
{
    public static class SocketConfigurationExtensions
    {
        /// <summary>
        /// Use WebSocket Transport
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="resolver"></param>
        /// /// <param name="certificateValidator"></param>
        /// <returns></returns>
        public static Matrix.Configuration UseSocketTransport(
            this Configuration configuration,
            IResolver resolver,
            ICertificateValidator certificateValidator)
        {
            configuration.Transport = new SocketTransport
            {
                Resolver = resolver,
                CertificateValidator = certificateValidator
            };
            return configuration;
        }

        /// <summary>
        /// Use WebSocket Transport
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="resolver"></param>
        /// <returns></returns>
        public static Matrix.Configuration UseSocketTransport(
            this Configuration configuration,
            IResolver resolver)
        {
            configuration.Transport = new SocketTransport
            {
                Resolver = resolver
            };
            return configuration;
        }

        /// <summary>
        /// Use WebSocket Transport
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static Configuration UseSocketTransport(
            this Configuration configuration)
        {
            configuration.Transport = new SocketTransport();
            return configuration;
        }

        /// <summary>
        /// Sets the resolver for this socket transport
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="resolver"></param>
        /// <returns></returns>
        public static Configuration WithResolver (
            this Configuration configuration,
            IResolver resolver)
        {
            ((SocketTransport)configuration.Transport).Resolver = resolver;
            return configuration;
        }

        /// <summary>
        /// Sets the certificate validator for this socket transport
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="certificateValidator"></param>
        /// <returns></returns>
        public static Configuration WithCertificateValidator(
            this Configuration configuration,
            ICertificateValidator certificateValidator)
        {
            ((SocketTransport)configuration.Transport).CertificateValidator = certificateValidator;
            return configuration;
        }
    }
}
