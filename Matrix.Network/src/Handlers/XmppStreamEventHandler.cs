using System;
using System.Reactive.Subjects;
using DotNetty.Transport.Channels;
using Matrix.Network.Codecs;
using Matrix.Xml;

namespace Matrix.Network.Handlers
{
    public class XmppStreamEventHandler : SimpleChannelInboundHandler<XmlStreamEvent>
    {
        readonly ISubject<XmppXElement>     xmppXElementStreamSubject   = new Subject<XmppXElement>();
        readonly ISubject<XmlStreamEvent>   xmlStreamEventSubject       = new Subject<XmlStreamEvent>();

        public IObservable<XmppXElement>    XmppXElementStream          => xmppXElementStreamSubject;
        public IObservable<XmlStreamEvent>  XmlStreamEvent              => xmlStreamEventSubject;

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            // this gets called when the socket gets closed
            base.ChannelInactive(context);
            xmppXElementStreamSubject.OnCompleted();
        }

       
        protected override void ChannelRead0(IChannelHandlerContext ctx, XmlStreamEvent msg)
        {
            xmlStreamEventSubject.OnNext(msg);

            if (msg.XmlStreamEventType == XmlStreamEventType.StreamStart)
            {
                xmppXElementStreamSubject.OnNext(msg.XmppXElement);
            }
            else if (msg.XmlStreamEventType == XmlStreamEventType.StreamElement)
            {
                xmppXElementStreamSubject.OnNext(msg.XmppXElement);
            }
            else if (msg.XmlStreamEventType == XmlStreamEventType.StreamEnd)
            {
                //xmppXElementStreamSubject.OnCompleted();
            }
            else if (msg.XmlStreamEventType == XmlStreamEventType.StreamError)
            {
                xmppXElementStreamSubject.OnError(msg.Exception);
            }

            ctx.FireChannelRead(msg.XmppXElement);
        }
    }
}
