using System;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.XHtmlIM
{
    [XmppTag(Name = "body", Namespace = Namespaces.Xhtml)]
    public class Body : XmppXElement
    {
        public Body() : base (Namespaces.Xhtml, "body")
        {
        }

        /// <summary>
        /// Gets or sets the inner X-HTML.
        /// </summary>
        /// <remarks>The content must be valid X-Html, otherwise an exception will be thrown.</remarks>
        /// <value>The inner X-HTML.</value>
        public string InnerXHtml
        {
            get
            {
                var body = ToString(false);
                var startTag    = StartTag();
                var endTag      = EndTag();
                int start = body.IndexOf(startTag) + startTag.Length;
                int end = body.LastIndexOf(endTag);
                
                return body.Substring(start, end - start);
            }
            set
            {
                try
                {
                    var el = LoadXml(StartTag() + value + EndTag());
                    foreach (var child in el.Elements())
                        Add(child);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}