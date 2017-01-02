using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Matrix.Xml;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Xmpp.Auth
{
    [TestClass]
    public class AuthTest
    {
        const string XML = @"<query xmlns='jabber:iq:auth'><username>gnauck</username><digest/></query>";
        
        [TestMethod]
        public void Test1()
        {
            var el = XmppXElement.LoadXml(XML);
            Assert.IsTrue(el is Matrix.Xmpp.Auth.Auth);

            var auth = el as Matrix.Xmpp.Auth.Auth;
            Assert.AreEqual(auth.Username, "gnauck");
            Assert.IsTrue(auth.HasTag("digest"));
            Assert.IsTrue(auth.Digest != null);
            //auth.Digest = null;
            //Assert.IsTrue(auth.Digest == null);

        }
    }
}
