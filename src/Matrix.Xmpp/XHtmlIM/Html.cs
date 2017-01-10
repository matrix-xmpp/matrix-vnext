using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.XHtmlIM
{
    [XmppTag(Name = "html", Namespace = Namespaces.XhtmlIm)]
    public class Html : XmppXElement
    {
        public Html()
            : base(Namespaces.XhtmlIm, "html")
        {
        }
        
        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        public Body Body
        {
            get { return Element<Body>(); }
            set { Replace(value); }
        }
    }
}