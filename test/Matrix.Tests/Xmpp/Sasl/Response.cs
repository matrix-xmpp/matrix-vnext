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

using System.Text;
using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Sasl
{
    public class ResponseTest
    {
        [Fact]
        public void ShouldBeOfTypeResponse()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.response1.xml")).ShouldBeOfType<Response>();
        }

        [Fact]
        public void TestResponse()
        {
            var resp = XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.response1.xml")).Cast<Response>();

            byte[] bval = resp.Bytes;
            string sval = Encoding.ASCII.GetString(bval);
            Assert.Equal("dummy value", sval);
        }
    }
}
