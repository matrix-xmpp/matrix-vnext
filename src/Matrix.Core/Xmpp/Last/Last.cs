using System;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Last
{
    [XmppTag(Name = Tag.Query, Namespace = Namespaces.IqLast)]
    public class Last : XmppXElement
    {
        public Last() : base(Namespaces.IqLast, Tag.Query)
        {
        }

        /// <summary>
        /// Seconds since the last activity.
        /// </summary>
        public int Seconds
        {
            get { return Int32.Parse(GetAttribute("seconds")); }
            set { SetAttribute("seconds", value.ToString()); }
        }
    }
}
