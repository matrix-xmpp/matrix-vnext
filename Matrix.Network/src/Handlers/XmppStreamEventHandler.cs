using System;
using System.Reactive.Subjects;
using DotNetty.Transport.Channels;
using Matrix.Network.Codecs;
using Matrix.Xml;

namespace Matrix.Network.Handlers
{
    public class XmppStreamEventHandler : SimpleChannelInboundHandler<XmlStreamEvent>
    {
        readonly ISubject<XmppXElement> _xmppXElementSubject = new Subject<XmppXElement>();
        
        public IObservable<XmppXElement> XmppXElementStream => _xmppXElementSubject;

        protected override void ChannelRead0(IChannelHandlerContext ctx, XmlStreamEvent msg)
        {
            if (msg.XmlStreamEventType == XmlStreamEventType.StreamStart)
            {
                _xmppXElementSubject.OnNext(msg.XmppXElement);
            }
            else if (msg.XmlStreamEventType == XmlStreamEventType.StreamElement)
            {
                _xmppXElementSubject.OnNext(msg.XmppXElement);
            }
            else if (msg.XmlStreamEventType == XmlStreamEventType.StreamEnd)
            {
                _xmppXElementSubject.OnCompleted();
            }
            else if (msg.XmlStreamEventType == XmlStreamEventType.Error)
            {
                
            }
            else if (msg.XmlStreamEventType == XmlStreamEventType.StreamError)
            {
                
            }

            ctx.FireChannelRead(msg.XmppXElement);
        }
    }
}
