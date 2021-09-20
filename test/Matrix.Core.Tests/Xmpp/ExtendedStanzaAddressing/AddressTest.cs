using Matrix.Xml;
using Matrix.Xmpp.ExtendedStanzaAddressing;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.ExtendedStanzaAddressing
{
    
    public class AddressTest
    {
        [Fact]
        public void ElementShouldBeOfTypeAddress()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.ExtendedStanzaAddressing.address1.xml")).ShouldBeOfType<Address>();
        }

        [Fact]
        public void TestAddressProperties()
        {
            var add = XmppXElement.LoadXml(Resource.Get("Xmpp.ExtendedStanzaAddressing.address1.xml")).Cast<Address>();;
            Assert.True(add.Delivered);
            Assert.True(add.Type == Type.To);
            Assert.True(add.Jid.Equals("to@header1.org"));
            Assert.True(add.Node == "mynode");


        }
        
        [Fact]
        public void TestAddressWithoutDeliveredAttribute()
        {
            var add = XmppXElement.LoadXml(Resource.Get("Xmpp.ExtendedStanzaAddressing.address2.xml")).Cast<Address>();
            Assert.True(!add.Delivered);

            add = XmppXElement.LoadXml(Resource.Get("Xmpp.ExtendedStanzaAddressing.address3.xml")).Cast<Address>();
            Assert.True(!add.Delivered);
        }

       

        [Fact]
        public void TestAddressUri()
        {
            var add = XmppXElement.LoadXml(Resource.Get("Xmpp.ExtendedStanzaAddressing.address4.xml")).Cast<Address>();
            System.Uri uri = add.Uri;
            Assert.True(uri.ToString() == "xmpp://user@server.org/");
        }

        [Fact]
        public void TestBuildAddress()
        {
            var add = new Address
                          {
                              Node = "mynode",
                              Type = Type.To,
                              Jid = "to@header1.org",
                              Delivered = true,
                              Description = "dummy text"
                          };
            add.ShouldBe(Resource.Get("Xmpp.ExtendedStanzaAddressing.address1.xml"));
        }

        [Fact]
        public void TestMessageWithAddress()
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

            msg.ShouldBe(Resource.Get("Xmpp.ExtendedStanzaAddressing.address5.xml"));
        }
    }
}
