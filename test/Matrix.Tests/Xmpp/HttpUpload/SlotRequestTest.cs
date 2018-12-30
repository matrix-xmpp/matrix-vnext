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
using Matrix.Xmpp.Client;
using Matrix.Xmpp.HttpUpload;
using Shouldly;
using System.Linq;
using Xunit;

namespace Matrix.Tests.Xmpp.HttpUpload
{
    public class SlotRequestTest
    {
        [Fact]
        public void ElementShouldBeOfTypeRequest()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.HttpUpload.slot-request.xml")).ShouldBeOfType<Request>();
        }

        [Fact]
        public void TestRequestProperties()
        {
            var request = XmppXElement.LoadXml(Resource.Get("Xmpp.HttpUpload.slot-request.xml")).Cast<Request>();
            Assert.Equal(request.Filename, "très cool.jpg");
            Assert.Equal(request.Size, 23456);
            Assert.Equal(request.ContentType, "image/jpeg");
        }

        [Fact]
        public void TestBuildRequest()
        {
            var request = new Request { Size = 23456, Filename = "très cool.jpg", ContentType = "image/jpeg" };
            request.ShouldBe(Resource.Get("Xmpp.HttpUpload.slot-request.xml"));
        }


        [Fact]
        public void TestRequestIq()
        {
            var iq = XmppXElement.LoadXml(Resource.Get("Xmpp.HttpUpload.slot-request-iq.xml")).Cast<Iq>();

            var slot = iq.Element<Slot>();
            Assert.NotNull(slot);

            var put = slot.Element<Put>();
            Assert.NotNull(put);
            Assert.Equal(put.Url, "https://upload.montague.tld/4a771ac1-f0b2-4a4a-9700-f2a26fa2bb67/tr%C3%A8s%20cool.jpg");

            Assert.True(put.HasHeaders);
            Assert.True(put.HasHeader(HeaderNames.Cookie));
            Assert.True(put.HasHeader(HeaderNames.Authorization));
            Assert.False(put.HasHeader(HeaderNames.Expires));
            Assert.Equal(put.GetHeaders().Count(), 2);
            Assert.Equal(put.GetHeader(HeaderNames.Authorization).Value, "Basic Base64String==");
            Assert.Equal(put.GetHeader(HeaderNames.Cookie).Value, "foo=bar; user=romeo");

            var get = slot.Element<Get>();
            Assert.NotNull(get);
            Assert.Equal(get.Url, "https://download.montague.tld/4a771ac1-f0b2-4a4a-9700-f2a26fa2bb67/tr%C3%A8s%20cool.jpg");
        }

        [Fact]
        public void TestBuildRequestIq()
        {
            var put = new Put
            {
                Url = "https://upload.montague.tld/4a771ac1-f0b2-4a4a-9700-f2a26fa2bb67/tr%C3%A8s%20cool.jpg"
            };
            put.AddHeader(HeaderNames.Authorization, "Basic Base64String==");
            put.AddHeader(HeaderNames.Cookie, "foo=bar; user=romeo");

            var get = new Get
            {
                Url = "https://download.montague.tld/4a771ac1-f0b2-4a4a-9700-f2a26fa2bb67/tr%C3%A8s%20cool.jpg"
            };

            var slot = new Slot
            {
                Put = put,
                Get = get,
            };

            var iq = new Iq { Type = Matrix.Xmpp.IqType.Result, Id = "step_03", To = "romeo@montague.tld/garden", From = "upload.montague.tld" };
            iq.Add(slot);

            iq.ShouldBe(Resource.Get("Xmpp.HttpUpload.slot-request-iq.xml"));
        }
    }
}
