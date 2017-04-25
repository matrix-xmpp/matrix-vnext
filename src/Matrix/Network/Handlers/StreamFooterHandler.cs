/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
 * See the COPYING file for more information.
 *
 * This file is part of the MatriX project.
 *
 * NOTICE: All information contained herein is, and remains the property
 * of AG-Software and its suppliers, if any.
 * The intellectual and technical concepts contained herein are proprietary
 * to AG-Software and its suppliers and may be covered by German and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 *
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from AG-Software.
 *
 * Contact information for AG-Software is available at http://www.ag-software.de
 */

using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Matrix.Network.Codecs;
using Matrix.Xmpp.Base;
using Matrix.Attributes;

namespace Matrix.Network.Handlers
{
    [Name("StreamFooter-Handler")]
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
                    await ctx.WriteAsync(streamFooter);
                
                if (ctx.Channel.Open)
                { 
                    await ctx.CloseAsync();
                    return;
                }
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
                if (sent != null && sent.StartsWith(streamFooter))
                    sentStreamFooter = true;
            }
            return context.WriteAsync(message);
        }
    }
}
