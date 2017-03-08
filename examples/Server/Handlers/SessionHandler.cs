using Matrix;
using Matrix.Network.Handlers;
using Matrix.Xml;
using Matrix.Xmpp;
using Matrix.Xmpp.Stream;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Session;

namespace Server.Handlers
{
    public class SessionHandler : XmppStanzaHandler, IStreamFeature
    {
        public void AddStreamFeatures(ServerConnectionHandler serverSession, StreamFeatures features)
        {
            if (serverSession.SessionState < SessionState.Binded)
                features.Add(new Session());
        }

        public SessionHandler()
        {
            Handle(
                el =>
                    el.OfType<Iq>()
                    && el.Cast<Iq>().Type == IqType.Set
                    && el.Cast<Iq>().Query.OfType<Session>(),

                async (context, xmppXElement) =>
                {
                    /*            
                        <iq type="set" id="aabca" >
                            <session xmlns="urn:ietf:params:xml:ns:xmpp-session"/>
                        </iq>            
                     */
                    await SendAsync(new Iq { Id = xmppXElement.Cast<Iq>().Id, Type = IqType.Result });
                    context.Channel.Pipeline.Remove(this);
                });
        }
    }
}
