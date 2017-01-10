using System;
using Matrix.Core;
using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Delay
{
    /// <summary>
    /// XEP-0091: Delayed Delivery
    /// </summary>
    [XmppTag(Name = "x", Namespace = Namespaces.XDelay)]
    public class XDelay : XmppXElement
    {
        /*
         * <x xmlns="jabber:x:delay" from="lynx@ve.symlynx.com" stamp="20090222T18:39:51" />         
         */
        #region << Constructors >>>
        /// <summary>
        /// Initializes a new instance of the <see cref="XDelay"/> class.
        /// </summary>
        public XDelay()
            : base(Namespaces.XDelay, "x")
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XDelay"/> class.
        /// </summary>
        /// <param name="stamp">The stamp.</param>
        public XDelay(DateTime stamp)
            : this()
        {
            Stamp = stamp;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XDelay"/> class.
        /// </summary>
        /// <param name="stamp">The stamp.</param>
        /// <param name="from">From.</param>
        public XDelay(DateTime stamp, Jid from)
            : this(stamp)
        {
            From = from;
        }
        #endregion

        /// <summary>
        /// The Jabber ID of the entity that originally sent the XML stanza or that delayed the delivery of the stanza 
        /// (for example, the address of a multi-user chat room).
        /// </summary>
        /// <value>From.</value>
        public Jid From
        {
            get { return GetAttributeJid("from"); }
            set { SetAttribute("from", value); }
        }

        /// <summary>
        /// Gets or sets the stamp. The time when the XML stanza was originally sent.
        /// </summary>
        /// <value>The stamp.</value>
        public DateTime Stamp
        {
            get { return Core.Time.JabberDate(GetAttribute("stamp")); }
            set { SetAttribute("stamp", Core.Time.JabberDate(value)); }
        }
    }
}