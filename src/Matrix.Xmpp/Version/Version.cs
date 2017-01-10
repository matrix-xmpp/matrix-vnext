using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Version
{
    /// <summary>
    /// XEP-0092: Software Version
    /// </summary>
    [XmppTag(Name = Tag.Query, Namespace = Namespaces.IqVersion)]
    public class Version : XmppXElement
    {
        public Version() : base(Namespaces.IqVersion, Tag.Query)
        {
        }

        //
        /// <summary>
        /// Gets the name of the software product.
        /// </summary>
        /// <returns>An <see cref="T:System.Xml.Linq.XName" /> that contains the name of this element.</returns>
        public new string Name
        {
            set { SetTag("name", value); }
            get { return GetTag("name"); }
        }

        /// <summary>
        /// Gets or sets the software version information.
        /// </summary>
        /// <value>
        /// The ver.
        /// </value>
        public string Ver
        {
            set { SetTag("version", value); }
            get { return GetTag("version"); }
        }

        /// <summary>
        /// Gets or sets the operating system of teh software.
        /// </summary>
        /// <value>
        /// The os.
        /// </value>
        public string Os
        {
            set { SetTag("os", value); }
            get { return GetTag("os"); }
        }
    }
}