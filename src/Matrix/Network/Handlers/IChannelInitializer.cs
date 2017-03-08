using DotNetty.Transport.Channels;

namespace Matrix.Network.Handlers
{
    public interface IChannelInitializer
    {
        IChannelPipeline Initialize(IChannelPipeline pipeline);
    }
}
