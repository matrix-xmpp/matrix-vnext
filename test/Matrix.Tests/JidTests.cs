/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
 *
 * This file is part of the MatriX project.
 *
 * NOTICE: All information contained herein is, and remains the property
 * of AG-Software and its suppliers, if any.
 * The intellectual and technical concepts contained herein are proprietary
 * to AG-Software and its suppliers and may be covered by German and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 *
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from AG-Software.
 *
 * Contact information for AG-Software is available at http://www.ag-software.de
 */

using Shouldly;
using Xunit;

namespace Matrix.Tests
{
    public class JidTests
    {

        [Fact]
        public void FullJidTestTest()
        {
            var jid1 = "user@server.de/resource";
            Jid jid = jid1;
            Assert.Equal(jid.ToString(), jid1);
            Assert.Equal((string)jid, jid1);
        }

        [Fact]
        public void BuilldJidFromUserAndServerTest()
        {
            const string user = "user";
            const string server = "server.com";

            var jid = new Jid { User = user, Server = server };
            Assert.Equal(jid.ToString(), user + "@" + server);
        }

        [Fact]
        public void JidEscapeTest()
        {
            const string user = "gnauck@ag-software.de";
            const string server = "server.com";

            var jid = new Jid();
            jid.SetUser(user);
            Assert.Equal(jid.User, @"gnauck\40ag-software.de");

            jid.SetServer(server);
            Assert.Equal(jid.Server, @"server.com");
            Assert.Equal(jid.ToString(), @"gnauck\40ag-software.de@server.com");
        }

        [Fact]
        public void UppercaseUsernameTest()
        {
            const string user = "Gnauck";
            const string server = "Server.com";

            var jid = new Jid { User = user };
            Assert.Equal(jid.User, user);

            jid.Server = server;
            Assert.Equal(jid.Server, server);
            Assert.Equal(jid.ToString(), user + "@" + server);

            jid.SetUser(user);
            jid.SetServer(server);
            Assert.Equal(jid.ToString(), "gnauck@server.com");
        }

        [Fact]
        public void CloneJidTest()
        {
            const string JID1 = "user@server.de/resource";
            const string JID2 = "user2@server.de/resource2";

            Jid jid1 = JID1;
            var jid2 = jid1.Clone();

            Assert.Equal(jid1.ToString(), JID1);
            Assert.Equal(jid2.ToString(), JID1);

            jid2.Resource = "resource2";
            jid2.User = "user2";

            Assert.Equal(jid1.ToString(), JID1);
            Assert.Equal(jid2.ToString(), JID2);
        }

        [Fact]
        public void CompareJidTest1()
        {
            var a = new Jid("User1", "Server.com", "Res01");
            var b = new Jid("User1@Server.com/Res01");

            var c = new Jid("User1@Server.com/Res01", true);

            Assert.True(a.CompareTo(b) != 0);
        }

        [Fact]
        public void CompareJidTest2()
        {
            var a = new Jid("User1", "Server.com", "Res01");
            var b = new Jid("user1@server.com/Res01");
            Assert.True(a.CompareTo(b) == 0);
        }

        [Fact]
        public void CompareJidTest3()
        {
            var a = new Jid("User1", "Server.com", "Res01");
            var b = new Jid("User1@Server.com/Res01");
            var c = new Jid(b.User, b.Server, b.Resource);
            Assert.True(a.CompareTo(c) == 0);
            Assert.True(b.CompareTo(c) != 0);
        }

        [Fact]
        public void BareJidEqualsTest()
        {
            var a = new Jid("user1@server.com/Res1");
            var b = new Jid("user1@server.com/Res2");
            
            a.Equals(b, new BareJidComparer()).ShouldBe(true);
        }

        [Fact]
        public void FullJidEqualsTest()
        {
            var a = new Jid("user1@server.com/Res1");
            var b = new Jid("user1@server.com/Res2");
            var c = new Jid("user1@server.com/Res2");
            var d = new Jid("UsEr1@server.com/Res2");

            a.Equals(b, new FullJidComparer()).ShouldBe(false);
            b.Equals(c, new FullJidComparer()).ShouldBe(true);
            //c.Equals(d, new FullJidComparer()).ShouldBe(true);
        }
    }
}
