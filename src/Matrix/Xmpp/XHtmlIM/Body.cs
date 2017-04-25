/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
 * See the COPYING file for more information.
 *
 * This file is part of the MatriX project.
 *
 * NOTICE: All information contained herein is, and remains the property
 * of AG-Software and its suppliers, if any.
 * The intellectual and technical concepts contained herein are proprietary
 * to AG-Software and its suppliers and may be covered by German and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 *
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from AG-Software.
 *
 * Contact information for AG-Software is available at http://www.ag-software.de
 */

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
