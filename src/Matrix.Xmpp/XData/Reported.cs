using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.XData
{
    [XmppTag(Name = "reported", Namespace = Namespaces.XData)]
    public class Reported : XmppXElement
    {
        /// <summary>
        /// The  &lt;reported/&gt; element can be understood as a "table header" describing the following data.
        /// </summary>
        public Reported() : base(Namespaces.XData, "reported")
        {
        }
    }
}