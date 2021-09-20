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

            Assert.Equal("instructions", reg1.Instructions);
            Assert.Equal("", reg1.Username);
            Assert.Equal("", reg1.Password);
            Assert.Equal("", reg1.Email);
            Assert.False(reg1.Remove);
            Assert.Null(reg1.Misc);
        }

        [Fact]
        public void TestRegister2()
        {
            Matrix.Xmpp.Register.Register reg1 = XmppXElement.LoadXml(Resource.Get("Xmpp.Register.register_query2.xml")).ShouldBeOfType<Matrix.Xmpp.Register.Register>();

            Assert.Equal("instructions", reg1.Instructions);
            Assert.Equal("user", reg1.Username);
            Assert.Equal("12345", reg1.Password);
            Assert.Equal("user@email.com", reg1.Email);
            Assert.Equal("name", reg1.Name);
            Assert.Equal("first", reg1.First);
            Assert.Equal("last", reg1.Last);
            Assert.Equal("nick", reg1.Nick);
            Assert.Equal("misc", reg1.Misc);
            Assert.False(reg1.Remove);
        }

        [Fact]
        public void TestRegister3()
        {
            Matrix.Xmpp.Register.Register reg1 = XmppXElement.LoadXml(Resource.Get("Xmpp.Register.register_query3.xml")).ShouldBeOfType<Matrix.Xmpp.Register.Register>();

            Assert.True(reg1.Remove);            
        }

        [Fact]
        public void TestRegister4()
        {
            Matrix.Xmpp.Register.Register reg1 = XmppXElement.LoadXml(Resource.Get("Xmpp.Register.register_query4.xml")).ShouldBeOfType<Matrix.Xmpp.Register.Register>();

            Assert.True(reg1.XData != null);
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

