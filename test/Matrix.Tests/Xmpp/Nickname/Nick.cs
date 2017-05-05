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
using Shouldly;

namespace Matrix.Tests.Xmpp.Nickname
{
    public class Nick
    {
        [Fact]
        public void ShoudBeOfTypeX()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Nickname.nick1.xml")).ShouldBeOfType<Matrix.Xmpp.Nickname.Nick>();
        }

        [Fact]
        public void TestNick()
        {
            Matrix.Xmpp.Nickname.Nick nick1 = XmppXElement.LoadXml(Resource.Get("Xmpp.Nickname.nick1.xml")).Cast<Matrix.Xmpp.Nickname.Nick>();
            Assert.Equal(nick1 == "Ishmael", true);
        }

        [Fact]
        public void TestBuildNick()
        {
            Matrix.Xmpp.Nickname.Nick nick1 = "Alex";
            Assert.Equal(nick1.Value, "Alex");
            Assert.Equal(nick1 == "Alex", true);

            Matrix.Xmpp.Nickname.Nick nick2 = new Matrix.Xmpp.Nickname.Nick();
            nick2 = "Ishmael";
            Assert.Equal(nick2.Value, "Ishmael");
            Assert.Equal(nick2 == "Ishmael", true);

            Matrix.Xmpp.Nickname.Nick nick3 = new Matrix.Xmpp.Nickname.Nick("Alex");
            Assert.Equal(nick3.Value, "Alex");
            Assert.Equal(nick3 == "Alex", true);
        }

        [Fact]
        public void TestNickInPresence()
        {
            Matrix.Xmpp.Client.Presence pres = XmppXElement.LoadXml(Resource.Get("Xmpp.Nickname.presence1.xml")).Cast<Matrix.Xmpp.Client.Presence>();
            Matrix.Xmpp.Nickname.Nick nick1 = pres.Nick;
            Assert.Equal(nick1 == "Ishmael", true);
        }

        [Fact]
        public void TestBuildPresenceWithNick()
        {
            Matrix.Xmpp.Client.Presence pres = new Matrix.Xmpp.Client.Presence { Nick = "Alex" };

            Assert.Equal(pres.Nick.Value, "Alex");

            pres.Nick.Value = "Ishmael";
            Assert.Equal(pres.Nick.Value, "Ishmael");
        }
    }
}
