using System;
using Matrix.Xml;

namespace Matrix.DotNetty.Codecs
{
    public class XmlStreamEvent
    {
        public XmlStreamEvent(XmlStreamEventType xmlStreamEventType)
        {
            XmlStreamEventType = xmlStreamEventType;
        }
        public XmlStreamEvent(XmlStreamEventType xmlStreamEventType, XmppXElement xmppXElement)
            : this(xmlStreamEventType)
        {
            XmppXElement = xmppXElement;
        }

        public XmlStreamEvent(XmlStreamEventType xmlStreamEventType, Exception exception)
             : this(xmlStreamEventType)
        {
            Exception = exception;
        }
        public XmlStreamEventType XmlStreamEventType { get; private set; }


        public XmppXElement XmppXElement { get; private set; }

        public Exception Exception { get; private set; }
    }
}
