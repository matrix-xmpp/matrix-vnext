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
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Base
{
    public class XmppXElementWithJidAttributeTest
    {
        [Fact]
        public void JidShouldNotBeNull()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Base.mucuser2.xml"))
                .Cast<Matrix.Xmpp.Muc.User.X>()
                .Item.Jid.ShouldNotBeNull();
        }

        [Fact]
        public void TestJid()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Base.mucuser2.xml"))
                .Cast<Matrix.Xmpp.Muc.User.X>()
                .Item.Jid.ToString().ShouldBe("server");
        }

        [Fact]
        public void JidShouldBeNull()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Base.mucuser1.xml"))
                .Cast<Matrix.Xmpp.Muc.User.X>()
                .Item.Jid.ShouldBeNull();
            

        }
    }
}
