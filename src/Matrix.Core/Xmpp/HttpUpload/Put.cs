using Matrix.Attributes;
using Matrix.Xml;
using System.Collections.Generic;
using System.Linq;

namespace Matrix.Xmpp.HttpUpload
{
    [XmppTag(Name = "put", Namespace = Namespaces.HttpUpload)]
    public class Put : XmppXElement
    {
        public Put() : base(Namespaces.HttpUpload, "put")
        {
        }

        /// <summary>
        ///   Gets or sets the url.
        /// </summary>
        /// <value>
        ///   The url.
        /// </value>
        public string Url
        {
            get { return GetAttribute("url"); }
            set { SetAttribute("url", value); }
        }

        #region << header >>
        /// <summary>
        /// Adds a new Header
        /// </summary>
        /// <returns>a new Header.</returns>
        public Header AddHeader()
        {
            var h = new Header();
            Add(h);
            return h;
        }

        /// <summary>
        /// Adds the given Header
        /// </summary>
        /// <param name="header">The header.</param>
        /// <returns>returns the given Header</returns>
        public Header AddHeader(Header header)
        {
            Add(header);
            return header;
        }

        public Header AddHeader(HeaderNames name, string val = null)
        {
            var header = new Header(name, val);
            Add(header);
            return header;
        }

        /// <summary>
        /// Sets the header.
        /// </summary>
        /// <param name="name">The header name.</param>
        /// <param name="val">The header value.</param>
        public void SetHeader(HeaderNames name, string val = null)
        {
            var header = GetHeader(name);
            if (header != null)
                header.Value = val;
            else
                AddHeader(name, val);
        }

        /// <summary>
        /// Determines whether a header with the specified name exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns><c>true</c> if the header exists; otherwise, <c>false</c>.</returns>
        public bool HasHeader(HeaderNames name)
        {
            return Elements<Header>().Any(h => h.HeaderName == name);
        }

        /// <summary>
        /// Gets a value indicating whether this instance has any headers.
        /// </summary>
        /// <value><c>true</c> if this instance has headers; otherwise, <c>false</c>.</value>
        public bool HasHeaders => Elements<Header>() != null;

        /// <summary>
        /// Gets the header with the given header name.
        /// </summary>
        /// <param name="name">The header name.</param>
        /// <returns>Header.</returns>
        public Header GetHeader(HeaderNames name)
        {
            return Elements<Header>().FirstOrDefault(h => h.HeaderName == name);
        }

        /// <summary>
        /// Gets all headers.
        /// </summary>
        /// <returns>IEnumerable&lt;Header&gt;.</returns>
        public IEnumerable<Header> GetHeaders()
        {
            return Elements<Header>();
        }
        #endregion
    }
}
