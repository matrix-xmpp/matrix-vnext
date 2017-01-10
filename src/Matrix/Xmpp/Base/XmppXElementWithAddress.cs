using System.Xml.Linq;
using Matrix.Xml;

namespace Matrix.Xmpp.Base
{
    /// <summary>
    /// AddressedXmppXElement
    /// </summary>
    public abstract class XmppXElementWithAddress  : XmppXElement
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndId"/> class.
        /// </summary>
        /// <param name="ns">The ns.</param>
        /// <param name="tagname">The tagname.</param>
        public XmppXElementWithAddress(XNamespace ns, string tagname) : base(ns + tagname)
        {            
        }

        //// check
        //public DirectionalStanza(XNamespace ns, string tagname, object content)
        //    : base(ns + tagname, content)
        //{
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndId"/> class.
        /// </summary>
        /// <param name="ns">The ns.</param>
        /// <param name="tagname">The tagname.</param>
        public XmppXElementWithAddress(string ns, string tagname) : this("{" + ns + "}" + tagname)
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndId"/> class.
        /// </summary>
        /// <param name="ns">The ns.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="tagname">The tagname.</param>
        public XmppXElementWithAddress(string ns, string prefix, string tagname)
            : this("{" + ns + "}" + tagname, new XAttribute(XNamespace.Xmlns + prefix, ns))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndId"/> class.
        /// </summary>
        /// <param name="other">The other.</param>
        public XmppXElementWithAddress(XElement other) : base(other)
        {         
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndId"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public XmppXElementWithAddress(XName name) : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndId"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="content">The content.</param>
        public XmppXElementWithAddress(XName name, object content)
            : base(name, content)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndId"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="content">The content.</param>
        public XmppXElementWithAddress(XName name, params object[] content)
            : base(name, content)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppXElementWithAddressAndId"/> class.
        /// </summary>
        /// <param name="other">An <see cref="T:System.Xml.Linq.XStreamingElement"/> that contains unevaluated queries that will be iterated for the contents of this <see cref="T:System.Xml.Linq.XElement"/>.</param>
        public XmppXElementWithAddress(XStreamingElement other)
            : base(other)            
        {
        }
        #endregion

        /// <summary>
        /// Gets or sets the from Jid.
        /// </summary>
        /// <value>From.</value>
        public Jid From
        {
            get
            {
                if (HasAttribute("from"))
                    return new Jid(GetAttribute("from"));
                
                return null;
            }
            set { SetAttribute("from", value); }
        }

        /// <summary>
        /// Gets or sets the to Jid.
        /// </summary>
        /// <value>To.</value>
        public Jid To
        {
            get
            {
                if (HasAttribute("to"))
                    return new Jid(GetAttribute("to"));
                
                return null;
            }
            set { SetAttribute("to", value); }
        }

        /// <summary>
        /// Switches the from and to attributes when existing
        /// </summary>
        public void SwitchDirection()
        {
            Jid from = From;
            Jid to = To;

            // Remove from and to now
            RemoveAttribute("from");
            RemoveAttribute("to");

            Jid helper = from;
            from = to;
            to = helper;

            From = from;
            To = to;
        }
    }
}