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
using Matrix.Xml.Parser;
using Matrix.Attributes;

namespace Matrix.Network.Codecs
{
    [Name("XmlStream-Decoder")]
    public class XmlStreamDecoder : MessageToMessageDecoder<IByteBuffer>
    {
        readonly StreamParser parser = new StreamParser();

        private List<object> output;
       
        public XmlStreamDecoder()
        {
            parser.OnStreamStart +=
                element =>
                    output?.Add(new XmlStreamEvent(XmlStreamEventType.StreamStart, element));

            parser.OnStreamElement +=
                element =>
                    output?.Add(new XmlStreamEvent(XmlStreamEventType.StreamElement, element));

            parser.OnStreamEnd += () =>
                output?.Add(new XmlStreamEvent(XmlStreamEventType.StreamEnd));

            parser.OnStreamError +=
                exception => output?.Add(new XmlStreamEvent(XmlStreamEventType.StreamError, exception));
        }

        public void Reset()
        {
            parser.Reset();
        }

        protected override void Decode(IChannelHandlerContext context, IByteBuffer message, List<object> output)
        {
            this.output = output;
            parser.Write(message.ReadReadableBytes());
            this.output = null;
        }
    }
}
