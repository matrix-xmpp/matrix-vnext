using Matrix.Attributes;
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.LastMessageCorrection
{
    /// <summary>
    /// XEP-0308: Last Message Correction
    /// </summary>
    [XmppTag(Name = "replace", Namespace = Namespaces.LastMessageCorrection)]
    public class Replace : XmppXElementWithIdAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Replace"/> class.
        /// </summary>
        public Replace() : base(Namespaces.LastMessageCorrection, "replace")
        {
        }
    }
}