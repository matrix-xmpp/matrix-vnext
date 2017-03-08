using DotNetty.Handlers.Tls;
using Matrix.Network.Handlers;
using Matrix.Xml;
using Matrix.Xmpp.Stream;
using Matrix.Xmpp.Tls;

namespace Server.Handlers
{
    public class StartTlsHandler : XmppStanzaHandler, IStreamFeature
    {
        private ServerConnectionHandler ServerSession;
        public void AddStreamFeatures(ServerConnectionHandler serverSession, StreamFeatures features)
        {
            ServerSession = serverSession;
            if (serverSession.SessionState < Matrix.SessionState.Secure)
                features.Add(new StartTls());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StartTlsHandler" /> class.
        /// </summary>
        /// <param name="certificateProvider">The certificate provider.</param>
        public StartTlsHandler(ICertificateProvider certificateProvider)
        {
            Handle(
               el => el.OfType<StartTls>(),

               async (context, xmppXElement) =>
               {                  
                   var serverSession = context.Channel.Pipeline.Get<ServerConnectionHandler>();

                    serverSession.ResetStream();
                   await SendAsync(new Proceed());

                   var certConfig = ServerSettings.Certificate(serverSession.XmppDomain);
                   var tlsCertificate =  certificateProvider.RequestCertificate(serverSession.XmppDomain);
                   context.Channel.Pipeline.AddFirst(TlsHandler.Server(tlsCertificate));

                   serverSession.SessionState = Matrix.SessionState.Secure;
               });         
        }
    }
}
