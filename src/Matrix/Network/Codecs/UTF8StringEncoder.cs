using System.Text;
using System.Collections.Generic;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace Matrix.Network.Codecs
{
    public class UTF8StringEncoder : MessageToMessageEncoder<string>
    {
        public override bool IsSharable => true;
        protected override void Encode(IChannelHandlerContext context, string message, List<object> output)
        {
            if (message.Length == 0)
                return;

            output.Add(ByteBufferUtil.EncodeString(context.Allocator, message, Encoding.UTF8));
        }
    }
}
