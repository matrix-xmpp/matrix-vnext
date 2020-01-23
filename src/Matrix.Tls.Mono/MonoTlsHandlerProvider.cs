namespace Matrix.Tls.Mono
{
    using System.Net.Security;
    using System.Threading.Tasks;
    using DotNetty.Transport.Channels;
    using Network;

    public class MonoTlsHandlerProvider : ITlsHandlerProvider
    {
        /// <summary>
        /// A default implementation of a <see cref="ITlsHandlerProvider"/>.
        /// </summary>
        public async Task<IChannelHandler> ProvideAsync(XmppClient xmppClient)
        {
            return await Task<IChannelHandler>.Run(async () =>
            {
                var tlsSettings = await xmppClient.TlsSettingsProvider.ProvideAsync(xmppClient);
                var tlsHandler = new TlsHandler(stream
                        => new SslStream(stream,
                            true,
                            (sender, certificate, chain, errors) =>
                                xmppClient.CertificateValidator.RemoteCertificateValidationCallback(sender, certificate,
                                    chain, errors)),
                    tlsSettings);

                return tlsHandler;
            });
        }
    }
}
