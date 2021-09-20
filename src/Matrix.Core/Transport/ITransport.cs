namespace Matrix.Transport
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Xml;

    public interface ITransport
    {
        /// <summary>
        /// Connect the transport to the given uri
        /// </summary>
        /// <param name="xmppDomain"></param>
        /// <returns></returns>
        Task ConnectAsync(string xmppDomain);

        /// <summary>
        /// Disconnect the transport
        /// </summary>
        /// <returns></returns>
        Task DisconnectAsync();

        /// <summary>
        /// Send xml over the transport
        /// </summary>
        /// <param name="xmppXElement">the Xml element to send over the transport</param>
        /// <returns></returns>
        Task SendAsync(XmppXElement xmppXElement);


        /// <summary>
        ///  Send bytes over the transport
        /// </summary>
        /// <param name="xmppXElement">the Xml element to send over the transport</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SendAsync(XmppXElement xmppXElement, CancellationToken cancellationToken);

        // Observers
        IObservable<XmppXElement> XmlReceived { get; }
        IObservable<XmppXElement> BeforeXmlSent { get; }
        IObservable<XmppXElement> XmlSent { get; }
        
        IObservable<byte[]> DataReceived { get; }
        IObservable<byte[]> DataSent { get; }
        IObservable<State> StateChanged { get; }

        IResolver Resolver { get; set; }

        /// <summary>
        /// Does the transport support the StartTls command
        /// </summary>
        bool SupportsStartTls { get; }

        Task InitTls(string xmppDomain);

        XmppXElement GetStreamHeader(string to, string version);
        XmppXElement GetStreamFooter();
    }
}
