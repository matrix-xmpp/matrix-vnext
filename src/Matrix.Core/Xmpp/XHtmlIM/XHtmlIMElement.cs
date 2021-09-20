using Matrix.Xml;

namespace Matrix.Xmpp.XHtmlIM
{
    public abstract class XHtmlIMElement : XmppXElement
    {
        protected XHtmlIMElement(string tagName) : base(Namespaces.Xhtml, tagName)
        {
        }
    }
}
