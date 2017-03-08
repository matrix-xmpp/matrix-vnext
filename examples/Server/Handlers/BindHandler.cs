using System;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Matrix;
using Matrix.Network.Handlers;
using Matrix.Xml;
using Matrix.Xmpp;
using Matrix.Xmpp.Bind;
using Matrix.Xmpp.Stream;
using Matrix.Xmpp.Client;

namespace Server.Handlers
{
    public class BindHandler : XmppStanzaHandler, IStreamFeature
    {
        public void AddStreamFeatures(ServerConnectionHandler serverSession, StreamFeatures features)
        {
           if (serverSession.SessionState < SessionState.Binded)
                features.Add(new Bind());
        }
        public BindHandler()
        {
            Handle(
                el =>
                    el.OfType<Iq>()
                    && el.Cast<Iq>().Type == IqType.Set
                    && el.Cast<Iq>().Query.OfType<Bind>(),

                async (context, xmppXElement) =>
                {
                    await ProcessStanza(context, xmppXElement.Cast<Iq>());
                    context.Channel.Pipeline.Remove(this);
                });
        }
        bool SecureResource { get; set; }

        public async Task ProcessStanza(IChannelHandlerContext context, Iq iq)
        {
            var bind = iq.Query as Bind;

            // read desired resource
            string res = bind.Resource;
            if (String.IsNullOrEmpty(res)
                || SecureResource)
            {
                // no resource given, assign random
                res = Guid.NewGuid().ToString();
            }

            var serverSession = context.Channel.Pipeline.Get<ServerConnectionHandler>();

            var jid = new Jid(serverSession.User, serverSession.XmppDomain, res);
            var resIq = new BindIq
            {
                Id = iq.Id,
                Type = Matrix.Xmpp.IqType.Result,
                Bind = { Jid = jid }
            };

            await SendAsync(resIq);

            serverSession.Resource = res;
            serverSession.SessionState = SessionState.Binded;

            // check if there is another session with this Jid already
            //if (Sessions.Exists(jid))
            //{
            //    Sessions.End(jid,
            //                      new Matrix.Xmpp.Stream.Error(Matrix.Xmpp.Stream.ErrorCondition.Conflict));
            //}

            //Sessions.Add(new ServerStream { Jid = jid, ServerSession = serverSession });
        }
    }
}
