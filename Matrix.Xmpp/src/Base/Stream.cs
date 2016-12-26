using Matrix.Core.Attributes;

namespace Matrix.Xmpp.Base
{
    /// <summary>
    /// Represents a XMPP stream header
    /// </summary>
    [XmppTag(Name = "stream", Namespace = Namespaces.Stream)]
    public class Stream : XmppXElementWithAddressAndId
    {
        #region << Constructors >>
        public Stream()
            : base(Namespaces.Stream, "stream", "stream")
        {
        }

        internal Stream(string ns, string tagname)
            : base(ns, tagname)
        {
        }
        #endregion
        
        /// <summary>
        /// See XMPP-Core 4.4.1 "Version Support"
        /// </summary>
        public string Version
        {
            get { return GetAttribute("version"); }
            set { SetAttribute("version", value); }
        }
    }
}