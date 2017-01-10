using Matrix.Attributes;

namespace Matrix.Xmpp.Framing
{
    /// <summary>
    /// Websocket framing close tag
    /// </summary>
    [XmppTag(Namespace = Namespaces.Framing, Name = "close")]
    public class Close : Base.Stream
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Close"/> class.
        /// </summary>
        public Close() : base(Namespaces.Framing, "close") { }
    }
}