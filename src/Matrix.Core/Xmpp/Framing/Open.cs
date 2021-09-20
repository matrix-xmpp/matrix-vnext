using Matrix.Attributes;

namespace Matrix.Xmpp.Framing
{
    /// <summary>
    /// Websocket framing open tag
    /// </summary>
    [XmppTag(Namespace = Namespaces.Framing, Name = "open")]
    public class Open : Base.Stream
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Open"/> class.
        /// </summary>
        public Open() : base(Namespaces.Framing, "open") { }
    }
}
