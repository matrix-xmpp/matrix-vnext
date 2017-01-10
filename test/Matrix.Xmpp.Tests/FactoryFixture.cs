using System.Reflection;
using Matrix.Xml;
using Xunit;

namespace Matrix.Xmpp.Tests
{
    public class FactoryFixture
    {
        public FactoryFixture()
        {
            Factory.RegisterElementsFromAssembly(typeof(Matrix.Xmpp.Client.Iq).GetTypeInfo().Assembly);
        }
    }
}
