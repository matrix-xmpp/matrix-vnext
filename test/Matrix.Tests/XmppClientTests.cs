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
