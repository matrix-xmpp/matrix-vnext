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
using Matrix.Xmpp.Bosh;
using Xunit;
using Shouldly;

namespace Matrix.Tests.Xmpp.Bosh
{
     
    public class BoshTest
    {
        [Fact]
        public void XmppXElementShouldbeOfTypeBody()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Bosh.bosh1.xml")).ShouldBeOfType<Body>();
        }

        [Fact]
        public void TestCondition()
        {
            var body = XmppXElement.LoadXml(Resource.Get("Xmpp.Bosh.bosh1.xml")).Cast<Body>();
            body.Condition.ShouldBe(Condition.RemoteConnectionFailed);

            var body2 = XmppXElement.LoadXml(Resource.Get("Xmpp.Bosh.bosh2.xml")).Cast<Body>();
            body2.Condition.ShouldBe(Condition.ItemNotFound);

            var body3 = XmppXElement.LoadXml(Resource.Get("Xmpp.Bosh.bosh3.xml")).Cast<Body>();
            body3.Condition.ShouldBe(Condition.HostUnknown);

            var body4 = XmppXElement.LoadXml(Resource.Get("Xmpp.Bosh.bosh4.xml")).Cast<Body>();
            body4.Condition.ShouldBe(Condition.SeeOtherUri);

            var body5 = XmppXElement.LoadXml(Resource.Get("Xmpp.Bosh.bosh5.xml")).Cast<Body>();
            body5.Condition.ShouldBe(Condition.BadRequest);
        }

        [Fact]
        public void BuildBody()
        {
            var expectedXml = Resource.Get("Xmpp.Bosh.bosh1.xml");
            new Body
                {
                    Type = Type.Terminate,
                    Condition = Condition.RemoteConnectionFailed
                }
                .ShouldBe(expectedXml);

            var expectedXml2 = Resource.Get("Xmpp.Bosh.bosh2.xml");
            new Body
            {
                Type = Type.Terminate,
                Condition = Condition.ItemNotFound
            }
            .ShouldBe(expectedXml2);

            var expectedXml3 = Resource.Get("Xmpp.Bosh.bosh3.xml");
            new Body
            {
                Type = Type.Terminate,
                Condition = Condition.HostUnknown
            }
            .ShouldBe(expectedXml3);

            var expectedXml4 = Resource.Get("Xmpp.Bosh.bosh4.xml");
            new Body
            {
                Type = Type.Terminate,
                Condition = Condition.SeeOtherUri
            }
            .ShouldBe(expectedXml4);

            var expectedXml5 = Resource.Get("Xmpp.Bosh.bosh5.xml");
            new Body
            {
                Type = Type.Terminate,
                Condition = Condition.BadRequest
            }
            .ShouldBe(expectedXml5);
        }
    }
}
