using Matrix.Attributes;

namespace Matrix.Xmpp.Client
{
    /// <summary>
    /// Represents a XMPP client to server stream header
    /// </summary>
    public class Stream : Base.Stream
    {
        public Stream()
        {
            SetAttribute("xmlns", Namespaces.Client);
        }
    }
}