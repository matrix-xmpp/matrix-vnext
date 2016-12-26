using System.Linq;
using Matrix.Attributes;
using Matrix.Xml;
using Matrix.Xmpp.XData;

namespace Matrix.Xmpp.Base
{
    /// <summary>
    /// Base Iq Stanza
    /// </summary>
    //[XmppTag(Name=Tag.Iq)]
    public abstract class Iq : XmppXElementWithAddressAndId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Iq"/> class.
        /// </summary>
        /// <param name="ns">The ns.</param>
        internal Iq(string ns) : base(ns, Tag.Iq)
        {            
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public IqType Type
        {
            get { return GetAttributeEnum<IqType>("type"); }
            set { SetAttribute("type", value.ToString().ToLower()); }
        }

        /// <summary>
        /// The query child of this Iq. Because the query tag can be in many different namespaces this
        /// member returns the first childnode which schould be the query in nearly all cases.
        /// Otherwise use XLinq routines to get the information.
        /// </summary>
        public XmppXElement Query
        {
            get { return Elements().FirstOrDefault() as XmppXElement; }
            set 
            {
                if (!Elements().Any())
                    Add(value);
            }
        }

        /// <summary>
        /// Gets or sets the Xdata object.
        /// </summary>
        /// <value>The X data.</value>
        public Data XData
        {
            get { return Element<Data>(); }
            set { Replace(value); }
        }
    }
}