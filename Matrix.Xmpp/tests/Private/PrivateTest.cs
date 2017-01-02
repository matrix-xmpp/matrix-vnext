using Matrix.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matrix.Xmpp.Tests.Private
{
    [TestClass]
    public class PrivateTest
    {
        private const string XML1
         = @"<query xmlns='jabber:iq:private'>
                <storage xmlns='storage:bookmarks'/>
             </query>";

        [TestMethod]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.AreEqual(true, xmpp1 is Matrix.Xmpp.Private.Private);

            var priv = xmpp1 as Matrix.Xmpp.Private.Private;
            if (priv != null)
            {
                
            }
        }
    }
}