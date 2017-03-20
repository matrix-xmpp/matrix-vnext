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
using Matrix.Xmpp.Disco;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Disco
{
    public class FeatureTest
    {
        [Fact]
        public void ElementShouldBeOfTypeFeature()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Disco.feature1.xml")).ShouldBeOfType<Feature>();
        }

        [Fact]
        public void TestFeatureVar()
        {
            var feat = XmppXElement.LoadXml(Resource.Get("Xmpp.Disco.feature1.xml")).Cast<Feature>();
            feat.Var.ShouldBe("http://jabber.org/protocol/disco#info");
        }

        [Fact]
        public void BuildFeature()
        {
            string expectedXml = Resource.Get("Xmpp.Disco.feature1.xml");
            Feature feat2 = new Feature("http://jabber.org/protocol/disco#info");
            feat2.ShouldBe(expectedXml);
        }
    }
}
