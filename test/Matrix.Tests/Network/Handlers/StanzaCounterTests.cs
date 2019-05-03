/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
 * See the COPYING file for more information.
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

using Matrix.Network.Handlers;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Network.Handlers
{
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
