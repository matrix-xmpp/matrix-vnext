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
using Shouldly;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace Matrix.Tests.Xml
{
    public class XmppXElementTests
    {
        [Fact]
        public void TestLoadXmlFromString()
        {
            string xml1 = "<a><b>foo</b></a>";
            var elA = XmppXElement.LoadXml(xml1);
            elA.ToString(false).ShouldBe(xml1);
        }

        [Fact]
        public void XmlShouldContainOneCDataElement()
        {
            var el = XmppXElement.LoadXml(Resource.Get("Xml.cdata1.xml"));

            var xml = el.ToString(false);
            Regex.Matches(xml, "CDATA").Count.ShouldBe(1);
        }

        [Fact]
        public void DescendantsTest()
        {
            var msg = XmppXElement.LoadXml(Resource.Get("Xml.message.xml")).Cast<Message>();
            var elements = msg.Descendants<Matrix.Xmpp.XData.Data>();

            elements.ShouldNotBeNull();
            elements.Count().ShouldBe(1);

            var firstResult = elements.First();
            firstResult.ShouldBeOfType<Matrix.Xmpp.XData.Data>();

            var elements2 = msg.Descendants<Matrix.Xmpp.XData.Field>();
            elements2.ShouldNotBeNull();
            elements2.Count().ShouldBeGreaterThan(1);
        }

        [Fact]
        public void DescendantsShouldReturnNull()
        {
            var msg = XmppXElement.LoadXml(Resource.Get("Xml.message.xml")).Cast<Message>();
            var desc = msg.Descendants<Iq>();
            
            desc.ShouldBeEmpty();
            desc.Count().ShouldBe(0);
        }

        [Fact]
        public void HasDescendantsTest()
        {
            var msg = XmppXElement.LoadXml(Resource.Get("Xml.message.xml")).Cast<Message>();
            msg.HasDescendants<Iq>().ShouldBe(false);
            msg.HasDescendants<Matrix.Xmpp.XData.Field>().ShouldBe(true);
        }

        [Fact]
        public void FindElementTest()
        {
            var msg = XmppXElement.LoadXml(Resource.Get("Xml.message.xml")).Cast<Message>();
            var elements = msg.Element<Matrix.Xmpp.XData.Data>(true);
            elements.ShouldNotBeNull();
            
            var elements2 = msg.Element<Matrix.Xmpp.XData.Field>(true);
            elements2.ShouldNotBeNull();            
        }
    }
}
