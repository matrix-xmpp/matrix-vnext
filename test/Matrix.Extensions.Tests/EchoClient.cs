namespace Matrix.Extensions.Tests
{   
    using System.Threading;
    using System.Threading.Tasks;
    using Matrix.Xmpp.Client;

    /// <summary>
    /// simple echo client for unit testing purposes
    /// </summary>
    class EchoClient : IClientIqSender
    {
        public Task<Iq> SendIqAsync(Iq iq)
        {
            return Task.FromResult(iq);
        }

        public Task<Iq> SendIqAsync(Iq iq, int timeout)
        {
            return Task.FromResult(iq);
        }

        public Task<Iq> SendIqAsync(Iq iq, CancellationToken cancellationToken)
        {
            return Task.FromResult(iq);
        }

        public Task<Iq> SendIqAsync(Iq iq, int timeout, CancellationToken cancellationToken)
        {
            return Task.FromResult(iq);
        }
    }
}
