using Shouldly;
using Xunit;

namespace Matrix.Tests
{
    using Matrix.Xmpp;
    using Matrix.Xmpp.Client;

    public class ObjectExtensionsTests
    {

        [Fact]
        public void Int_IsAnyOf_Should_Return_True()
        {
            1.IsAnyOf(0, 2, 3, 4, 5, 6, 1).ShouldBeTrue();
        }

        [Fact]
        public void String_IsAnyOf_Values_Should_Return_True()
        {
            "foo".IsAnyOf("foo", "bar", "foobar").ShouldBeTrue();
        }

        [Fact]
        public void String_IsAnyOf_Values_Should_Return_False()
        {
            "foo".IsAnyOf("foooo", "bar", "foobar").ShouldBeFalse();
        }

        [Fact]
        public void IqType_IsAnyOf_Values_Should_Return_True()
        {
            var iq = new Iq
            {
                Type = IqType.Result
            };

            iq.Type.IsAnyOf(IqType.Set, IqType.Result).ShouldBeTrue();
        }

        [Fact]
        public void IqType_IsAnyOf_Values_Should_Return_False()
        {
            var iq = new Iq
            {
                Type = IqType.Result
            };

            iq.Type.IsAnyOf(IqType.Error, IqType.Get).ShouldBeFalse();
        }
    }
}
