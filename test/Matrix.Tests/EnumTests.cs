/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
 *
 * This file is part of the MatriX project.
 *
 * NOTICE: All information contained herein is, and remains the property
 * of AG-Software and its suppliers, if any.
 * The intellectual and technical concepts contained herein are proprietary
 * to AG-Software and its suppliers and may be covered by German and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 *
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from AG-Software.
 *
 * Contact information for AG-Software is available at http://www.ag-software.de
 */

using Matrix.Attributes;
using Shouldly;
using Xunit;

namespace Matrix.Tests
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
