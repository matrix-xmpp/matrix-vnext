using System;
using DotNetty.Transport.Channels;

namespace Matrix.Tests.ClientEnd2End
{  

    class ExceptionCatchHandler : ChannelHandlerAdapter
    {
        readonly Action<Exception> exceptionCaughtAction;

        public ExceptionCatchHandler(Action<Exception> exceptionCaughtAction)
        {            
            this.exceptionCaughtAction = exceptionCaughtAction;
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception) => this.exceptionCaughtAction(exception);
    }
}
