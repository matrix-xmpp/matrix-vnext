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

using DotNetty.Transport.Bootstrapping;
using Matrix;
using Matrix.Network;
using Matrix.Network.Resolver;
using Shouldly;
using Xunit;
using System.Linq;

namespace Matrix.Tests
{
    public class ObjectExtensionsTests
    {
        [Fact]
        public void Is_Interface_Implemented_Test()
        {
            INameResolver resolver = new StaticNameResolver("example.com", 5222, true);
            resolver.Implements<IDirectTls>().ShouldBe(true);

            resolver.Cast<IDirectTls>().DirectTls.ShouldBe(true);

            string s = "hello world";
            s.Implements<IDirectTls>().ShouldBe(false);
        }
    }
}
