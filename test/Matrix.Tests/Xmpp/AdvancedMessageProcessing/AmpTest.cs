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
using Matrix.Xmpp.AdvancedMessageProcessing;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.AdvancedMessageProcessing
{
    public class AmpTest
    {
        [Fact]
        public void TestXmlShouldBeOfTypeAmp()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.AdvancedMessageProcessing.amp1.xml")).ShouldBeOfType<Amp>();
        }

        [Fact]
        public void PerHopShouldBeTrue()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.AdvancedMessageProcessing.amp1.xml"))
                .Cast<Amp>()
                .PerHop.ShouldBeTrue();
        }

        [Fact]
        public void BuildAmpElement()
        {
            string expectedXml = Resource.Get("Xmpp.AdvancedMessageProcessing.amp3.xml");
            new Amp().ShouldBe(expectedXml);
        }

        [Fact]
        public void BuildAmpElementWithPerHopAttribute()
        {
            string expectedXml = Resource.Get("Xmpp.AdvancedMessageProcessing.amp1.xml");
            new Amp { PerHop = true }.ShouldBe(expectedXml);
        }

        [Fact]
        public void TestFrom()
        {
            var amp = XmppXElement.LoadXml(Resource.Get("Xmpp.AdvancedMessageProcessing.amp2.xml")).Cast<Amp>();
            amp.From.Equals("bernardo@hamlet.lit/elsinore").ShouldBeTrue(); 
        }

        [Fact]
        public void TestTo()
        {
            var amp = XmppXElement.LoadXml(Resource.Get("Xmpp.AdvancedMessageProcessing.amp2.xml")).Cast<Amp>();
            amp.To.Equals("francisco@hamlet.lit/pda").ShouldBeTrue();
        }

        [Fact]
        public void BuildAmpWithToFromStatus()
        {
            string expectedXml = Resource.Get("Xmpp.AdvancedMessageProcessing.amp4.xml");
            new Amp
            {
                Status = Matrix.Xmpp.AdvancedMessageProcessing.Action.Alert,
                From = "bernardo@hamlet.lit/elsinore",
                To = "francisco@hamlet.lit"
            }
            .ShouldBe(expectedXml);
        }
    }
}
