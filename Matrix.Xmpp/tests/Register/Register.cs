using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Tests;
using Matrix.Xml;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Xmpp.Register
{
    [TestClass]
    public class Register
    {
        string xml1 = @"<query xmlns='jabber:iq:register'>
    <instructions>instructions</instructions>
    <username/>
    <password/>
    <email/>
  </query>";

        string xml2 = @"<query xmlns='jabber:iq:register'>
    <instructions>instructions</instructions>
    <username>user</username>
    <password>12345</password>
    <name>name</name>
    <first>first</first>
    <last>last</last>
    <email>user@email.com</email>
    <nick>nick</nick>
    <misc>misc</misc>
  </query>";

        string xml3 = @"<query xmlns='jabber:iq:register'>
    <remove/>
  </query>";

        // Register xdata test
        string xml4 = @"<query xmlns='jabber:iq:register'>
    <x xmlns='jabber:x:data' type='submit'>
      <field type='hidden' var='FORM_TYPE'>
        <value>jabber:iq:register</value>
      </field>
      <field type='text-single' label='Given Name' var='first'>
        <value>Juliet</value>
      </field>
      <field type='text-single' label='Family Name' var='last'>
        <value>Capulet</value>
      </field>
      <field type='text-single' label='Email Address' var='email'>
        <value>juliet@capulet.com</value>
      </field>
      <field type='list-single' label='Gender' var='x-gender'>
        <value>F</value>
      </field>
    </x>
  </query>";

       string xml5 = @"<iq type='get' id='reg1' xmlns='jabber:client'>
  <query xmlns='jabber:iq:register'/>
</iq>";

       string xml6 = @"<iq type='result' id='reg1' xmlns='jabber:client'>
  <query xmlns='jabber:iq:register'>
    <instructions>Instructions</instructions>
    <username>Alex</username>
    <password>12345</password>
    <email>alex@server.org</email>
  </query>
</iq>";

        private string xml7 = @"<iq xmlns='jabber:client' type='set' from='bill@shakespeare.lit/globe' id='unreg1'>
                  <query xmlns='jabber:iq:register'>
                    <remove/>
                  </query>
                </iq>";

        [TestMethod]
        public void Test()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(xml1);

            Assert.AreEqual(true, xmpp1 is Matrix.Xmpp.Register.Register);

            Matrix.Xmpp.Register.Register reg1 = xmpp1 as Matrix.Xmpp.Register.Register;

            Assert.AreEqual(reg1.Instructions, "instructions");
            Assert.AreEqual(reg1.Username, "");
            Assert.AreEqual(reg1.Password, "");
            Assert.AreEqual(reg1.Email, "");
            Assert.AreEqual(reg1.Remove, false);
            Assert.AreEqual(reg1.Misc, null);
        }

        [TestMethod]
        public void Test2()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(xml2);

            Assert.AreEqual(true, xmpp1 is Matrix.Xmpp.Register.Register);

            Matrix.Xmpp.Register.Register reg1 = xmpp1 as Matrix.Xmpp.Register.Register;

            Assert.AreEqual(reg1.Instructions, "instructions");
            Assert.AreEqual(reg1.Username, "user");
            Assert.AreEqual(reg1.Password, "12345");
            Assert.AreEqual(reg1.Email, "user@email.com");
            Assert.AreEqual(reg1.Name, "name");
            Assert.AreEqual(reg1.First, "first");
            Assert.AreEqual(reg1.Last, "last");
            Assert.AreEqual(reg1.Nick, "nick");
            Assert.AreEqual(reg1.Misc, "misc");

            Assert.AreEqual(reg1.Remove, false);
        }

        [TestMethod]
        public void Test3()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(xml3);

            Assert.AreEqual(true, xmpp1 is Matrix.Xmpp.Register.Register);

            Matrix.Xmpp.Register.Register reg1 = xmpp1 as Matrix.Xmpp.Register.Register;

            Assert.AreEqual(reg1.Remove, true);            
        }

        [TestMethod]
        public void Test4()
        {
            // xData  regform test
            XmppXElement xmpp1 = XmppXElement.LoadXml(xml4);

            Assert.AreEqual(true, xmpp1 is Matrix.Xmpp.Register.Register);

            Matrix.Xmpp.Register.Register reg1 = xmpp1 as Matrix.Xmpp.Register.Register;

            Assert.AreEqual(reg1.XData != null, true);
        }

        [TestMethod]
        public void Test5()
        {
            /*string xml5 = @"<iq type='get' id='reg1' xmlns='jabber:client'>
  <query xmlns='jabber:iq:register'/>
</iq>";
             */
             Matrix.Xmpp.Client.RegisterIq riq = new Matrix.Xmpp.Client.RegisterIq();
            riq.Id = "reg1";
            riq.Type = Matrix.Xmpp.IqType.Get;

            riq.ShouldBe(xml5);
          
            riq.Type = Matrix.Xmpp.IqType.Result;
            riq.Register.Instructions = "Instructions";
            riq.Register.Username = "Alex";
            riq.Register.Password = "12345";
            riq.Register.Email = "alex@server.org";

            riq.ShouldBe(xml6);
         }

        [TestMethod]
        public void Test6()
        {
            /* 
            * <iq type='set' from='bill@shakespeare.lit/globe' id='unreg1'>
                 <query xmlns='jabber:iq:register'>
                   <remove/>
                 </query>
               </iq>
            */
            var regIq = new RegisterIq { Type = IqType.Set, From = "bill@shakespeare.lit/globe", Id = "unreg1", Register = { Remove = true } };
            regIq.ShouldBe(xml7);
        }

    }
}

