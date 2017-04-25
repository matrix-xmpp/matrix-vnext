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

using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.SecurityLabels;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.SecurityLabels
{
    public class SecurityLabelsTests
    {
        [Fact]
        public void TestMessageWuthSecurityLabel()
        {
            var msg = XmppXElement.LoadXml(Resource.Get("Xmpp.SecurityLabels.message1.xml")).Cast<Message>();
            var secLabel = msg.Element<SecurityLabel>();

            Assert.Equal(secLabel != null, true);

            var display = secLabel.DisplayMarking;
            Assert.Equal(display.ForegroundColor == Color.Black, true);
            Assert.Equal(display.BackgroundColor == Color.Red, true);
            Assert.Equal(display.Value, "SECRET");

            var label = secLabel.Label;
            var ess = label.EssSecurityLabel;
            Assert.Equal(ess.Value.Trim(), "MQYCAQQGASk=");
        }

        [Fact]
        public void TestBuildMessageWithSecurityLabel()
        {
            new Message
            {
                Body = "This content is classified.",
                SecurityLabel = new SecurityLabel
                {
                    DisplayMarking = new DisplayMarking
                    {
                        ForegroundColor = Color.Black,
                        BackgroundColor = Color.Red,
                        Value = "SECRET"
                    },
                    Label = new Label
                    {
                        EssSecurityLabel = new EssSecurityLabel {Value = "MQYCAQQGASk="}
                    }
                }
            }
            .ShouldBe(Resource.Get("Xmpp.SecurityLabels.message1.xml"));
        }

        [Fact]
        public void TestShouldbeOfTypeCatalog()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.SecurityLabels.catalog1.xml")).ShouldBeOfType<Catalog>();
        }

        [Fact]
        public void TestCatalog()
        {
            var catalog = XmppXElement.LoadXml(Resource.Get("Xmpp.SecurityLabels.catalog1.xml")).Cast<Catalog>();

            Assert.Equal(catalog != null, true);
            Assert.Equal(catalog.To.Bare, "example.com");
            Assert.Equal(catalog.CatalogName, "Default");
            Assert.Equal(catalog.Description, "an example set of labels");
            Assert.Equal(catalog.Restrictive, false);

            var items = catalog.GetItems();
            Assert.Equal(items.Count(), 4);
            var item1 = items.ElementAt(0);
            Assert.Equal(item1.Selector, "Classified|SECRET");
            var seclabel1 = item1.SecurityLabel;
            Assert.Equal(seclabel1.DisplayMarking.ForegroundColor == Color.Black, true);
            Assert.Equal(seclabel1.DisplayMarking.BackgroundColor == Color.Red, true);
            Assert.Equal(seclabel1.DisplayMarking.Value, "SECRET");

            var item2 = items.ElementAt(1);
            Assert.Equal(item2.Selector, "Classified|CONFIDENTIAL");
            var seclabel2 = item2.SecurityLabel;
            Assert.Equal(seclabel2.DisplayMarking.ForegroundColor == Color.Black, true);
            Assert.Equal(seclabel2.DisplayMarking.BackgroundColor == Color.Navy, true);
            Assert.Equal(seclabel2.DisplayMarking.Value, "CONFIDENTIAL");

            var item3 = items.ElementAt(2);
            Assert.Equal(item3.Selector, "Classified|RESTRICTED");
            var seclabel3 = item3.SecurityLabel;
            Assert.Equal(seclabel3.DisplayMarking.ForegroundColor == Color.Black, true);
            Assert.Equal(seclabel3.DisplayMarking.BackgroundColor == Color.Aqua, true);
            Assert.Equal(seclabel3.DisplayMarking.Value, "RESTRICTED");
            
            var item4 = items.ElementAt(3);
            Assert.Equal(item4.Selector, "UNCLASSIFIED");
        }

        [Fact]
        public void TestSecurityLabelWithUnknownColor()
        {
            var label = XmppXElement.LoadXml(Resource.Get("Xmpp.SecurityLabels.securitylabel1.xml")).Cast<SecurityLabel>();
            var display = label.DisplayMarking;
            Assert.Equal(display.ForegroundColor == Color.UnknownColor, true);
        }
    }
}
