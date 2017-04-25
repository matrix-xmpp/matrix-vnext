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

using Matrix.Xmpp.Client;
using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.Muc;
using X = Matrix.Xmpp.Muc.X;

using Xunit;
using Shouldly;

namespace Matrix.Tests.Xmpp.Muc
{
    
    public class XTest
    {
        [Fact]
        public void ShoudBeOfTypeX()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Muc.x1.xml")).ShouldBeOfType<X>();
        }

        [Fact]
        public void TestX()
        {
            var x = XmppXElement.LoadXml(Resource.Get("Xmpp.Muc.x1.xml")).Cast<X>();
            Assert.Equal(x.Password, "secret");

            x = XmppXElement.LoadXml(Resource.Get("Xmpp.Muc.x2.xml")).Cast<X>();
            Assert.Equal(x.History.MaxStanzas, 20);
        }


        [Fact]
        public void TestBuildX()
        {
            var x = new X(new History(20));
            x.ShouldBe(Resource.Get("Xmpp.Muc.x2.xml"));

            x = new X("secret");
            x.ShouldBe(Resource.Get("Xmpp.Muc.x1.xml"));
        }
        

        [Fact]
        public void TestMucInvite()
        {
            var msg = XmppXElement.LoadXml(Resource.Get("Xmpp.Muc.message1.xml")).Cast<Message>();
            var xMuc = msg.Element<Matrix.Xmpp.Muc.User.X>();
            Assert.Equal(xMuc.Password, "cauldronburn");

            var invite = xMuc.GetInvites().First();
            Assert.Equal(invite.Reason, "FOO");
        }
    }
}
