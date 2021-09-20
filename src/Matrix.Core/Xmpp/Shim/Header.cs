using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Shim
{
    /// <summary>
    ///<a href="http://xmpp.org/extensions/xep-0131.html">XEP-0131</a> SHIM Header
    /// </summary>
    [XmppTag(Name = "header", Namespace = Namespaces.Shim)]
    public class Header : XmppXElement
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Header"/> class.
        /// </summary>
        public Header(): base(Namespaces.Shim, "header")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Header"/> class.
        /// </summary>
        /// <param name="name">The header name.</param>
        /// <param name="val">The header value.</param>
        public Header(HeaderNames name, string val = null)
            : this(name.GetName(), val) { }
        

        /// <summary>
        /// Initializes a new instance of the <see cref="Header"/> class.
        /// </summary>
        /// <param name="name">The header name.</param>
        /// <param name="val">The header value.</param>
        public Header(string name, string val = null) 
            : this()      
		{
			Name	= name;

            if (!string.IsNullOrEmpty(val))
			    Value	= val;
		}
        #endregion

        /// <summary>
        /// Gets the name of this header.
        /// </summary>
        /// <value>The header name.</value>
        public new string Name
		{
			get { return GetAttribute("name"); }
			set { SetAttribute("name", value); }
		}
    }
}
