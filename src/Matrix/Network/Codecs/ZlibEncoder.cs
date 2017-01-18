using System.Collections.Generic;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Matrix.IO.Compression;

namespace Matrix.Network.Codecs
{
    public class ZlibEncoder : MessageToMessageEncoder<IByteBuffer>
    {
        /// <summary>
        /// is used to compress data
        /// </summary>
        private readonly Deflater deflater;

        public bool Active { get; set; } 
     
        public ZlibEncoder()
        {
            deflater = new Deflater();
        }

        public ZlibEncoder(bool active) : this()
        {
            Active = active;
        }

        protected override void Encode(IChannelHandlerContext context, IByteBuffer message, List<object> output)
        {
            if (Active)
            {
                var buf = CompressionHelper.Compress(deflater, message.ToArray());
                output.Add(context.Allocator.Buffer(buf.Length).WriteBytes(buf));
            }
            else
            {
                output.Add(message.Retain());
            }
        }
    }
}
