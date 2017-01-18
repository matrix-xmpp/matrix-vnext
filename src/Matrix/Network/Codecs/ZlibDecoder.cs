using System.Collections.Generic;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Matrix.IO.Compression;

namespace Matrix.Network.Codecs
{
    /// <summary>
    /// Decoder which implements zlib compression
    /// </summary>
    public class ZlibDecoder : MessageToMessageDecoder<IByteBuffer>
    {
        /// <summary>
        /// is used to compress data
        /// </summary>
        private readonly Inflater inflater;

        public override bool IsSharable => true;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ZlibDecoder" /> is active.
        /// </summary>
        /// <value>
        /// <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; set; }

        public ZlibDecoder()
        {
            inflater = new Inflater();
        }

        public ZlibDecoder(bool active) : this()
        {
            Active = active;
        }

        protected override void Decode(IChannelHandlerContext context, IByteBuffer message, List<object> output)
        {
            if (Active)
            {
                var decompressBuf = CompressionHelper.Decompress(inflater, message.ToArray());
                var buf = context.Allocator.Buffer(decompressBuf.Length).WriteBytes(decompressBuf);
                output.Add(buf);
            }
            else
            {
                output.Add(message.Retain());
            }
        }
    }
}
