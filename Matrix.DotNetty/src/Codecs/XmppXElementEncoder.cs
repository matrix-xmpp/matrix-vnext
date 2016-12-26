using System.Collections.Generic;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

using Matrix.Xml;

namespace Matrix.DotNetty.Codecs
{
    public class XmppXElementEncoder : MessageToMessageEncoder<XmppXElement>
    {
        public override bool IsSharable => true;

        protected override void Encode(IChannelHandlerContext context, XmppXElement message, List<object> output)
        {
            output.Add(
                ByteBufferUtil.EncodeString(context.Allocator, message.ToString(false), Encoding.UTF8)
                );
        }
    }
}
