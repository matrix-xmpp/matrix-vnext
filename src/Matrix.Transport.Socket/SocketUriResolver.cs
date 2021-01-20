namespace Matrix.Transport.Socket
{
    using System;
    using System.Threading.Tasks;
    using Srv;

    public class SocketUriResolver : IResolver
    {
        private readonly SrvResolver resolver = new SrvResolver();

        public Task<Uri> ResolveUriAsync(string xmppDomain)
        {
            return resolver.ResolveClientUriAsync(xmppDomain);
        }
    }
}
