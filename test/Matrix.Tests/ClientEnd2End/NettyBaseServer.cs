using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;

using System;
using System.Net;
using System.Threading.Tasks;

namespace Matrix.Tests.ClientEnd2End
{
    public class NettyBaseServer
    {
        public static readonly  TimeSpan    ShutdownTimeout = TimeSpan.FromSeconds(10);
        public static readonly  int         Port            = 5222;

        public async Task<Func<Task>> StartServerAsync(bool tcpNoDelay, Action<IChannel> childHandlerSetupAction, TaskCompletionSource<bool> testPromise)
        {
            var bossGroup = new MultithreadEventLoopGroup(1);
            var workerGroup = new MultithreadEventLoopGroup();
            bool started = false;
            try
            {
                ServerBootstrap b = new ServerBootstrap()
                    .Group(bossGroup, workerGroup)
                    .Channel<TcpServerSocketChannel>()
                    .Handler(new ExceptionCatchHandler(ex => testPromise.TrySetException(ex)))
                    .ChildHandler(new ActionChannelInitializer<ISocketChannel>(childHandlerSetupAction))
                    .ChildOption(ChannelOption.TcpNodelay, tcpNoDelay)
                    .ChildOption(ChannelOption.SoRcvbuf, 4096);

                IChannel serverChannel = await b.BindAsync(IPAddress.Any, Port);
                                
                started = true;

                return async () =>
                {
                    try
                    {
                        await serverChannel.CloseAsync();
                    }
                    finally
                    {
                        await bossGroup.ShutdownGracefullyAsync();
                        await workerGroup.ShutdownGracefullyAsync();
                    }
                };
            }
            finally
            {
                if (!started)
                {
                    await bossGroup.ShutdownGracefullyAsync();
                    await workerGroup.ShutdownGracefullyAsync();
                }
            }
        }
    }
}
