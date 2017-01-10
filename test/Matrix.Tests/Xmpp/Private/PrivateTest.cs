using Matrix.Xml;
using Xunit;

namespace Matrix.Tests.Xmpp.Private
{
    
    public class PrivateTest
    {
        private const string XML1
         = @"<query xmlns='jabber:iq:private'>
                <storage xmlns='storage:bookmarks'/>
             </query>";

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.Private.Private);

            var priv = xmpp1 as Matrix.Xmpp.Private.Private;
            if (priv != null)
            {
                
            }
        }
    }
}