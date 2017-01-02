using Matrix.Xml;
using Matrix.Xmpp.ExtendedStanzaAddressing;
using Matrix.Xmpp.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Xmpp.ExtendedStanzaAddressing
{
    [TestClass]
    public class AddressTest
    {
        private const string XML1 = @" <address xmlns='http://jabber.org/protocol/address' type='to' jid='to@header1.org' delivered='true' node='mynode' desc='dummy text'/>";
        private const string XML2 = @" <address xmlns='http://jabber.org/protocol/address' type='to' jid='to@header1.org'/>";
        private const string XML3 = @" <address xmlns='http://jabber.org/protocol/address' type='to' jid='to@header1.org' delivered='foo'/>";

        private const string XML4 = @" <address xmlns='http://jabber.org/protocol/address' type='to' uri='xmpp://user@server.org'/>";

        private const string XML5 = @"<message xmlns='jabber:client' to='multicast.jabber.org'>
   <addresses xmlns='http://jabber.org/protocol/address'>
       <address type='to' jid='hildjj@jabber.org/Work' desc='Joe Hildebrand'/>
       <address type='cc' jid='jer@jabber.org/Home' desc='Jeremie Miller'/>
   </addresses>
    <body>Hello, world!</body></message>
";
        [TestMethod]
        public void Test1()
        {
            var el = XmppXElement.LoadXml(XML1);
            Assert.IsTrue(el is Address);
            var add = el as Address;
            Assert.IsTrue(add.Delivered);
            Assert.IsTrue(add.Type == Matrix.Xmpp.ExtendedStanzaAddressing.Type.To);
            Assert.IsTrue(add.Jid.Equals("to@header1.org"));
            Assert.IsTrue(add.Node == "mynode");
        }
        
        [TestMethod]
        public void Test_Without_Delivered()
        {
            var el = XmppXElement.LoadXml(XML2);
            Assert.IsTrue(el is Address);
            var add = el as Address;
            Assert.IsTrue(!add.Delivered);
        }

        [TestMethod]
        public void Test_Wrong_Delivered_Attribute()
        {
            var el = XmppXElement.LoadXml(XML3);
            Assert.IsTrue(el is Address);
            var add = el as Address;
            Assert.IsTrue(!add.Delivered);
        }

        [TestMethod]
        public void Test_Uri()
        {
            var el = XmppXElement.LoadXml(XML4);
            Assert.IsTrue(el is Address);
            var add = el as Address;
            System.Uri uri = add.Uri;
            Assert.IsTrue(uri.ToString() == "xmpp://user@server.org/");
        }

        [TestMethod]
        public void Test_Build_Address()
        {
            var add = new Address
                          {
                              Node = "mynode",
                              Type = Matrix.Xmpp.ExtendedStanzaAddressing.Type.To,
                              Jid = "to@header1.org",
                              Delivered = true,
                              Description = "dummy text"
                          };
            add.ShouldBe(XML1);
        }

        [TestMethod]
        public void Test_Message_With_Address()
        {
            var addresses = new Addresses();
            addresses.AddAddress(new Address
                          {
                              Type = Type.To,
                              Jid = "hildjj@jabber.org/Work",
                              Description = "Joe Hildebrand"
                          });

            addresses.AddAddress(new Address
                    {
                        Type = Type.Cc,
                        Jid = "jer@jabber.org/Home",
                        Description = "Jeremie Miller"
                    });

            var msg = new Matrix.Xmpp.Client.Message
                {
                    To = "multicast.jabber.org",
                    Body = "Hello, world!",
                    Addresses = addresses
                };

            msg.ShouldBe(XML5);
        }
    }
}
