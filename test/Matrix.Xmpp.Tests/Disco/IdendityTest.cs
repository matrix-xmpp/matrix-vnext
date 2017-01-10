using System.Collections.Generic;
using Xunit;

using Matrix.Xmpp.Disco;
namespace Matrix.Xmpp.Tests.Disco
{
    [Collection("Factory collection")]
    public class IdendityTest
    {
        [Fact]
        public void TestEquals()
        {
            var id = new Identity {Type = "t", Name = "n", Category = "c"};
            var id1 = new Identity { Type = "t", Name = "n", Category = "c" };
            var id2 = new Identity { Type = "t2", Name = "n2", Category = "c2" };

            Assert.Equal(id.Equals(id1), true);
            Assert.Equal(id.Equals(id2), false);

            var list = new List<Identity>();
            list.Add(id);

            Assert.Equal(list.Contains(id), true);
            Assert.Equal(list.Contains(id1), true);
            Assert.Equal(list.Contains(id2), false);
        }
    }

}
