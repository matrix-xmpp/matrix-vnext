using System;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Matrix.Network.Handlers;
using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using Matrix.Xmpp.Stream;

namespace Server.Handlers
{
    public class SaslPlainHandler : XmppStanzaHandler, IStreamFeature
    {
        public void AddStreamFeatures(ServerConnectionHandler serverSession, StreamFeatures features)
        {
            if (serverSession.SessionState < Matrix.SessionState.Authenticated)
            {
                if (features.Mechanisms == null)
                    features.Mechanisms = new Mechanisms();

                features.Mechanisms.AddMechanism(SaslMechanism.Plain);
            }
        }
        public SaslPlainHandler()
        {
            Handle(
                el => el.OfType<Auth>()
                      && el.Cast<Auth>().SaslMechanism == SaslMechanism.Plain
                      ,
                  async (context, xmppXElement) =>
                {
                    var auth = xmppXElement.Cast<Auth>();
                    await ProcessSaslPlainAuth(context, auth);
                    context.Channel.Pipeline.Remove(this);
                });
        }

        private async Task ProcessSaslPlainAuth(IChannelHandlerContext context, Auth auth)
        {
            string pass = null;
            string user = null;

            byte[] bytes = Convert.FromBase64String(auth.Value);
            string sasl = Encoding.UTF8.GetString(bytes);
            // trim nullchars
            sasl = sasl.Trim((char)0);
            string[] split = sasl.Split((char)0);

            if (split.Length == 3)
            {
                user = split[1];
                pass = split[2];
            }
            else if (split.Length == 2)
            {
                user = split[0];
                pass = split[1];
            }

            string dbPass = "secret"; // TODO // Server.Storage.GetPassword(serverSession.XmppDomain, user);
            if (dbPass == null || pass != dbPass)
            {
                // user does not exist or wrong password
                await SendAsync(new Failure(FailureCondition.NotAuthorized));
            }
            else if (pass == dbPass)
            {
                var serverSession = context.Channel.Pipeline.Get<ServerConnectionHandler>();
                // pass correct
                serverSession.User = user;

                // stream reset
                serverSession.ResetStream();

                serverSession.SessionState = Matrix.SessionState.Authenticated;
                await SendAsync(new Success());
            }
        }
    }
}
