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

using System.Text;
using Matrix.Xml;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Sasl
{
    
    public class AuthTest
    {
        [Fact]
        public void ShouldBeOfTypeAuth()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.auth1.xml")).ShouldBeOfType<Matrix.Xmpp.Sasl.Auth>();
        }

        [Fact]
        public void TestAuth()
        {
            var resp = XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.auth1.xml")).Cast<Matrix.Xmpp.Sasl.Auth>();

            byte[] bval = resp.Bytes;
            string sval = Encoding.ASCII.GetString(bval);
            Assert.Equal("dummy value", sval);
        }

        [Fact]
        public void TestBuildAuth()
        {
            new Matrix.Xmpp.Sasl.Auth { Bytes = Encoding.ASCII.GetBytes("dummy value") }
                .ShouldBe(Resource.Get("Xmpp.Sasl.auth1.xml"));
        }
    }
}
