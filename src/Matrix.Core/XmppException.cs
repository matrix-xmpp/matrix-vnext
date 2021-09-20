using Matrix.Xml;
using System;

namespace Matrix
{
    public class XmppException : Exception
    {
        public XmppException()
        {
        }

        public XmppException(string message)
            : base(message)
        {
        }

        public XmppException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public XmppException(XmppXElement el)
            : this()
        {
            Stanza = el;
        }

        public XmppException(XmppXElement el, string message)
            : this(message)
        {
            Stanza = el;
        }

        /// <summary>
        /// the Stanza which raised this exception
        /// </summary>
        public XmppXElement Stanza { get; set; }
    }
}
