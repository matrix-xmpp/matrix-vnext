using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Auth
{
    /// <summary>
    /// Non SASL authentication (XEP-0078)
    /// </summary>
    [XmppTag(Name = Tag.Query, Namespace = Namespaces.IqAuth)]
    public class Auth : XmppXElement
    {
        public Auth():base(Namespaces.IqAuth, Tag.Query)
        {}

        #region << Properties >>
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username
        {
            get { return GetTag("username"); }
            set { SetTag("username", value); }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password
        {
            get { return GetTag("password"); }
            set { SetTag("password", value); }
        }

        /// <summary>
        /// Gets or sets the resource.
        /// </summary>
        /// <value>The resource.</value>
        public string Resource
        {
            get { return GetTag("resource"); }
            set { SetTag("resource", value); }
        }

        public string Digest
        {
            get { return GetTag("digest"); }
            set { SetTag("digest", value); }
        }
        #endregion
    }
}