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

using System;
using System.Reactive.Subjects;
using DotNetty.Transport.Channels;
using Matrix.Network.Codecs;
using Matrix.Xml;
using Matrix.Attributes;

namespace Matrix.Network.Handlers
{
    [Name("XmppStreamEvent-Handler")]
    public class XmppStreamEventHandler : SimpleChannelInboundHandler<XmlStreamEvent>
    {
        public override bool IsSharable => true;

        private ISubject<XmppXElement> xmppXElementStreamSubject = new Subject<XmppXElement>();
        private ISubject<XmlStreamEvent> xmlStreamEventSubject = new Subject<XmlStreamEvent>();

        public IObservable<XmppXElement> XmppXElementStream => xmppXElementStreamSubject;
        public IObservable<XmlStreamEvent> XmlStreamEvent => xmlStreamEventSubject;

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
                // ignore here
            }
            else if (msg.XmlStreamEventType == XmlStreamEventType.StreamError)
            {
                // ignore here
            }

            ctx.FireChannelRead(msg.XmppXElement);
        }
    }
}
