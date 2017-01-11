using Xunit;
using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.Bookmarks;

namespace Matrix.Tests.Xmpp.Bookmarks
{
    public class StorageTest
    {
        private const string XML1
          = @"<storage xmlns='storage:bookmarks'>
                <conference name='matrix' jid='matrix@conference.ag-software.de' >
                    <nick>Alex</nick>
                </conference>
                <conference name='dev' jid='dev@conference.ag-software.de' >
                    <nick>Alex</nick>
                </conference>
                <conference name='jdev' jid='jdev@conference.jabber.org' >
                    <nick>Alex</nick>
                </conference>
                <conference name='jorg-muc@isode' jid='jorg-muc@talk.isode.com' >
                    <nick>Alex</nick>
                </conference>
            </storage>";

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Storage);

            var store = xmpp1 as Storage;
            if (store != null)
            {
                Assert.Equal(store.GetConferences().Count(), 4);
                Assert.NotEqual(store.GetConferences().Count(), 5);
            }
        }
    }
}