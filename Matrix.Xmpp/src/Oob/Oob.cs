using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Oob
{
	 /// <summary>
    /// XEP-0066: Out of Band Data
	/// </summary>
    [XmppTag(Name = Tag.Query, Namespace = Namespaces.IqOob)]
    public class Oob : XmppXElement
    {
        #region Xml sample
        //	<iq type="set" to="horatio@denmark" from="sailor@sea" id="i_oob_001">
        //		<query xmlns="jabber:iq:oob" id="xy">
        //			<url>http://denmark/act4/letter-1.html</url>
        //			<desc>There's a letter for you sir.</desc>
        //		</query>
        // </iq>	
        #endregion

        internal Oob(string ns, string tagname) : base(ns, tagname)
        {
        }

        public Oob()
            : base(Namespaces.IqOob, Tag.Query)
		{
		}

        /// <summary>
        /// Gets or sets the session id.
        /// </summary>
        /// <value>
        /// The sid.
        /// </value>
	    public string Sid
	    {
            get { return GetAttribute("sid"); }
            set { SetAttribute("sid", value); }
	    }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
		public string Url
		{
			set{ SetTag("url", value); }
			get{ return GetTag("url"); }
		}

        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        public System.Uri Uri
        {
            set { SetTag("url", value.ToString()); }
            get
            {
                var url = Url;
                return url != null ? new System.Uri(url) : null;
            }
        }

        /// <summary>
        /// Gets or sets a description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
		public string Description
		{
			set { SetTag("desc", value); }
			get { return GetTag("desc"); }
		}
	}
}