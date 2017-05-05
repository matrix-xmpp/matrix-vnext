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

using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using Matrix.Xml;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Register
{
    public class RegisterTest
    {
        [Fact]
        public void ShouldBeOfTypeRegister()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Register.register_query1.xml")).ShouldBeOfType<Matrix.Xmpp.Register.Register>();
        }

        [Fact]
        public void TestRegister()
        {
            Matrix.Xmpp.Register.Register reg1 = XmppXElement.LoadXml(Resource.Get("Xmpp.Register.register_query1.xml")).Cast<Matrix.Xmpp.Register.Register>();

            Assert.Equal(reg1.Instructions, "instructions");
            Assert.Equal(reg1.Username, "");
            Assert.Equal(reg1.Password, "");
            Assert.Equal(reg1.Email, "");
            Assert.Equal(reg1.Remove, false);
            Assert.Equal(reg1.Misc, null);
        }

        [Fact]
        public void TestRegister2()
        {
            Matrix.Xmpp.Register.Register reg1 = XmppXElement.LoadXml(Resource.Get("Xmpp.Register.register_query2.xml")).ShouldBeOfType<Matrix.Xmpp.Register.Register>();

            Assert.Equal(reg1.Instructions, "instructions");
            Assert.Equal(reg1.Username, "user");
            Assert.Equal(reg1.Password, "12345");
            Assert.Equal(reg1.Email, "user@email.com");
            Assert.Equal(reg1.Name, "name");
            Assert.Equal(reg1.First, "first");
            Assert.Equal(reg1.Last, "last");
            Assert.Equal(reg1.Nick, "nick");
            Assert.Equal(reg1.Misc, "misc");
            Assert.Equal(reg1.Remove, false);
        }

        [Fact]
        public void TestRegister3()
        {
            Matrix.Xmpp.Register.Register reg1 = XmppXElement.LoadXml(Resource.Get("Xmpp.Register.register_query3.xml")).ShouldBeOfType<Matrix.Xmpp.Register.Register>();

            Assert.Equal(reg1.Remove, true);            
        }

        [Fact]
        public void TestRegister4()
        {
            Matrix.Xmpp.Register.Register reg1 = XmppXElement.LoadXml(Resource.Get("Xmpp.Register.register_query4.xml")).ShouldBeOfType<Matrix.Xmpp.Register.Register>();

            Assert.Equal(reg1.XData != null, true);
        }

        [Fact]
        public void TestBuildRegisterIq()
        {
            var riq = new RegisterIq
            {
                Id = "reg1",
                Type = IqType.Get
            };
            riq.ShouldBe(Resource.Get("Xmpp.Register.register_query5.xml"));
          
            riq.Type = IqType.Result;
            riq.Register.Instructions = "Instructions";
            riq.Register.Username = "Alex";
            riq.Register.Password = "12345";
            riq.Register.Email = "alex@server.org";
            riq.ShouldBe(Resource.Get("Xmpp.Register.register_query6.xml"));
         }

        [Fact]
        public void TestBuildRegisterIq2()
        {
            var regIq = new RegisterIq { Type = IqType.Set, From = "bill@shakespeare.lit/globe", Id = "unreg1", Register = { Remove = true } };
            regIq.ShouldBe(Resource.Get("Xmpp.Register.register_query7.xml"));
        }
    }
}

