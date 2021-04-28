using Shouldly;
using Xunit;

namespace Matrix.Tests
{
    public class JidTests
    {
        [Fact]
        public void FullJidTestTest()
        {
            var jid1 = "local@domain.de/resource";
            Jid jid = jid1;
            Assert.Equal(jid.ToString(), jid1);
            Assert.Equal((string)jid, jid1);
        }

        [Fact]
        public void BuilldJidFromUserAndDomainTest()
        {
            const string local = "local";
            const string domain = "domain.com";

            var jid = new Jid { Local = local, Domain = domain };
            Assert.Equal(jid.ToString(), local + "@" + domain);
        }

        [Fact]
        public void JidEscapeTest()
        {
            const string local = "gnauck@ag-software.de";
            const string domain = "domain.com";

            var jid = new Jid();
            jid.SetLocal(local);
            Assert.Equal(@"gnauck\40ag-software.de", jid.Local);

            jid.SetDomain(domain);
            Assert.Equal(@"domain.com", jid.Domain);
            Assert.Equal(@"gnauck\40ag-software.de@domain.com", jid.ToString());
        }

        [Fact]
        public void UppercaseLocalnameTest()
        {
            const string local = "Gnauck";
            const string domain = "Domain.com";

            var jid = new Jid { Local = local };
            Assert.Equal(jid.Local, local);

            jid.Domain = domain;
            Assert.Equal(jid.Domain, domain);
            Assert.Equal(jid.ToString(), local + "@" + domain);

            jid.SetLocal(local);
            jid.SetDomain(domain);
            Assert.Equal("gnauck@domain.com", jid.ToString());
        }

        [Fact]
        public void CloneJidTest()
        {
            const string JID1 = "local@domain.de/resource";
            const string JID2 = "local2@domain.de/resource2";

            Jid jid1 = JID1;
            var jid2 = jid1.Clone();

            Assert.Equal(jid1.ToString(), JID1);
            Assert.Equal(jid2.ToString(), JID1);

            jid2.Resource = "resource2";
            jid2.Local = "local2";

            Assert.Equal(jid1.ToString(), JID1);
            Assert.Equal(jid2.ToString(), JID2);
        }

        [Fact]
        public void CompareJidTest1()
        {
            var a = new Jid("Local1", "Domain.com", "Res01");
            var b = new Jid("Local1@Domain.com/Res01");

            Assert.True(a.CompareTo(b) != 0);
        }

        [Fact]
        public void CompareJidTest2()
        {
            var a = new Jid("Local1", "Domain.com", "Res01");
            var b = new Jid("local1@domain.com/Res01");
            Assert.True(a.CompareTo(b) == 0);
        }

        [Fact]
        public void CompareJidTest3()
        {
            var a = new Jid("Local1", "Domain.com", "Res01");
            var b = new Jid("Local1@Domain.com/Res01");
            var c = new Jid(b.Local, b.Domain, b.Resource);
            Assert.True(a.CompareTo(c) == 0);
            Assert.True(b.CompareTo(c) != 0);
        }

        [Fact]
        public void BareJidEqualsTest()
        {
            var a = new Jid("local1@domain.com/Res1");
            var b = new Jid("local1@domain.com/Res2");
            
            a.Equals(b, new BareJidComparer()).ShouldBe(true);
        }

        [Fact]
        public void FullJidEqualsTest()
        {
            var a = new Jid("local1@domain.com/Res1");
            var b = new Jid("local1@domain.com/Res2");
            var c = new Jid("local1@domain.com/Res2");
            
            a.Equals(b, new FullJidComparer()).ShouldBe(false);
            b.Equals(c, new FullJidComparer()).ShouldBe(true);
        }
    }
}
