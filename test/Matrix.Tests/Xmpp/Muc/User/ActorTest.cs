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

using Matrix.Xml;
using Matrix.Xmpp.Muc.User;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Muc.User
{
    public class ActorTest
    {
        [Fact]
        public void ShoudBeOfTypeActor()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Muc.User.actor1.xml")).ShouldBeOfType<Actor>();
        }

        [Fact]
        public void TestActor()
        {
            var a = XmppXElement.LoadXml(Resource.Get("Xmpp.Muc.User.actor1.xml")).Cast<Actor>();
            Assert.Equal(a.Jid.Equals("bard@shakespeare.lit"), true);
        }

        [Fact]
        public void TestBuildActor()
        {
            var act = new Actor("bard@shakespeare.lit");
            act.ShouldBe(Resource.Get("Xmpp.Muc.User.actor1.xml"));
        }       
    }
}
