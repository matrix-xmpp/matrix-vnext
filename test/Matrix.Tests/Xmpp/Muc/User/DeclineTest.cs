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

using Xunit;
using Matrix.Xml;
using Matrix.Xmpp.Muc.User;
using Shouldly;

namespace Matrix.Tests.Xmpp.Muc.User
{
    
    public class DeclineTest
    {
        [Fact]
        public void ShoudBeOfTypeDecline()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Muc.User.decline1.xml")).ShouldBeOfType<Decline>();
        }

        [Fact]
        public void TestDecline()
        {
            var d = XmppXElement.LoadXml(Resource.Get("Xmpp.Muc.User.decline1.xml")).Cast<Decline>();
            Assert.Equal(d.Reason, "Sorry, I'm too busy right now.");
            Assert.Equal(d.To.Equals("crone1@shakespeare.lit"), true);
        }

        [Fact]
        public void TestBuildDecline()
        {
            var dec = new Decline(new Jid("crone1@shakespeare.lit"), "Sorry, I'm too busy right now.");
            dec.ShouldBe(Resource.Get("Xmpp.Muc.User.decline1.xml"));
        }
    }
}
