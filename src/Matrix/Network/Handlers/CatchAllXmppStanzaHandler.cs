using DotNetty.Transport.Channels;
using Matrix.Xml;

namespace Matrix.Network.Handlers
{
    public class CatchAllXmppStanzaHandler : XmppStanzaHandler
    {
        public override bool IsSharable => true;

        protected override void ChannelRead0(IChannelHandlerContext ctx, XmppXElement msg)
        {
            // only responsible for cacthing the last packet o nthe pipeline
        }

        public static string Name => "CatchAllXmppStanzaHandler";
    }
}
