using System.Reflection;
using Matrix.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matrix.Xmpp.Tests
{
    [TestClass]
    public class Startup
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext testContext)
        {
            Factory.RegisterElementsFromAssembly(typeof(Matrix.Xmpp.Client.Iq).GetTypeInfo().Assembly);
        }
    }
}
