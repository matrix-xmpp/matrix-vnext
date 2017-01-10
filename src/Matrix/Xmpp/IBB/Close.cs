using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.IBB
{
    [XmppTag(Name = "close", Namespace = Namespaces.Ibb)]
    public class Close : XmppXElement
    {
        internal Close(string tagname)
            : base(Namespaces.Ibb, tagname)
        {
        }

        internal Close(string ns, string tagname)
            : base(ns, tagname)
        {
        }

        public Close() : this("close")
        {
        }

        /// <summary>
        /// Sid
        /// </summary>
        public string Sid
        {
            get { return GetAttribute("sid"); }
            set { SetAttribute("sid", value); }
        }
    }
}