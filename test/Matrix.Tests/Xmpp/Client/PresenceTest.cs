/*
 * Copyright (c) 2003-2020 by AG-Software <info@ag-software.de>
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
