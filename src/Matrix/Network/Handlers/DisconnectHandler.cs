/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
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

using DotNetty.Transport.Channels;

namespace Matrix.Network.Handlers
{
    public class DisconnectHandler : ChannelHandlerAdapter//InboundHandler
    {
        public override void ChannelInactive(IChannelHandlerContext context)
        {
            base.ChannelInactive(context);

            var channel = context.Channel;
          
            /* If shutdown is on going, ignore */
            if (channel.EventLoop.IsShuttingDown)
                return;

            //ReconnectionTask reconnect = new ReconnectionTask(channel);
            //reconnect.run();
        }

        //public override void Read(IChannelHandlerContext context)
        //{
        //    //base.Read(context);
        //}

        //public override void ChannelRead(IChannelHandlerContext context, object message)
        //{
        //    //base.ChannelRead(context, message);
        //}

        /*
         public class ReconnectionTask extends Runnable, ChannelFutureListener {

            Channel previous;

            public ReconnectionTask(Channel c) {
                this.previous = c;
            }

            @Override
            public void run() {
                 Bootstrap b = createBootstrap();
                 b.remoteAddress(previous.remoteAddress())
                  .connect()
                  .addListener(this);
            }

            @Override
            public void operationComplete(ChannelFuture future) throws Exception {
                if (!future.isSuccess()) {
                    // Will try to connect again in 100 ms.
                    // Here you should probably use exponential backoff or some sort of randomization to define the retry period.
                    previous.eventLoop()
                            .schedule(this, 100, MILLISECONDS); 
                    return;
                }
                // Do something else when success if needed.
            }
        }
        
         */
        //public override void ChannelRead(IChannelHandlerContext context, object message)
        //{
        //    base.ChannelRead(context, message);
        //    //context.FireChannelRead(message);
            
        //}
    }
}
