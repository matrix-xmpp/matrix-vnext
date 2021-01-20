namespace Matrix.Tests.Xmpp.Client
{
    using System;
    using Matrix.Xmpp.Client;
    using Shouldly;
    using Xunit;

    public class PresenceTest
    {
        [Fact]
        public void PriorityIsInRange()
        {
            ShouldThrowExtensions.ShouldNotThrow(() => new Presence { Priority = 10 });
            ShouldThrowExtensions.ShouldNotThrow(() => new Presence { Priority = 127 });
            ShouldThrowExtensions.ShouldNotThrow(() => new Presence { Priority = -127 });
        }

        [Fact]
        public void PriorityShouldThrowArgumentException()
        {
            ShouldThrowExtensions.ShouldThrow<ArgumentException>(() => new Presence { Priority = 500 });
            ShouldThrowExtensions.ShouldThrow<ArgumentException>(() => new Presence { Priority = 128 });
            ShouldThrowExtensions.ShouldThrow<ArgumentException>(() => new Presence { Priority = -128 });
        }
    }
}
