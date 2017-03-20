/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
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

using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.XHtmlIM;
using Xunit;
using Shouldly;

namespace Matrix.Tests.Xmpp.Client
{
    public class XHTML
    {
        [Fact]
        public void TestXhtmlMessage()
        {
            var msg = XmppXElement.LoadXml(Resource.Get("Xmpp.Client.message2.xml"));
            var body = msg.Element<Html>().Element<Body>();
            body.InnerXHtml.Trim().ShouldBe("<p>Hello World</p>");
        }

        [Fact]
        public void BuildXhtmlMessage()
        {
            string expectedXml = Resource.Get("Xmpp.Client.message2.xml");
            new Message
                {
                    XHtml = new Html
                    {
                        Body = new Body {InnerXHtml = "<p>Hello World</p>"}
                    }
                }
                .ShouldBe(expectedXml);
        }
    }
}
