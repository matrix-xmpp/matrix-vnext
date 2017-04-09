using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Attributes;

namespace Matrix.Network.Handlers
{
    [Name("StreamManagement-Handler")]
    public class StreamManagementHandler : XmppStanzaHandler
    {
        private int countStanzasIn;
        private int countStanzasOut;

        public StreamManagementHandler()
        {
            Handle(
                el =>
                    el.OfType<Iq>()
                    || el.OfType<Presence>()
                    || el.OfType<Message>(),

                (context, xmppXElement) =>
                {
                    // count incoming stanzas
                    countStanzasIn++;                    
                });
        }
        
        public override Task WriteAsync(IChannelHandlerContext context, object message)
        {
            if (message is XmppXElement)
            {
                var el = message as XmppXElement;
                if (
                    el.OfType<Iq>()
                    || el.OfType<Presence>()
                    || el.OfType<Message>()
                    )
                {
                    countStanzasOut++;
                }
            }
            //var writeObj = message as XmppXElement;
            return base.WriteAsync(context, message);
        }
    }
}
