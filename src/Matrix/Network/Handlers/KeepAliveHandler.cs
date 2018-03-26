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

using System.Threading;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Matrix.Attributes;

namespace Matrix.Network.Handlers
{
    /// <summary>
    /// a Handler to keep the socket conenction alive by sending a space character every 2 minutes over the existing socket
    /// </summary>
    [Name("KeepAlive-Handler")]
    public class KeepAliveHandler : ChannelHandlerAdapter
    {
        private const string Whitespace = " ";

        private Timer keepaliveTimer;
        public int KeepAliveInterval => TimeConstants.TwoMinutes;


        public override void ChannelActive(IChannelHandlerContext context)
        {
            base.ChannelActive(context);
            keepaliveTimer = new Timer(async state =>  await context.Channel.WriteAndFlushAsync(Whitespace), null, KeepAliveInterval, KeepAliveInterval);
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            base.ChannelInactive(context);
            keepaliveTimer.Dispose();
            keepaliveTimer = null;
        }
        
        public override Task WriteAsync(IChannelHandlerContext ctx, object msg)
        {
            if (KeepAliveInterval > 0)
            { 
                keepaliveTimer?.Change(KeepAliveInterval, KeepAliveInterval);
            }

            return ctx.WriteAsync(msg);
        }
    }
}
