namespace Matrix.Xmpp.Server
{
    /// <summary>
    /// Represents a XMPP Server to server stream header
    /// </summary>
    public class Stream : Base.Stream
    {
        /// <summary>
        /// Initializes a Server Stream header.
        /// </summary>
        public Stream()
        {
            SetAttribute("xmlns", Namespaces.Server);
        }

        /// <summary>
        /// Initializes a Server Stream header.
        /// </summary>
        /// <param name="includeDialbackNameSpaceDeclaration">if set to <c>true</c> includes dialback name space declaration.</param>
        public Stream(bool includeDialbackNameSpaceDeclaration) : this()
        {
            if (includeDialbackNameSpaceDeclaration)
                AddDialbackNameSpaceDeclaration();
        }

        /// <summary>
        /// Adds the Dialback Namespace declaration to the stream element (xmlns:db='jabber:server:dialback')
        /// </summary>
        public void AddDialbackNameSpaceDeclaration()
        {
            AddNameSpaceDeclaration("db", Namespaces.ServerDialback);
        }
    }
}