using System;
using System.Collections.Generic;
using System.Text;
using DotNetty.Transport.Channels;
using Matrix.Network.Codecs;
using Matrix.Network.Handlers;

namespace Matrix
{
    public class XmppConnection
    {
        IChannelPipeline pipeline;
        XmlStreamDecoder xmlStreamDecoder = new XmlStreamDecoder();

        XmppStreamEventHandler xmppStreamEventHandler = new XmppStreamEventHandler();

        public IqHandler IqHandler { get; } = new IqHandler();

        public WaitForStanzaHandler WaitForStanzaHandler { get; } = new WaitForStanzaHandler();
    }
}
