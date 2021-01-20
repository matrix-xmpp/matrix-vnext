namespace Matrix.Transport
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a resolver which allow us to pass the Uri of the server manual.
    /// This is useful when the server does not implement SRV records or is not using
    /// XEP-0156 for discovering alternative XMPP connection methods.
    /// Or if you just want to disable automatic discovery and override with your custom settings.
    /// </summary>
    public class StaticNameResolver : IResolver
    {
        private readonly Uri uri;

        public StaticNameResolver(Uri uri)
        {
            this.uri = uri;
        }

        public async Task<Uri> ResolveUriAsync(string xmppDomain)
        {
            return await Task.FromResult<Uri>(uri).ConfigureAwait(false);
        }
    }
}
