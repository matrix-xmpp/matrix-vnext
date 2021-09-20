namespace Matrix
{    
    using System.Threading;
    using System.Threading.Tasks;

    using Matrix.Xmpp.Client;

    public interface IClientIqSender
    {
        /// <summary>
        /// Send an Iq asynchronous to the server
        /// </summary>
        /// <param name="iq"></param>
        /// <returns>The server response Iq</returns>
        Task<Iq> SendIqAsync(Iq iq);

        /// <summary>
        /// Send an Iq asynchronous to the server
        /// </summary>
        /// <param name="iq"></param>
        /// <param name="timeout"></param>
        /// <returns>The server response Iq</returns>
        Task<Iq> SendIqAsync(Iq iq, int timeout);

        /// <summary>
        /// Send an Iq asynchronous to the server
        /// </summary>
        /// <param name="iq"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The server response Iq</returns>
        Task<Iq> SendIqAsync(Iq iq, CancellationToken cancellationToken);
        
        /// <summary>
        /// Send an Iq asynchronous to the server
        /// </summary>
        /// <param name="iq"></param>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The server response Iq</returns>
        Task<Iq> SendIqAsync(Iq iq, int timeout, CancellationToken cancellationToken);        
    }
}
