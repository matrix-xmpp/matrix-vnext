using System.Collections.Generic;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Matrix.Xml.Parser;

namespace Matrix.DotNetty.Codecs
{
    public class XmlStreamDecoder : MessageToMessageDecoder<IByteBuffer>
    {
        readonly StreamParser _parser = new StreamParser();

        private List<object> _output;

        public XmlStreamDecoder()
        {
            _parser.OnStreamStart +=
                element =>
                    _output?.Add(new XmlStreamEvent(XmlStreamEventType.StreamStart, element));

            _parser.OnStreamElement +=
                element =>
                    _output?.Add(new XmlStreamEvent(XmlStreamEventType.StreamElement, element));

            _parser.OnStreamEnd += () =>
                _output?.Add(new XmlStreamEvent(XmlStreamEventType.StreamEnd));

            _parser.OnStreamError +=
                exception => _output?.Add(new XmlStreamEvent(XmlStreamEventType.Error, exception));
        }

        public void Reset()
        {
            _parser.Reset();
        }
      

        protected override void Decode(IChannelHandlerContext context, IByteBuffer message, List<object> output)
        {
            _output = output;
            _parser.Write(message.ToArray());
            _output = null;
        }
    }
}
