using System;
using Matrix.Core.Attributes;
using Matrix.Core;
using Shouldly;
using Xunit;

namespace Matrix.Core.Tests
{
    public class EnumTests
    {
        enum TestEnum
        {
            A = 1,
            B = 5,
            C = 11
        }

        enum Colors
        {
            Unknown = -1,

            [Name("rot")]
            Red = 1,

            [Name("blau")]
            Blue = 5,

            [Name("gelb")]
            Yellow = 11
        }

        [Fact]
        public void GetValuesTest()
        {
            Enum.GetValues<TestEnum>().ShouldBe(new int[]{1,5,11});
        }

        [Fact]
        public void ParseUsingNameAttributeTest()
        {
            Enum.ParseUsingNameAttrib<Colors>("rot").ShouldBe(Colors.Red);
            Enum.ParseUsingNameAttrib<Colors>("gelb").ShouldBe(Colors.Yellow);
            Enum.ParseUsingNameAttrib<Colors>("blau").ShouldBe(Colors.Blue);
            Enum.ParseUsingNameAttrib<Colors>("BLAU").ShouldNotBe(Colors.Blue);
            Enum.ParseUsingNameAttrib<Colors>("BLAU").ShouldBe(Colors.Unknown);
        }
    }
}
