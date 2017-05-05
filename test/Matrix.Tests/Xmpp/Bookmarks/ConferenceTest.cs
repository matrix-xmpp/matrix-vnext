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
using Matrix.Xmpp.Bookmarks;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Bookmarks
{
    public class ConferenceTest
    {
        [Fact]
        public void XmlShouldBeOfTypeConference()
        {
             XmppXElement.LoadXml(Resource.Get("Xmpp.Bookmarks.conference1.xml"))
                .ShouldBeOfType<Conference>();
        }

        [Fact]
        public void TestConferenceName()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Bookmarks.conference1.xml"))
                .Cast<Conference>()
                .Name.ShouldBe("Council of Oberon");
        }

        [Fact]
        public void TestConferenceAutoJoin()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Bookmarks.conference1.xml"))
                .Cast<Conference>()
                .AutoJoin.ShouldBeTrue();
        }

        [Fact]
        public void TestConferenceJid()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Bookmarks.conference1.xml"))
                .Cast<Conference>()
                .Jid.ToString().ShouldBe("council@conference.underhill.org");
        }

        [Fact]
        public void TestConferenceNickame()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Bookmarks.conference1.xml"))
                .Cast<Conference>()
                .Nickname
                .ShouldBe("Puck");
        }

        [Fact]
        public void TestConferencePassword()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Bookmarks.conference1.xml"))
                .Cast<Conference>()
                .Password
                .ShouldBe("secret");
        }

        [Fact]
        public void BuildConference()
        {
            var expectedXml = Resource.Get("Xmpp.Bookmarks.conference1.xml");
            new Conference
                {
                    Name = "Council of Oberon",
                    AutoJoin = true,
                    Jid = "council@conference.underhill.org",
                    Nickname = "Puck",
                    Password = "secret"
                }
                .ShouldBe(expectedXml);
        }
    }
}
