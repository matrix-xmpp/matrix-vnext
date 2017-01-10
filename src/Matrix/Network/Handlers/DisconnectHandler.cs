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
