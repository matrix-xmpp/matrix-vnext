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
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub.Owner
{
    public class ConfigureTest
    {
        [Fact]
        public void ShoudBeOfTypeDisassociate()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.configure1.xml")).ShouldBeOfType<Matrix.Xmpp.PubSub.Owner.Configure>();
        }

        [Fact]
        public void BuildConfigure()
        {
            new Matrix.Xmpp.PubSub.Owner.Configure().ShouldBe(Resource.Get("Xmpp.PubSub.Owner.configure1.xml"));
        }

        [Fact]
        public void TestConfigureContainsXData()
        {
            var conf = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.configure2.xml")).Cast<Matrix.Xmpp.PubSub.Owner.Configure>();
            Assert.Equal(conf.XData != null, true);
        }
    }
}
