using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;


namespace Test.Xmpp.Chatstates
{
    [TestClass]
    public class ChatstatesTest
    {
        public const string XML1 =
            @"<message 
                    from='bernardo@shakespeare.lit/pda'
                    to='francisco@shakespeare.lit'
                    type='chat' xmlns='jabber:client'>
                  <body>Who's there?</body>
                  <active xmlns='http://jabber.org/protocol/chatstates'/>
                </message>";

        public const string XML2 =
            @"<message 
                    from='bernardo@shakespeare.lit/pda'
                    to='francisco@shakespeare.lit'
                    type='chat' xmlns='jabber:client'>
                  <body>Who's there?</body>                  
                </message>";

        public const string XML3 =
           @"<message type='chat' xmlns='jabber:client'> 
                <gone xmlns='http://jabber.org/protocol/chatstates'/>                 
                </message>";

        [TestMethod]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);

            Assert.AreEqual(true, xmpp1 is Message);
            var msg = xmpp1 as Message;

            Assert.AreEqual(msg.Chatstate == Matrix.Xmpp.Chatstates.Chatstate.Active, true);
            Assert.AreEqual(msg.Chatstate == Matrix.Xmpp.Chatstates.Chatstate.Composing, false);
            Assert.AreEqual(msg.Chatstate == Matrix.Xmpp.Chatstates.Chatstate.Gone, false);

            msg.Chatstate = Matrix.Xmpp.Chatstates.Chatstate.None;
            
            msg.ShouldBe(XML2);
        }

        [TestMethod]
        public void Test2()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML2);

            Assert.AreEqual(true, xmpp1 is Message);
            var msg = xmpp1 as Message;

            Assert.AreEqual(msg.Chatstate == Matrix.Xmpp.Chatstates.Chatstate.None, true);

            msg.Chatstate = Matrix.Xmpp.Chatstates.Chatstate.Active;

            msg.ShouldBe(XML1);
        }

        [TestMethod]
        public void Test3()
        {
            var msg = new Message
                          {Type = Matrix.Xmpp.MessageType.Chat, Chatstate = Matrix.Xmpp.Chatstates.Chatstate.Gone};

            msg.ShouldBe(XML3);
        }
    }
}