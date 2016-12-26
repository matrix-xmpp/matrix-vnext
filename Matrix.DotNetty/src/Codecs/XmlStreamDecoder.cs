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

        private List<object> _output = null;

        public XmlStreamDecoder()
        {
            _parser.OnStreamStart +=
                element =>
                    _output?.Add(new Xml.Parser.XmlStreamEvent(Xml.Parser.XmlStreamEventType.StreamStart, element));

            _parser.OnStreamElement +=
                element =>
                    _output?.Add(new Xml.Parser.XmlStreamEvent(Xml.Parser.XmlStreamEventType.StreamElement, element));

            _parser.OnStreamEnd += () =>
                _output?.Add(new Xml.Parser.XmlStreamEvent(Xml.Parser.XmlStreamEventType.StreamEnd));

            _parser.OnStreamError +=
                exception => _output?.Add(new Xml.Parser.XmlStreamEvent(Xml.Parser.XmlStreamEventType.Error, exception));
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
