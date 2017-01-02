using Matrix.Xml;
using Matrix.Xmpp.MessageCarbons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shouldly;

namespace Matrix.Xmpp.Tests.MessageCarbons
{
    [TestClass]
    public class MessageCarbonTest
    {
        string xml1 = @"<message from='JID' to='JID' xmlns='jabber:client' type='chat'>
   <sent xmlns = 'urn:xmpp:carbons:2'>
     <forwarded xmlns='urn:xmpp:forward:0'>
       <message from = 'gnauck@jabber.org' id='6c2126bc-20b8-4a33-b921-d2556fe923d6' to='resouce' type='chat' xmlns='jabber:client'>
         <body>blarg</body>
       </message>
     </forwarded>
   </sent>
 </message>";

        [TestMethod]
        public void ParseTest()
        {
            var msg = XmppXElement.LoadXml(xml1);
            var forwarded = ((ForwardContainer) (msg.FirstXmppXElement))
                .Forwarded;

            forwarded.ShouldNotBeNull();
            
        }
    }
}
