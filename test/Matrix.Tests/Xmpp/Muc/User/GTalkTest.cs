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
using Matrix.Xmpp.Client;
using Xunit;

namespace Matrix.Tests.Xmpp.Muc.User
{
    public class GTalkTest
    {
        [Fact]
        public void TestGtalkPresence()
        {
            var pres = XmppXElement.LoadXml(Resource.Get("Xmpp.Muc.User.presence_gtalk1.xml")).Cast<Presence>();

            Assert.Equal(pres.Show == Matrix.Xmpp.Show.DoNotDisturb, true);
            Assert.Equal(pres.Nick.Value, "Alex");
            
            var mucUser = pres.MucUser;
            var item = mucUser.Item;
            Assert.Equal(item.Role == Matrix.Xmpp.Muc.Role.Participant, true);
            Assert.Equal(item.Affiliation == Matrix.Xmpp.Muc.Affiliation.Member, true);
            Assert.Equal(item.Jid == "XXX@gmail.com/gmail.59477926", true);
        }
    }
}
