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
using Matrix.Xmpp.Shim;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Shim
{
    public class ShimTests
    {
        [Fact]
        public void TestShouldbeOfTypeHeaders()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Shim.headers1.xml")).ShouldBeOfType<Headers>();
        }

        [Fact]
        public void TestHeaders()
        {
            var headers = XmppXElement.LoadXml(Resource.Get("Xmpp.Shim.headers1.xml")).Cast<Headers>();

            Assert.Equal(headers.HasHeaders, true);
            Assert.Equal(headers.HasHeader("Created"), true);
            Assert.Equal(headers.HasHeader("created"), false);
            Assert.Equal(headers.HasHeader(HeaderNames.Created), true);
            Assert.Equal(headers[HeaderNames.Created].Value == "2004-09-21T03:01:52Z", true);
        }

        [Fact]
        public void TestHeaders2()
        {
            var headers = new Headers();
            headers.AddHeader(HeaderNames.Created, "2004-09-21T03:01:52Z");
            headers.ShouldBe(Resource.Get("Xmpp.Shim.headers1.xml"));

            var headers2 = new Headers();
            headers2[HeaderNames.Created].Value = "2004-09-21T03:01:52Z";
            headers2.ShouldBe(Resource.Get("Xmpp.Shim.headers1.xml"));
        }

        [Fact]
        public void TestBuildHeaders()
        {
            var headers = new Headers();
            headers.AddHeader(HeaderNames.Created, "2004-09-21T03:01:52Z");

            headers.ShouldBe(Resource.Get("Xmpp.Shim.headers1.xml"));
        }

        [Fact]
        public void TestBuildHeaders2()
        {
            var headers = new Headers();
            headers.SetHeader(HeaderNames.Created);
            headers.ShouldBe(Resource.Get("Xmpp.Shim.headers2.xml"));
        }
    }
}
