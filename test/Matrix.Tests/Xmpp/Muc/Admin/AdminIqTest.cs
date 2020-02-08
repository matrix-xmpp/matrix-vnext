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
using Matrix.Xmpp.Muc.Admin;
using Shouldly;

namespace Matrix.Tests.Xmpp.Muc.Admin
{
    public class AdminIqTest
    {
        [Fact]
        public void ShoudBeOfTypeAdminQuery()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Muc.Admin.admin_query1.xml")).ShouldBeOfType<AdminQuery>();
        }

        [Fact]
        public void TestBuildAdminQuery()
        {
            var aIq = new AdminIq();
            aIq.AdminQuery.AddItem(new Item(Matrix.Xmpp.Muc.Role.None, "pistol", "my reason!"));
            aIq.Id = "1";
            aIq.ShouldBe(Resource.Get("Xmpp.Muc.Admin.admin_iq1.xml"));
        }
    }
}
