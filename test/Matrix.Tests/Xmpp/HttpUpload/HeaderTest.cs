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

using Matrix.Xml;
using Matrix.Xmpp.HttpUpload;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.HttpUpload
{

    public class HeaderTest
    {
        [Fact]
        public void ElementShouldBeOfTypeHeader()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.HttpUpload.header-cookie.xml")).ShouldBeOfType<Header>();
        }
           
        [Fact]
        public void TestCookieHeaderProperties()
        {
            var header = XmppXElement.LoadXml(Resource.Get("Xmpp.HttpUpload.header-cookie.xml")).Cast<Header>();
            Assert.Equal(header.HeaderName, HeaderNames.Cookie);
            Assert.Equal(header.Value, "foo=bar; user=romeo");
        }

        [Fact]
        public void TestAuthHeaderProperties()
        {
            var header = XmppXElement.LoadXml(Resource.Get("Xmpp.HttpUpload.header-authorization.xml")).Cast<Header>();
            Assert.Equal(header.HeaderName, HeaderNames.Authorization);
            Assert.Equal(header.Value, "Basic Base64String==");
        }

        [Fact]
        public void TestBuildCookieHeader()
        {
            var header = new Header(HeaderNames.Cookie, "foo=bar; user=romeo");
            header.ShouldBe(Resource.Get("Xmpp.HttpUpload.header-cookie.xml"));
        }

        [Fact]
        public void TestBuildAuthHeader()
        {
            var header = new Header(HeaderNames.Authorization, "Basic Base64String==");
            header.ShouldBe(Resource.Get("Xmpp.HttpUpload.header-authorization.xml"));
        }
    }
}
