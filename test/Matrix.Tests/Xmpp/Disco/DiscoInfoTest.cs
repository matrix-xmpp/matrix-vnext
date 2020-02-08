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

using System.Linq;
using Xunit;
using Matrix.Xml;
using Matrix.Xmpp.Disco;
using Shouldly;

namespace Matrix.Tests.Xmpp.Disco
{
    
    public class DiscoInfoTest
    {
        [Fact]
        public void ElementShouldBeOfTypeDiscoInfo()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Disco.discoinfo1.xml")).ShouldBeOfType<Info>();
        }

        [Fact]
        public void TestHasField1()
        {
            var info = XmppXElement.LoadXml(Resource.Get("Xmpp.Disco.discoinfo1.xml")).Cast<Info>();
            var xdata = info.XData;
            
            xdata.HasField("muc#roominfo_description").ShouldBeTrue();
            xdata.HasField("muc#roominfo_occupants").ShouldBeTrue();
            xdata.HasField("muc#roominfo_description2").ShouldBeFalse();
            xdata.HasField("muc#roominfo_occupants").ShouldBeTrue();
        }

        [Fact]
        public void TestGetField()
        {
            var info = XmppXElement.LoadXml(Resource.Get("Xmpp.Disco.discoinfo1.xml")).Cast<Info>();
            var xdata = info.XData;

            xdata.GetFields().Count().ShouldBe(5);
            xdata.GetField("muc#roominfo_occupants").GetValue().ShouldBe("3");
        }
    }
}
