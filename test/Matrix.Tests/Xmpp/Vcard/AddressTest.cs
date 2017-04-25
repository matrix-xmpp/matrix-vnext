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

using Xunit;
using Matrix.Xml;
using Matrix.Xmpp.Vcard;
using Shouldly;

namespace Matrix.Tests.Xmpp.Vcard
{
    public class AddressTest
    {
        [Fact]
        public void XmlShouldBeTypeOfEmail()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.address1.xml")).ShouldBeOfType<Address>();
        }

        [Fact]
        public void TestAddress()
        {
            Address address1 = XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.address1.xml")).Cast<Address>();
            Assert.Equal(address1.IsWork, true);
            Assert.Equal(address1.IsHome, false);
            Assert.Equal(address1.ExtraAddress, "Suite 600");
            Assert.Equal(address1.Street, "1899 Wynkoop Street");
            Assert.Equal(address1.Locality, "Denver");
            Assert.Equal(address1.Region, "CO");
            Assert.Equal(address1.PostCode, "80202");
            Assert.Equal(address1.Country, "USA");
        }
    }
}
