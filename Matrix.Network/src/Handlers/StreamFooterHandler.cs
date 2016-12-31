// Copyright (c)  AG-Software. All Rights Reserved.
// by Alexander Gnauck (alex@ag-software.net)

using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Matrix.Network.Codecs;
using Matrix.Xmpp.Base;

namespace Matrix.Network.Handlers
{
    public class StreamFooterHandler : SimpleChannelInboundHandler<XmlStreamEvent>
    {
        private readonly string streamFooter = new Stream().EndTag();
        private bool sentStreamHeader;
        private bool sentStreamFooter;

        public override void ChannelActive(IChannelHandlerContext context)
        {
            base.ChannelActive(context);
            sentStreamHeader = sentStreamFooter = false;
        }

        protected override async void ChannelRead0(IChannelHandlerContext ctx, XmlStreamEvent msg)
        {
            if (msg.XmlStreamEventType == XmlStreamEventType.StreamEnd)
            {
                if (!sentStreamFooter && sentStreamHeader && ctx.Channel.IsWritable)
                    await ctx.Channel.Pipeline.WriteAsync(streamFooter);
            }

            ctx.FireChannelRead(msg);
        }

        public override Task WriteAsync(IChannelHandlerContext context, object message)
        {
            var sent = message as string;
            if (!sentStreamHeader)
            {
                if (sent != null && sent.StartsWith("<stream:stream"))
                    sentStreamHeader = true;
            }
            if (!sentStreamFooter)
            {
                if (sent != null && sent.Equals(streamFooter))
                    sentStreamFooter = true;
            }
            return context.WriteAsync(message);
        }
    }
}