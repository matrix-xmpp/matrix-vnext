using System;
using Xunit;
using Shouldly;

namespace Matrix.Tests
{
    public class XmppClientTests
    {
        [Fact]
        public void PriorityIsInRange()
        {
            ShouldThrowExtensions.ShouldNotThrow(() => new XmppClient { Priority = 10});
            ShouldThrowExtensions.ShouldNotThrow(() => new XmppClient { Priority = 127 });
            ShouldThrowExtensions.ShouldNotThrow(() => new XmppClient { Priority = -127 });
        }

        [Fact]
        public void PriorityShouldThrowArgumentException()
        {
            ShouldThrowExtensions.ShouldThrow<ArgumentException>(() => new XmppClient {Priority = 500});
            ShouldThrowExtensions.ShouldThrow<ArgumentException>(() => new XmppClient {Priority = 128});
            ShouldThrowExtensions.ShouldThrow<ArgumentException>(() => new XmppClient {Priority = -128});
        }


        [Fact]
        public void ResourceCannotBeNull()
        {
            ShouldThrowExtensions.ShouldThrow<ArgumentNullException>(() => new XmppClient { Resource = null });
            ShouldThrowExtensions.ShouldNotThrow(() => new XmppClient { Resource = "Foo" });
        }
    }
}
