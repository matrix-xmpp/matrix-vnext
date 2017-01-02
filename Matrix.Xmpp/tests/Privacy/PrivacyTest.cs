using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Matrix.Xml;
using Matrix.Xmpp.Privacy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Xmpp.Privacy
{
    [TestClass]
    public class PrivacyTest
    {
        private const string XML1 = @"<query xmlns='jabber:iq:privacy'>
                                  <active name='private'/>
                                  <default name='public'/>
                                  <list name='public'/>
                                  <list name='private'/>
                                  <list name='special'/>
                                </query>";

        [TestMethod]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.AreEqual(true, xmpp1 is Matrix.Xmpp.Privacy.Privacy);

            var priv = xmpp1 as Matrix.Xmpp.Privacy.Privacy;
            Assert.AreEqual(priv.Default.Name, "public");
            Assert.AreEqual(priv.Active.Name, "private");

            priv.Active = null;
            priv.Default = null;

            Console.WriteLine("Test");
            //Assert.AreEqual(item.Action, Matrix.Xmpp.Privacy.Action.deny);
            //Assert.AreEqual(item.Order, 5);
            //Assert.AreEqual(item.Type, Matrix.Xmpp.Privacy.Type.subscription);
        }
    }
}
