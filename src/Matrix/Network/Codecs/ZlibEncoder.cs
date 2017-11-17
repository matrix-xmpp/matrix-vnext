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
    /// Encoder which implements zlib compression
    /// </summary>
    [Name("Zlib-Encoder")]
    public class ZlibEncoder : MessageToMessageEncoder<IByteBuffer>, IActive
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
                var buf = CompressionHelper.Compress(deflater, message.ReadReadableBytes());
                output.Add(context.Allocator.Buffer(buf.Length).WriteBytes(buf));
            }
            else
            {
                output.Add(message.Retain());
            }
        }
    }
}
