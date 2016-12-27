using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matrix.Core.Tests
{
    [TestClass]
    public class JidTests
    {

        [TestMethod]
        public void FullJidTestTest()
        {
            var jid1 = "user@server.de/resource";
            Jid jid = jid1;
            Assert.AreEqual(jid.ToString(), jid1);
            Assert.AreEqual((string)jid, jid1);
        }

        [TestMethod]
        public void BuilldJidFromUserAndServerTest()
        {
            const string user = "user";
            const string server = "server.com";

            var jid = new Jid { User = user, Server = server };
            Assert.AreEqual(jid.ToString(), user + "@" + server);
        }

        [TestMethod]
        public void JidEscapeTest()
        {
            const string user = "gnauck@ag-software.de";
            const string server = "server.com";

            var jid = new Jid();
            jid.SetUser(user);
            Assert.AreEqual(jid.User, @"gnauck\40ag-software.de");

            jid.SetServer(server);
            Assert.AreEqual(jid.Server, @"server.com");
            Assert.AreEqual(jid.ToString(), @"gnauck\40ag-software.de@server.com");
        }

        [TestMethod]
        public void UppercaseUsernameTest()
        {
            const string user = "Gnauck";
            const string server = "Server.com";

            var jid = new Jid { User = user };
            Assert.AreEqual(jid.User, user);

            jid.Server = server;
            Assert.AreEqual(jid.Server, server);
            Assert.AreEqual(jid.ToString(), user + "@" + server);

            jid.SetUser(user);
            jid.SetServer(server);
            Assert.AreEqual(jid.ToString(), "gnauck@server.com");
        }

        [TestMethod]
        public void CloneJidTest()
        {
            const string JID1 = "user@server.de/resource";
            const string JID2 = "user2@server.de/resource2";

            Jid jid1 = JID1;
            var jid2 = jid1.Clone();

            Assert.AreEqual(jid1.ToString(), JID1);
            Assert.AreEqual(jid2.ToString(), JID1);

            jid2.Resource = "resource2";
            jid2.User = "user2";

            Assert.AreEqual(jid1.ToString(), JID1);
            Assert.AreEqual(jid2.ToString(), JID2);
        }

        [TestMethod]
        public void CompareJidTest1()
        {
            var a = new Jid("User1", "Server.com", "Res01");
            var b = new Jid("User1@Server.com/Res01");

            var c = new Jid("User1@Server.com/Res01", true);

            Assert.IsTrue(a.CompareTo(b) != 0);
        }

        [TestMethod]
        public void CompareJidTest2()
        {
            var a = new Jid("User1", "Server.com", "Res01");
            var b = new Jid("user1@server.com/Res01");
            Assert.IsTrue(a.CompareTo(b) == 0);
        }

        [TestMethod]
        public void CompareJidTest3()
        {
            var a = new Jid("User1", "Server.com", "Res01");
            var b = new Jid("User1@Server.com/Res01");
            var c = new Jid(b.User, b.Server, b.Resource);
            Assert.IsTrue(a.CompareTo(c) == 0);
            Assert.IsTrue(b.CompareTo(c) != 0);
        }
    }
}
