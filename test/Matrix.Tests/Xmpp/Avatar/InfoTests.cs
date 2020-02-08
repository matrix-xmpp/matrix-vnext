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

using Xunit;

using Matrix.Xml;
using Shouldly;
using Matrix.Xmpp.Avatar;

namespace Matrix.Tests.Xmpp.Avatar
{
    
    public class InfoTests
    {
        [Fact]
        public void TestFactory()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Avatar.info.xml"))
                .ShouldBeOfType<Info>();           
        }

        [Fact]
        public void TestReadAttributes()
        {
            var info = XmppXElement.LoadXml(Resource.Get("Xmpp.Avatar.info.xml")).Cast<Info>();

            info.CountBytes.ShouldBe(123456);
            info.Height.ShouldBe(64);
            info.Width.ShouldBe(64);
            info.Id.ShouldBe("357a8123a30844a3aa99861b6349264ba67a5694");
            info.Uri.ShouldBe(new System.Uri("http://avatars.example.org/happy.gif"));
            info.Type.ShouldBe("image/gif");        
        }

        [Fact]
        public void TestBuildInfo()
        {
            var expectedXml = Resource.Get("Xmpp.Avatar.info.xml");
            new Info
            {
                CountBytes = 123456,
                Height = 64,
                Width = 64,
                Type = "image/gif",
                Uri = new System.Uri("http://avatars.example.org/happy.gif"),
                Id = "357a8123a30844a3aa99861b6349264ba67a5694"                
            }
            .ShouldBe(expectedXml);
        }
    }
}
