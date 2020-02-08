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
using Matrix.Xmpp.Vcard;
using Shouldly;

namespace Matrix.Tests.Xmpp.Vcard
{
    public class TelephoneTest
    {
        [Fact]
        public void XmlShouldBeTypeOfTelephone()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.telephone1.xml")).ShouldBeOfType<Telephone>();
            XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.telephone2.xml")).ShouldBeOfType<Telephone>();
            XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.telephone3.xml")).ShouldBeOfType<Telephone>();
            XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.telephone4.xml")).ShouldBeOfType<Telephone>();
            XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.telephone5.xml")).ShouldBeOfType<Telephone>();
            XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.telephone6.xml")).ShouldBeOfType<Telephone>();
        }

        [Fact]
        public void TestTelephone()
        {
            Telephone tel1 = XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.telephone1.xml")).Cast<Telephone>();
            Assert.Equal(tel1.Number, "303-308-3282");
            Assert.Equal(tel1.IsVoice, true);
            Assert.Equal(tel1.IsWork, true);
            Assert.Equal(tel1.IsHome, false);
            
            Telephone tel2 = XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.telephone2.xml")).Cast<Telephone>();
            Assert.Equal(tel2.Number, "12345");
            Assert.Equal(tel2.IsWork, true);
            Assert.Equal(tel2.IsFax, true);

            Telephone tel3 = XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.telephone3.xml")).Cast<Telephone>();
            Assert.Equal(tel3.Number, "67890");
            Assert.Equal(tel2.IsWork, true);
            Assert.Equal(tel2.IsMessagingService, false);
        }

        [Fact]
        public void TestBuildTelephone()
        {           
            Telephone tel1 = new Telephone("12345", true);     
            tel1.ShouldBe(Resource.Get("Xmpp.Vcard.telephone4.xml"));

            Telephone tel2 = new Telephone("12345", true) {IsHome = true};
            tel2.ShouldBe(Resource.Get("Xmpp.Vcard.telephone5.xml"));

            Telephone tel3 = new Telephone("12345", true) {IsWork = true};
            tel3.ShouldBe(Resource.Get("Xmpp.Vcard.telephone6.xml"));
        }
    }
}
