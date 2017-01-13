using System;
using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.MessageArchiving;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Archiving
{
    public class ArchivingTests
    {
        [Fact]
        public void XmlShoudbeOfTypeChat()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Archiving.chat.xml")).ShouldBeOfType<Chat>();
        }

        [Fact]
        public void TestChatSubject()
        {
            var chat = XmppXElement.LoadXml(Resource.Get("Xmpp.Archiving.chat.xml")).Cast<Chat>();
            Assert.Equal(chat.Subject, "She speaks!");
        }

        [Fact]
        public void TestChatWith()
        {
            var chat = XmppXElement.LoadXml(Resource.Get("Xmpp.Archiving.chat.xml")).Cast<Chat>();
            Assert.Equal(chat.With.ToString(), "juliet@capulet.com/chamber");
        }
        
        [Fact]
        public void TestChatItems()
        {
            var chat = XmppXElement.LoadXml(Resource.Get("Xmpp.Archiving.chat.xml")).Cast<Chat>();
            Assert.Equal(chat.GetItems().Count(), 3);
        }


        [Fact]
        public void TestChatToBody()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Archiving.chat.xml"))
                .Cast<Chat>()
                .Element<To>()
                .Body
                .ShouldBe("Neither, fair saint, if either thee dislike.");
        }

        [Fact]
        public void TestChatToSeconds()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Archiving.chat.xml"))
                .Cast<Chat>()
                .Element<To>()
                .Seconds.ShouldBe(11);
        }

        [Fact]
        public void TestChatFromBody()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Archiving.chat.xml"))
                .Cast<Chat>()
                .Element<From>()
                .Body.ShouldBe("Art thou not Romeo, and a Montague?");

        }

        [Fact]
        public void TestChatFromTimestamp()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Archiving.chat.xml"))
                .Cast<Chat>()
                .Element<From>()
                .TimeStamp.ToUniversalTime().ShouldBe(new DateTime(1469, 07, 21, 0, 32, 29, DateTimeKind.Utc));
        }

        [Fact]
        public void XmlShoudbeOfTypePref()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Archiving.pref.xml")).ShouldBeOfType<Preferences>();
        }
        
        [Fact]
        public void TestPreferenceProperties()
        {
            var prefs = XmppXElement.LoadXml(Resource.Get("Xmpp.Archiving.pref.xml")).Cast<Preferences>();
         
            Assert.Equal(prefs.Auto != null,true);
            Assert.Equal(prefs.Auto.Save, true);

            Assert.Equal(prefs.Default != null, true);
            Assert.Equal(prefs.Default.Expire, 31536000);
            Assert.Equal(prefs.Default.Otr == OtrType.Concede, true);
            Assert.Equal(prefs.Default.Save == SaveType.Body, true);

            Assert.Equal(prefs.Session != null, true);
            Assert.Equal(prefs.Session.Thread, "ffd7076498744578d10edabfe7f4a866");
            Assert.Equal(prefs.Default.Save == SaveType.Body, true);

            // first: <method type='auto' use='forbid'/> 
            Assert.Equal(prefs.GetMethods().Count(), 3);
            Assert.Equal(prefs.GetMethods().First().Type == MethodType.Auto, true);
            Assert.Equal(prefs.GetMethods().First().Use == UseType.Forbid, true);
        }

        [Fact]
        public void BuildListQuery()
        {
            var expectedXml = Resource.Get("Xmpp.Archiving.list.xml");
            new List { With = "juliet@capulet.com" }.ShouldBe(expectedXml);
        }

        [Fact]
        public void BuildIqQuery()
        {
            var expectedXml = Resource.Get("Xmpp.Archiving.iq1.xml");
            new Matrix.Xmpp.Client.Iq
            {
                Type = Matrix.Xmpp.IqType.Get,
                Id = "foo",
                Query = new List { With = "juliet@capulet.com" }
            }
            .ShouldBe(expectedXml);
        }

        [Fact]
        public void BuildRetrieveQuery()
        {
            var expectedXml = Resource.Get("Xmpp.Archiving.iq2.xml");
            new Matrix.Xmpp.Client.Iq
            {
                Type = Matrix.Xmpp.IqType.Get,
                Id = "page1",
                Query = new Retrieve
                {
                    With = "juliet@capulet.com/chamber",
                    Start = new DateTime(1469, 07, 21, 2, 56, 15, DateTimeKind.Utc),
                    ResultSet = new Matrix.Xmpp.ResultSetManagement.Set
                    {
                        Maximum = 100
                    }
                }
            }
            .ShouldBe(expectedXml);
        }
    }
}