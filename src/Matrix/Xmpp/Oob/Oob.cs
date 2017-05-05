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

using Matrix.Attributes;
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
