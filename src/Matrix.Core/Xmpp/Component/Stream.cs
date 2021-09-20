namespace Matrix.Xmpp.Component
{
    /// <summary>
    /// Represents a XMPP client to server stream header
    /// </summary>
    public class Stream : Base.Stream
    {
        public Stream()
        {
            SetAttribute("xmlns", Namespaces.Accept);
        }
    }
}
