namespace Matrix.Tests
{
    using Shouldly;
    using Xunit;

    public class StanzaCounterTests
    {
        [Fact]
        public void TestIncrement()
        {
            var counter = new StanzaCounter();
            counter.Increment();
            counter.Value.ShouldBe(1);

            counter.Increment();
            counter.Value.ShouldBe(2);
        }

        [Fact]
        public void TestIncrementOverMaximumValue()
        {
            var counter = new StanzaCounter();
            // init with 1 below max
            counter.Value = 4294967295;
            counter.Increment();
            counter.Value.ShouldBe(0);
        }
    }
}
