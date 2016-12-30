using System;
using System.Collections.Generic;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Matrix.Xml.Parser;

namespace Matrix.Network.Codecs
{
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
            parser.Write(message.ToArray());
            this.output = null;
        }
    }
}
