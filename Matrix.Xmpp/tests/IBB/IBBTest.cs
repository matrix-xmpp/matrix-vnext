using Matrix.Xml;
using Matrix.Xmpp.IBB;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matrix.Xmpp.Tests.IBB
{
    [TestClass]
    public class IBBTest
    {
        const string XML1 = @"<open xmlns='http://jabber.org/protocol/ibb' block-size='4096' sid='i781hf64' stanza='iq'/>";
        const string XML2 = @"<open xmlns='http://jabber.org/protocol/ibb' block-size='4096' sid='i781hf64'/>";

        const string XML3 = @"<close xmlns='http://jabber.org/protocol/ibb' sid='i781hf64'/>";

        const string XML4 = @"<data xmlns='http://jabber.org/protocol/ibb' seq='99' sid='i781hf64'/>";

        [TestMethod]
        public void Test1()
        {
            var xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.AreEqual(true, xmpp1 is Open);

            var open = xmpp1 as Open;
            if (open != null)
            {
                Assert.AreEqual(open.BlockSize, 4096);
                Assert.AreEqual(open.Sid, "i781hf64");
                Assert.AreEqual(open.Stanza, StanzaType.Iq);
            }
        }

        [TestMethod]
        public void Test2()
        {
            var xmpp1 = XmppXElement.LoadXml(XML2);
            Assert.AreEqual(true, xmpp1 is Open);

            var open = xmpp1 as Open;
            if (open != null)
            {
                Assert.AreEqual(open.BlockSize, 4096);
                Assert.AreEqual(open.Sid, "i781hf64");
                Assert.AreEqual(open.Stanza, StanzaType.Iq);
            }
        }

        [TestMethod]
        public void Test3()
        {
            var open = new Open {BlockSize = 4096, Sid = "i781hf64", Stanza = StanzaType.Iq};
            open.ShouldBe(XML1);
            
            var open2 = new Open { BlockSize = 4096, Sid = "i781hf64" };
            open2.ShouldBe(XML2);
        }

        [TestMethod]
        public void Test4()
        {
            var xmpp1 = XmppXElement.LoadXml(XML3);
            Assert.AreEqual(true, xmpp1 is Close);

            var close = xmpp1 as Close;
            if (close != null)
            {
                Assert.AreEqual(close.Sid, "i781hf64");
            }
        }

        [TestMethod]
        public void Test5()
        {
            var open = new Close {Sid = "i781hf64"};
            open.ShouldBe(XML3);
        }

        [TestMethod]
        public void Test6()
        {
            var xmpp1 = XmppXElement.LoadXml(XML4);
            Assert.AreEqual(true, xmpp1 is Data);

            var data = xmpp1 as Data;
            if (data != null)
            {
                Assert.AreEqual(data.Sid, "i781hf64");
                Assert.AreEqual(data.Sequence, 99);
            }
        }

        [TestMethod]
        public void Test7()
        {
            var data = new Data { Sid = "i781hf64", Sequence = 99 };
            data.ShouldBe(XML4);
        }
    }
}
