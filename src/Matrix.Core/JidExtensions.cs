namespace Matrix
{
    using System.Xml.Linq;
    using Matrix.Xml;

    public static class JidExtensions
    {
        #region GetTag Jid
        public static Jid GetTagJid(this XmppXElement el,  string tagname)
        {
            return GetTagJid(el, el.Name.Namespace, tagname);
        }

        public static Jid GetTagJid(this XmppXElement el, string ns, string tagname)
        {
            XNamespace xns = ns;
            return GetTagJid(el, xns, tagname);
        }

        public static Jid GetTagJid(this XmppXElement el, XNamespace xns, string tagname)
        {
            string val = el.GetTag(xns, tagname);
            if (val != null)
                return new Jid(val);

            return null;
        }
        #endregion

        /// <summary>
        /// Get a Jid attribute. Returns null if the attribute does not exist.
        /// </summary>
        /// <param name="name">attribute name to lookup</param>
        /// <returns></returns>
        public static Jid GetAttributeJid(this XmppXElement el, string name)
        {
            return el.HasAttribute(name) ? new Jid(el.GetAttribute(name)) : null;
        }

        /// <summary>
        /// Set a attribute of type Jid
        /// </summary>
        /// <param name="name">attribute name</param>
        /// <param name="jid">value of the attribute, or null to remove the attribute</param>
        public static XmppXElement SetAttribute(this XmppXElement el, string name, Jid jid)
        {
            if (jid != null)
                el.SetAttribute(name, jid.ToString());
            else
                el.RemoveAttribute(name);

            return el;
        }
    }
}
