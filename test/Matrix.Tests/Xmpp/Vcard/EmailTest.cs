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
    
    public class EmailTest
    {
        [Fact]
        public void XmlShouldBeTypeOfEmail()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.email1.xml")).ShouldBeOfType<Email>();
            XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.email2.xml")).ShouldBeOfType<Email>();
        }

        [Fact]
        public void TestEmail()
        {
            Email email1 = XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.email1.xml")).Cast<Email>();
            email1.Address.ShouldBe("info@ag-software.de");
            email1.IsPreferred.ShouldBeFalse();


            Email email2 = XmppXElement.LoadXml(Resource.Get("Xmpp.Vcard.email2.xml")).Cast<Email>();
            email2.Address.ShouldBe("stpeter@jabber.org");
            email2.IsPreferred.ShouldBeTrue();
           
            email1.ShouldBe(Resource.Get("Xmpp.Vcard.email1.xml"));

            Email email4 = new Email("stpeter@jabber.org", true);
            email4.ShouldBe(Resource.Get("Xmpp.Vcard.email2.xml"));
        }
    }
}
