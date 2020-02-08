/*
 * Copyright (c) 2003-2020 by AG-Software <info@ag-software.de>
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

    public class GetTest
    {
        [Fact]
        public void ElementShouldBeOfTypeGet()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.HttpUpload.get.xml")).ShouldBeOfType<Get>();
        }
           
        [Fact]
        public void TestGetProperties()
        {
            var get = XmppXElement.LoadXml(Resource.Get("Xmpp.HttpUpload.get.xml")).Cast<Get>();
            Assert.Equal(get.Url, "https://download.montague.tld/4a771ac1-f0b2-4a4a-9700-f2a26fa2bb67/tr%C3%A8s%20cool.jpg");
        }

        [Fact]
        public void TestBuildGet()
        {
            var get = new Get { Url = "https://download.montague.tld/4a771ac1-f0b2-4a4a-9700-f2a26fa2bb67/tr%C3%A8s%20cool.jpg" };
            get.ShouldBe(Resource.Get("Xmpp.HttpUpload.get.xml"));
        }
    }
}
