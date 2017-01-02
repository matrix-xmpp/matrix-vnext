using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Matrix.Xmpp.Disco;
namespace Test.Xmpp.Disco
{
    [TestClass]
    public class IdendityTest
    {
        [TestMethod]
        public void TestEquals()
        {
            var id = new Identity {Type = "t", Name = "n", Category = "c"};
            var id1 = new Identity { Type = "t", Name = "n", Category = "c" };
            var id2 = new Identity { Type = "t2", Name = "n2", Category = "c2" };

            Assert.AreEqual(id.Equals(id1), true);
            Assert.AreEqual(id.Equals(id2), false);

            var list = new List<Identity>();
            list.Add(id);

            Assert.AreEqual(list.Contains(id), true);
            Assert.AreEqual(list.Contains(id1), true);
            Assert.AreEqual(list.Contains(id2), false);
        }
    }

}
