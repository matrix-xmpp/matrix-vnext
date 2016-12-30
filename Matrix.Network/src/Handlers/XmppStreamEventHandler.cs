using System;
using System.Reactive.Subjects;
using DotNetty.Transport.Channels;
using Matrix.Network.Codecs;
using Matrix.Xml;

namespace Matrix.Network.Handlers
{
    public class XmppStreamEventHandler : SimpleChannelInboundHandler<XmlStreamEvent>
    {
        readonly ISubject<XmppXElement> xmppXElementSubject = new Subject<XmppXElement>();
        
        public IObservable<XmppXElement> XmppXElementStream => xmppXElementSubject;

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            // thiget called when the socket gets closed
            base.ChannelInactive(context);
            xmppXElementSubject.OnCompleted();
        }

        //public override void ChannelRead(IChannelHandlerContext ctx, object msg)
        //{
        //    base.ChannelRead(ctx, msg);
        //}

        protected override void ChannelRead0(IChannelHandlerContext ctx, XmlStreamEvent msg)
        {
            if (msg.XmlStreamEventType == XmlStreamEventType.StreamStart)
            {
                xmppXElementSubject.OnNext(msg.XmppXElement);
            }
            else if (msg.XmlStreamEventType == XmlStreamEventType.StreamElement)
            {
                xmppXElementSubject.OnNext(msg.XmppXElement);
            }
            else if (msg.XmlStreamEventType == XmlStreamEventType.StreamEnd)
            {
                //xmppXElementSubject.OnCompleted();
            }
            else if (msg.XmlStreamEventType == XmlStreamEventType.StreamError)
            {
                xmppXElementSubject.OnError(msg.Exception);
            }

            ctx.FireChannelRead(msg.XmppXElement);
        }
    }
}
