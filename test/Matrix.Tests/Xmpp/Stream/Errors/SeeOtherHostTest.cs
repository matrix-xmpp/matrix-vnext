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
using Matrix.Xmpp.Stream.Errors;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Stream.Errors
{
    public class SeeOtherHostTest
    {
        [Fact]
        public void TestShouldbeOfTypeHeaders()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Stream.Errors.see_other_host1.xml")).ShouldBeOfType<SeeOtherHost>();
        }

        [Fact]
        public void TestSeeOtherHost1()
        {
            var soh = XmppXElement.LoadXml(Resource.Get("Xmpp.Stream.Errors.see_other_host1.xml")).Cast<SeeOtherHost>();
            Assert.Equal(soh.Hostname, "foo.com");
            Assert.Equal(soh.Port, 5222);
        }

        [Fact]
        public void TestSeeOtherHost2()
        {
            var soh = XmppXElement.LoadXml(Resource.Get("Xmpp.Stream.Errors.see_other_host2.xml")).Cast<SeeOtherHost>(); ;
            Assert.Equal(soh.Hostname, "foo.com");
            Assert.Equal(soh.Port, 80);
        }

        [Fact]
        public void TestbuildSeeOtherHost()
        {
            new SeeOtherHost {Port = 80, Hostname = "foo.com" }
                .ShouldBe(Resource.Get("Xmpp.Stream.Errors.see_other_host2.xml"));
        }
    }
}
