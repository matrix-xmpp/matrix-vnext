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

using System.Collections.Generic;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Matrix.IO.Compression;
using Matrix.Attributes;

namespace Matrix.Network.Codecs
{
    /// <summary>
    /// Decoder which implements zlib compression
    /// </summary>
    [Name("Zlib-Decoder")]
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
                var decompressBuf = CompressionHelper.Decompress(inflater, message.ReadReadableBytes());
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
