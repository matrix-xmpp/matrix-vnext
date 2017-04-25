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
using Matrix.Xmpp.Sasl;
using Xunit;
using Shouldly;

namespace Matrix.Tests.Xmpp.Sasl
{
    public class MechanismsTest
    {
        [Fact]
        public void ShouldBeOfTypeMechanisms()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.mechanisms1.xml")).ShouldBeOfType<Mechanisms>();
            XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.mechanisms2.xml")).ShouldBeOfType<Mechanisms>();
            XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.mechanisms3.xml")).ShouldBeOfType<Mechanisms>();
        }

        [Fact]
        public void TestMechanisms1()
        {
            var mechs = XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.mechanisms1.xml")).Cast<Mechanisms>();
            Assert.Equal(mechs.SupportsMechanism(SaslMechanism.DigestMd5), true);
            Assert.Equal(mechs.SupportsMechanism(SaslMechanism.Plain), true);
            Assert.Equal(mechs.SupportsMechanism(SaslMechanism.Gssapi), true);
            Assert.Equal(mechs.SupportsMechanism(SaslMechanism.Anonymous), false);
            Assert.Equal(mechs.SupportsMechanism(SaslMechanism.XGoogleToken), false);
        }

        [Fact]
        public void TestMechanisms2()
        {
            var mechanisms = XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.mechanisms2.xml")).ShouldBeOfType<Mechanisms>();
            Assert.Equal(mechanisms.PrincipalHostname, "auth42.us.example.com");
        }

        [Fact]
        public void TestMechanisms3()
        {
            var mechanisms = XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.mechanisms3.xml")).ShouldBeOfType<Mechanisms>();
            Assert.Equal(mechanisms.PrincipalHostname, "auth43.us.example.com");
        }
    }
}
